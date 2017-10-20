[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MVCHoldem.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MVCHoldem.Web.App_Start.NinjectWebCommon), "Stop")]

namespace MVCHoldem.Web.App_Start
{
    using System;
    using System.Data.Entity;
    using System.Web;
    using AutoMapper;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using MVCHoldem.Auth.Contracts;
    using MVCHoldem.Auth.Managers;
    using MVCHoldem.Data;
    using MVCHoldem.Data.Contracts;
    using MVCHoldem.Data.SaveChanges;
    using MVCHoldem.Data.SetWrappers;
    using MVCHoldem.Services;
    using MVCHoldem.Services.Contracts;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();
        private static Type userServiceAssemblyType = typeof(UserService);

        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(x =>
            {
                x.FromThisAssembly()
                 .SelectAllClasses()
                 .BindDefaultInterface();
            });

            kernel.Bind(x =>
            {
                x.FromAssemblyContaining(typeof(IUserService))
                 .SelectAllClasses()
                 .BindDefaultInterface();
            });

            kernel.Bind(typeof(DbContext), typeof(EfDbContext)).To<EfDbContext>().InRequestScope();
            kernel.Bind(typeof(IEfDbSetWrapper<>)).To(typeof(EfDbSetWrapper<>));
            kernel.Bind(typeof(IEfDbContextSaveChanges)).To(typeof(EfDbContextSaveChanges));
            kernel.Bind<IApplicationSignInManager>().ToMethod(_ => HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>());
            kernel.Bind<IApplicationUserManager>().ToMethod(_ => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>());
        }        
    }
}
