[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MusicR8r.App_Start.NinjectConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MusicR8r.App_Start.NinjectConfig), "Stop")]

namespace MusicR8r.App_Start
{
    using AutoMapper;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using MusicR8r.Data;
    using MusicR8r.Data.Repositories;
    using MusicR8r.Data.UnitOfWork;
    using MusicR8r.Services.Contracts;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;
    using System;
    using System.Data.Entity;
    using System.Web;

    public static class NinjectConfig
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
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

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
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
                x.FromAssemblyContaining(typeof(IService))
                 .SelectAllClasses()
                 .BindDefaultInterface();
            });
            
            kernel.Bind(typeof(DbContext), typeof(MsSqlDbContext)).To<MsSqlDbContext>().InRequestScope();

            kernel.Bind(typeof(IEfRepository<>)).To(typeof(EfRepository<>));
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();

            kernel.Bind<IMapper>().ToMethod(x => Mapper.Instance).InSingletonScope();
        }
    }
}
