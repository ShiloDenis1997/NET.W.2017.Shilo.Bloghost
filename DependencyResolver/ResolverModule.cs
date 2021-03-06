﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Concrete;
using BLL.Interfaces.Services;
using DAL.Concrete;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Managers;
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
            kernel.Bind<IBlogRepository>().To<BlogRepository>();
            kernel.Bind<IBlogService>().To<BlogService>();
            kernel.Bind<IArticleRepository>().To<ArticleRepository>();
            kernel.Bind<IArticleService>().To<ArticleService>();
            kernel.Bind<ICommentRepository>().To<CommentRepository>();
            kernel.Bind<ICommentService>().To<CommentService>();
            kernel.Bind<ILikeManager>().To<LikeManager>();
            kernel.Bind<ILikeService>().To<LikeService>();
            kernel.Bind<ITagRepository>().To<TagRepository>();
            kernel.Bind<ITagService>().To<TagService>();
        }
    }
}
