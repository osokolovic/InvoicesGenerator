using Invoices.Services.Interfaces;
using InvoicesGenerator.ViewModels;
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

        public InvoiceController(IInvoiceService invoiceService, IClientService clientService, IChargeService chargeService)
        {
            this.invoiceService = invoiceService;
            this.clientService = clientService;
            this.chargeService = chargeService;
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
            return View();
        }

        [HttpPost]
        public ActionResult AddInvoiceForm(InvoiceFormViewModel invoice)
        {
            if (ModelState.IsValid)
            {
                return View();
            } 
            else
            {
                return View(invoice);
            }
        }

        public void DownloadExcel(int InvoiceId)
        {
            var dbInvoice = invoiceService.GetInvoice(InvoiceId);
            var dbClient = clientService.GetClient(dbInvoice.ClientId);
            var dbCharges = chargeService.GetInvoiceCharges(InvoiceId).ToList();

            var calculationResult = chargeService.CalculateCharge(InvoiceId);
            var unitsPerChargeName = chargeService.GetUnits(dbCharges);
            var ratePerChargeName = chargeService.GetRates(dbCharges);

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
    }
}