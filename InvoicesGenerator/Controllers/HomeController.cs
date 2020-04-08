using Invoices.Models;
using Invoices.Services.Interfaces;
using InvoicesGenerator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoicesGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClientService clientService;

        public HomeController()
        {

        }
        public HomeController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult AddClient(ClientViewModel client)
        {
            if (client != null)
            {
                var dbClient = new Client
                {
                    ClientId = client.ClientId,
                    CompanyName = client.CompanyName,
                    Address = client.Address
                };
                clientService.CreateClient(dbClient);

                clientService.SaveClient();
                return Json(new { success = true, responseText = "Your message successfuly sent!" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, responseText = "The attached file is not supported." }, JsonRequestBehavior.AllowGet);
        }
    }
}