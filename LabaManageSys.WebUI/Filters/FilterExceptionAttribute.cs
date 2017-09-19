using System.Web.Mvc;
using LabaManageSys.WebUI.Abstract;

namespace LabaManageSys.WebUI.Filters
{
    public class FilterExceptionAttribute : HandleErrorAttribute
    {
        private ILogger log;

        public FilterExceptionAttribute(ILogger log)
        {
           this.log = log;
        }

        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                this.log.Debug(filterContext.Exception);
                base.OnException(filterContext);
            }
        }
    }
}