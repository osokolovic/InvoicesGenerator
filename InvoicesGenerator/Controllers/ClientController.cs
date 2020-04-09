using Invoices.Models;
using Invoices.Services.Interfaces;
using InvoicesGenerator.ViewModels;
using Newtonsoft.Json;
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

    }
}