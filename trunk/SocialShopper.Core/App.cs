using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.IoC;
using Cirrious.CrossCore.Plugins;
using SocialShopper.Core.Services;
using SocialShopper.Core.Services.Interface;
using System.Collections.Generic;

namespace SocialShopper.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
		public static void InitializeServices(Func<IEnumerable<Type>> CreatableTypes)
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

			Mvx.Resolve<IDataInitializerService>().Init();
		}

		public static void InitializeData()
		{
			Mvx.Resolve<IDataFillerService>().Init();
		}

        public override void Initialize()
        {
			InitializeServices (CreatableTypes);

			InitializeData();

            RegisterAppStart<ViewModels.MenuViewModel>();

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