using Invoices.Models;
using Invoices.Services.Interfaces;
using InvoicesGenerator.ViewModels;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoicesGenerator.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService invoiceService;
        private readonly IClientService clientService;
        private readonly IChargeService chargeService;
        private readonly IChargeNameService chargeNameService;

        public InvoiceController(IInvoiceService invoiceService, IClientService clientService, 
            IChargeService chargeService, IChargeNameService chargeNameService)
        {
            this.invoiceService = invoiceService;
            this.clientService = clientService;
            this.chargeService = chargeService;
            this.chargeNameService = chargeNameService;
        }

        public InvoiceController()
        {

        }

        public ActionResult Index()
        {
            ViewBag.Title = "Invoice generator";
            return View();
        }

        public ActionResult AddInvoiceForm()
        {
            var companies = clientService.GetClients().ToList();
            ViewBag.Companies = companies;
            return View();
        }

        [HttpPost]
        public ActionResult AddInvoiceForm(InvoiceFormViewModel invoice)
        {
            if (ModelState.IsValid)
            {
                Invoice dbInvoice = invoice;
                try
                {
                    //get company id
                    var client = clientService.GetClient(invoice.CompanyName);
                    dbInvoice.Client = client;
                    dbInvoice.ClientId = client.ClientId;

                    invoiceService.CreateInvoice(dbInvoice);
                    invoiceService.SaveInvoice();
                    ViewBag.MainMessage = "New invoice added successfully.";
                    ViewBag.MessageType = "success";
                }
                catch (Exception e)
                {
                    ViewBag.MainMessage = "Error, new invoice was not added.";
                    ViewBag.MessageType = "danger";
                }
                return View();
            } 
            else
            {
                return View(invoice);
            }
        }

        public ActionResult UpDelInvoiceForm()
        {
            var companies = clientService.GetClients().ToList();
            ViewBag.Clients = companies;
            return View();
        }

        public string GetInvoicesForClient(int clientId)
        {
            var invoices = invoiceService.GetClientInvoices(clientId).Select(i => new {
                i.InvoiceId,
                i.InvoiceNumber,
                i.CompanyName,
                i.ClientId
            });
            
            return JsonConvert.SerializeObject(invoices);
        }

        public string GetInvoiceById(int invoiceId)
        {
            var dbInvoice = invoiceService.GetInvoice(invoiceId);
            InvoiceFormViewModel viewInvoice = dbInvoice;

            return JsonConvert.SerializeObject(viewInvoice);
        }

        private List<InvoiceFormViewModel> castInvoices(IEnumerable<Invoice> invoices)
        {
            List<InvoiceFormViewModel> viewInvoices = new List<InvoiceFormViewModel>();
            foreach (var item in invoices)
            {
                viewInvoices.Add(item);
            }

            return viewInvoices;
        }

        public ActionResult ShowInvoices(string sortOrder, string CompanyName = null)
        {
            var invoices = invoiceService.GetInvoices();
            var sortedInvoices = invoiceService.SortInvoicesByParam(invoices, sortOrder);
            if (String.IsNullOrEmpty(CompanyName) == false)
            {
                ViewBag.Filtered = CompanyName;
                sortedInvoices = invoiceService.FilterByCompanyName(sortedInvoices, CompanyName);
            }

            return View(this.castInvoices(sortedInvoices));
        }

        public ActionResult ShowFilteredInvoices(string CompanyName)
        {
            var filteredInvoices = invoiceService.GetInvoices();
            if (String.IsNullOrEmpty(CompanyName) == false)
            {
                ViewBag.Filtered = CompanyName;
                filteredInvoices = invoiceService.FilterByCompanyName(filteredInvoices, CompanyName);
            }

            return View("ShowInvoices", this.castInvoices(filteredInvoices));
        }

        public ActionResult Delete(InvoiceFormViewModel invoice)
        {
            try
            {
                invoiceService.DeleteInvoice(invoice);
                invoiceService.SaveInvoice();
                //Poruke bi trebale biti izdvojene u zaseban file zbog mogucnosti lokalizacije u buducnosti
                TempData["MainMessage"] = "Invoice was deleted successfully.";
                TempData["MessageType"] = "success";
            }
            catch (Exception e)
            {
                TempData["MainMessage"] = "Error, invoice was not deleted.";
                TempData["MessageType"] = "danger";
            }

            var companies = clientService.GetClients().ToList();
            ViewBag.Clients = companies;
            return View("UpDelInvoiceForm", invoice);
        }

        public ActionResult Update(InvoiceFormViewModel invoice)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    invoiceService.UpdateInvoice(invoice);
                    invoiceService.SaveInvoice();
                    TempData["MainMessage"] = "Invoice was updated successfully.";
                    TempData["MessageType"] = "success";
                }
                catch (Exception e)
                {
                    TempData["MainMessage"] = "Error, client was not updated.";
                    TempData["MessageType"] = "danger";
                }
            }
            else
            {
                var companies = clientService.GetClients().ToList();
                ViewBag.Clients = companies;
                return View("UpDelInvoiceForm", invoice);
            }

            return RedirectToAction("UpDelInvoiceForm");
        }

        public void DownloadExcel(int invoiceId)
        {
            var dbInvoice = invoiceService.GetInvoice(invoiceId);
            var dbClient = clientService.GetClient(dbInvoice.ClientId);
            var dbCharges = chargeService.GetInvoiceCharges(invoiceId).ToList();

            var calculationResult = chargeService.CalculateCharge(invoiceId);
            var unitsPerChargeName = chargeService.GetUnits(dbCharges);
            var ratePerChargeName = chargeService.GetRates(dbCharges);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Invoice Print");

            // Sheet labels
            Sheet.Cells["B6"].Value = "Contract";
            Sheet.Cells["B4"].Value = "Invoice number";
            Sheet.Cells["G2"].Value = "Invoice date";
            Sheet.Cells["B2"].Value = "Company name";
            Sheet.Cells["G4"].Value = "Start date";
            Sheet.Cells["G6"].Value = "End date";

            // Sheet values
            Sheet.Cells["D6"].Value = dbClient.Contract;
            Sheet.Cells["D4"].Value = dbInvoice.InvoiceNumber;
            Sheet.Cells["I2"].Value = string.Format("{0:d.M.yyyy}", dbInvoice.InvoiceDate);
            Sheet.Cells["D2"].Value = dbInvoice.CompanyName;
            Sheet.Cells["I4"].Value = string.Format("{0:d.M.yyyy}", dbInvoice.StartDate);
            Sheet.Cells["I6"].Value = string.Format("{0:d.M.yyyy}", dbInvoice.EndDate);

            // Sheet labels
            Sheet.Cells["A10"].Value = "Charge";
            Sheet.Cells["A11"].Value = "Day";
            Sheet.Cells["A12"].Value = "Night";
            Sheet.Cells["A13"].Value = "Weekend";
            Sheet.Cells["C10"].Value = "Units";
            Sheet.Cells["E10"].Value = "Rate";
            Sheet.Cells["I10"].Value = "Amount";

            // Sheet values
            Sheet.Cells["C11"].Value = unitsPerChargeName["Day"];       // Units per day
            Sheet.Cells["C12"].Value = unitsPerChargeName["Night"];     // Units per night
            Sheet.Cells["C13"].Value = unitsPerChargeName["Weekend"];   // Units per weekend
            Sheet.Cells["E11"].Value = ratePerChargeName["Day"]; ;      // Rate per day
            Sheet.Cells["E12"].Value = ratePerChargeName["Night"]; ;    // Rate per night
            Sheet.Cells["E13"].Value = ratePerChargeName["Weekend"]; ;  // Rate per weekend

            Sheet.Cells["I11"].Value = unitsPerChargeName["Day"] * ratePerChargeName["Day"];
            Sheet.Cells["I12"].Value = unitsPerChargeName["Night"] * ratePerChargeName["Night"];
            Sheet.Cells["I13"].Value = unitsPerChargeName["Weekend"] * ratePerChargeName["Weekend"];

            // Sheet labels
            Sheet.Cells["A16"].Value = "Total";
            Sheet.Cells["A17"].Value = "Tax";
            Sheet.Cells["A18"].Value = "Grand Total";

            // Sheet values
            Sheet.Cells["I16"].Value = calculationResult["Total"];
            Sheet.Cells["I17"].Value = calculationResult["Tax"];
            Sheet.Cells["I18"].Value = calculationResult["GrandTotal"];

            // Execute
            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

        public ActionResult GetChart(int year, int month)
        {
            List<InvoiceChart> chart = invoiceService.GetInvoiceChart(year, month).ToList();
            List<InvoiceChartFormViewModel> result = new List<InvoiceChartFormViewModel>
            {
                new InvoiceChartFormViewModel { Name = chart[0].Name, Total = chart[0].Total },
                new InvoiceChartFormViewModel { Name = chart[1].Name, Total = chart[1].Total },
                new InvoiceChartFormViewModel { Name = chart[2].Name, Total = chart[2].Total }
            };

            ViewBag.Year = year;
            ViewBag.Month = month;

            return RedirectToAction("ShowInvoiceChart", result);
        }
    }
}