using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Test.Core;
using Moq;
using NUnit.Framework;
using SocialShopper.Core.Services.Interface;
using SocialShopper.Core.ViewModels;

using Cirrious.CrossCore.IoC;
using SocialShopper.Core.Tests.Mocks;
using Cirrious.MvvmCross.Views;
using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.Platform;
using Cirrious.MvvmCross.Community.Plugins.Sqlite.Wpf;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;

namespace SocialShopper.Core.Tests.ViewModels
{

	public class TestBase : MvxIoCSupportingTest
	{
		protected MockDispatcher MockDispatcher { get; private set; }

		protected IEnumerable<Type> CreatableTypes()
		{
			return MvxTypeExtensions.CreatableTypes(typeof(IDataInitializerService).Assembly);
		}

		protected override void AdditionalSetup()
		{
			MockDispatcher = new MockDispatcher();
			Ioc.RegisterSingleton<IMvxViewDispatcher>(MockDispatcher);
			Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(MockDispatcher);

			// for navigation parsing
			Ioc.RegisterSingleton<IMvxStringToTypeParser>(new MvxStringToTypeParser());

			Ioc.RegisterSingleton<ISQLiteConnectionFactory>(new MvxWpfSqLiteConnectionFactory());

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

			var s = Ioc.Resolve<IProductDataService> ();

			Ioc.Resolve<IDataInitializerService>().Init();
			Ioc.Resolve<IDataFillerService>().Init();

		}
	}
    
}
