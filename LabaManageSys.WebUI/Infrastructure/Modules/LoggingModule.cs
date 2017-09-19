using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Concrete;
using log4net;
using Ninject.Modules;

namespace LabaManageSys.WebUI.Infrastructure
{
    public class LoggingModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.ReflectedType));
            Bind<ILogger>().To<Logger>();
        }
    }
}