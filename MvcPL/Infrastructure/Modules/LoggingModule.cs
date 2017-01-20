using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;

namespace MvcPL.Infrastructure.Modules
{
    public class LoggingModule : IHttpModule
    {
        public void Init(HttpApplication application)
        {
            application.PostLogRequest += Application_PostLogRequest;
        }

        public void Application_PostLogRequest(object sender, EventArgs args)
        {
            LogManager.Flush();
        }

        public void Dispose() { }
    }
}