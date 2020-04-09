using Invoices.Models;
using Invoices.Services.Interfaces;
using InvoicesGenerator.ViewModels;
using Newtonsoft.Json;
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
            ViewBag.Title = "Invoice generator";
            return View();
        }

        public ActionResult AddClientForm()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult UpDelClientForm()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult InvoiceForm()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult AddClient(ClientFormViewModel client)
        {
            var cli = client;
            return View();
        }

        [HttpGet]
        public string GetClientById(int id)
        {
            var client = clientService.GetClient(id);
            return JsonConvert.SerializeObject(client);
        }
    }
}