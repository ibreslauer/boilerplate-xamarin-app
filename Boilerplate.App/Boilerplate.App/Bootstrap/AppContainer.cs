using Autofac;
using Boilerplate.App.Repositories;
using Boilerplate.App.Repositories.Contracts;
using Boilerplate.App.Services.Data;
using Boilerplate.App.Services.Data.Contracts;
using Boilerplate.App.Services.General;
using Boilerplate.App.Services.General.Contracts;
using Boilerplate.App.ViewModels;
using Boilerplate.App.ViewModels.Samples;
using System;

namespace Boilerplate.App.Bootstrap
{
    public class AppContainer
    {
        private static IContainer _container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // ViewModels
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<SettingsViewModel>();
            builder.RegisterType<AppRootViewModel>();
            builder.RegisterType<AppRootMenuViewModel>();
            builder.RegisterType<MainMenuViewModel>();
            builder.RegisterType<SampleViewModel>();
            builder.RegisterType<SamplePopupViewModel>();
            builder.RegisterType<SampleLookupViewModel>();

            // Services - data
            builder.RegisterType<AvailabilityService>().As<IAvailabilityService>();
            
            // Services - general
            builder.RegisterType<ConnectionService>().As<IConnectionService>().SingleInstance();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().SingleInstance();
            builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();
            builder.RegisterType<AppSettings>().As<IAppSettings>().SingleInstance();

            // General
            builder.RegisterType<GenericRepository>().As<IGenericRepository>();

            _container = builder.Build();
        }

        public static object Resolve(Type typeName)
        {
            return _container.Resolve(typeName);
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
