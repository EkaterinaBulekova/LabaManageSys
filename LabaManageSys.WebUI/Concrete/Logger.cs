using System;
using LabaManageSys.WebUI.Abstract;
using log4net;
using log4net.Config;

namespace LabaManageSys.WebUI.Concrete
{
   public class Logger : ILogger
    {
        private ILog log;

        public Logger(ILog logger)
        {
            this.log = logger;
        }

        public static void InitLogger()
        {
            XmlConfigurator.Configure();
        }

        public void Debug(Exception exception)
        {
            this.log.Debug(exception);
        }

        public void Debug(string message)
        {
            this.log.Debug(message);
        }

        public void Debug(string message, Exception exception)
        {
            this.log.Debug(message, exception);
        }

        public void Info(string message)
        {
            this.log.Info(message);
        }
    }
}