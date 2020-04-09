using Invoices.Models;
using Invoices.Services.Interfaces;
using InvoicesGenerator.ViewModels;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoicesGenerator.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService clientService;

        public ClientController()
        {

        }

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Invoice generator";
            return View();
        }

        public ActionResult UpDelClientForm()
        {
            var clients = clientService.GetClients();
            ViewBag.Clients = clients;

            return View();
        }

        public ActionResult AddClientForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddClientForm(ClientFormViewModel client)
        {
            if (ModelState.IsValid)
            {
                Client dbClient = client;
                clientService.CreateClient(dbClient);
                try
                {
                    clientService.SaveClient();
                    ViewBag.MainMessage = "New client added successfully.";
                    ViewBag.MessageType = "success";
                }
                catch (Exception e) {
                    //Pozeljno je dodatno implementirati bolji exception handling
                    //koji bi uključivao i logovanje izuzetaka
                    ViewBag.MainMessage = "Error, new client was not added.";
                    ViewBag.MessageType = "danger";
                }
                
                return View();
            }
            else
            {
                return View(client);
            }
        }
        
        [HttpGet]
        public string GetClientById(int clientId)
        {
            var client = clientService.GetClient(clientId);

            return JsonConvert.SerializeObject(client);
        }

        public ActionResult Update(ClientFormViewModel client)
        {
            var dbClient = client;
            if (ModelState.IsValid)
            {
                try
                {
                    clientService.UpdateClient(dbClient);
                    clientService.SaveClient();
                    //Poruke bi trebale biti izdvojene u zaseban file zbog mogucnosti lokalizacije u buducnosti
                    TempData["MainMessage"] = "Client was updated successfully.";
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
                var clients = clientService.GetClients();
                ViewBag.Clients = clients;
                return View("UpDelClientForm", client);
            }

            return RedirectToAction("UpDelClientForm");
        }

        public ActionResult Delete(ClientFormViewModel client)
        {
            var dbClient = client;
            if (ModelState.IsValid)
            {
                try
                {
                    clientService.DeleteClient(client);
                    clientService.SaveClient();
                    //Poruke bi trebale biti izdvojene u zaseban file zbog mogucnosti lokalizacije u buducnosti
                    TempData["MainMessage"] = "Client was deleted successfully.";
                    TempData["MessageType"] = "success";
                }
                catch (Exception e)
                {
                    TempData["MainMessage"] = "Error, client was not deleted.";
                    TempData["MessageType"] = "danger";
                }
            }
            else
            {
                var clients = clientService.GetClients();
                ViewBag.Clients = clients;
                return View("UpDelClientForm", client);
            }

            return RedirectToAction("UpDelClientForm");
        }

        private List<ClientFormViewModel> castClients(IEnumerable<Client> clients)
        {
            List<ClientFormViewModel> viewClients = new List<ClientFormViewModel>();
            foreach (var item in clients)
            {
                viewClients.Add(item);
            }

            return viewClients;
        }

        public ActionResult ShowClients(string sortOrder, string CompanyName = null)
        {
            var clients = clientService.GetClients();
            var sortedClients = clientService.SortClientsByParam(clients, sortOrder);
            if (String.IsNullOrEmpty(CompanyName) == false)
            {
                ViewBag.Filtered = CompanyName;
                sortedClients = clientService.FilterByCompanyName(sortedClients, CompanyName);
            }

            return View(this.castClients(sortedClients));
        }

        public ActionResult ShowFilteredClients(string CompanyName)
        {
            var filteredClients = clientService.GetClients();
            if (String.IsNullOrEmpty(CompanyName) == false)
            {
                ViewBag.Filtered = CompanyName;
                filteredClients = clientService.FilterByCompanyName(filteredClients, CompanyName);
            }

            return View("ShowClients", this.castClients(filteredClients));
        }

    }
}