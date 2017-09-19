using System.Web.Mvc;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Filters;

namespace LabaManageSys.WebUI.App_Start
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new FilterExceptionAttribute(DependencyResolver.Current.GetService<ILogger>()));
        }
    }
}