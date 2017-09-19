using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LabaManageSys.WebUI.App_Start;
using LabaManageSys.WebUI.Infrastructure.Binders;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(FilterModel), new FilterModelBinder());
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
