using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Concrete;
using BLL.Interfaces.Services;
using DAL.Concrete;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repository;
using Logger.Concrete;
using Ninject;
using Ninject.Web.Common;
using NLog;
using ORM;
using ILogger = Logger.Interfaces.ILogger;

namespace DependencyResolver
{
    public static class ResolverModule
    {
        public static void ConfigurateResolver(this IKernel kernel)
        {
            kernel.Bind<DbContext>().To<BlogHostModel>().InRequestScope();
            kernel.Bind<ILogger>().ToMethod
                (context => new LoggerToILoggerAdapter
                    (LogManager.GetLogger(context.Request.Target?.Name))).InRequestScope();
            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<IRepository<DalUser>>().To<UserRepository>().InRequestScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
