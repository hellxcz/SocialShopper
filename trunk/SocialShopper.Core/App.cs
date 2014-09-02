using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.IoC;
using Cirrious.CrossCore.Plugins;
using SocialShopper.Core.Services;
using SocialShopper.Core.Services.Interface;

namespace SocialShopper.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                 .EndingWith("DataService")
                 .AsInterfaces()
                 .RegisterAsSingleton();

            CreatableTypes()
                .EndingWith("InitializerService")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CreatableTypes()
                .EndingWith("FillerService")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            
            CreatableTypes()
                .EndingWith("CodeScanner")
                .AsInterfaces()
                .RegisterAsLazySingleton();

			CreatableTypes()
				.EndingWith("PublisherService")
				.AsInterfaces()
				.RegisterAsLazySingleton();

            RegisterAppStart<ViewModels.MenuViewModel>();

            Mvx.Resolve<IDataInitializerService>().Init();
            Mvx.Resolve<IDataFillerService>().Init();
        }

        public override void LoadPlugins(IMvxPluginManager pluginManager)
        {
            base.LoadPlugins(pluginManager);
            try
            {
                pluginManager.EnsurePluginLoaded<Acr.MvvmCross.Plugins.BarCodeScanner.PluginLoader>();
            }
            catch (Exception)
            {

            }
        }
    }
}