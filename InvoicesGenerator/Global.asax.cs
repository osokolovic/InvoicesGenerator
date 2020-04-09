using Invoices.Data;
using Store.Web.App_Start;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace InvoicesGenerator
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        { 
            // Init database
            //System.Data.Entity.Database.SetInitializer(new InvoiceSeedData());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Autofac configuration
            Bootstrapper.Run();
        }
    }
}
