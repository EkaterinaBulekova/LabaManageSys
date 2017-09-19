using System;
using log4net;

namespace LabaManageSys.WebUI.Abstract
{
    public interface ILogger
    {
        void Debug(string message);

        void Debug(string message, Exception exception);

        void Debug(Exception exception);

        void Info(string message);
    }
}
