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

namespace SocialShopper.Core.Tests.ViewModels
{
    [TestFixture]
    public class MenuViewModelTEST
    {
        [Test]
        public void cTor()
        {
            var testee = new MenuViewModel();

        }
    }

    [TestFixture]
    public class ProductSearchViewModelTEST : MvxIoCSupportingTest
    {

        protected MockDispatcher MockDispatcher { get; private set; }

        protected override void AdditionalSetup()
        {
            MockDispatcher = new MockDispatcher();
            Ioc.RegisterSingleton<IMvxViewDispatcher>(MockDispatcher);
            Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(MockDispatcher);

            // for navigation parsing
            Ioc.RegisterSingleton<IMvxStringToTypeParser>(new MvxStringToTypeParser());

            //Ioc.RegisterSingleton<IDataInitializerService>();

            Ioc.Resolve<IDataInitializerService>().Init();
            Ioc.Resolve<IDataFillerService>().Init();

            //Ioc.IoCConstruct();

            //var creatableTypes = MvxTypeExtensions.CreatableTypes(typeof(IDataInitializerService).Assembly);

            //foreach (var pair in creatableTypes
            //  .EndingWith("DataService")
            //  .AsInterfaces())
            //{
            //    Ioc.IoCConstruct();
            //}
        }

        [SetUp]
        public void SetUp()
        {
            //var creatableTypes = MvxTypeExtensions.CreatableTypes(typeof(IDataInitializerService).Assembly);

            //foreach (var pair in creatableTypes
            //  .EndingWith("DataService")
            //  .AsInterfaces())
            //{
            //    Ioc.RegisterSingleton();
            //}

            //creatableTypes
            //  .EndingWith("DataService")
            //  .AsInterfaces()
            //  .RegisterAsSingleton();

            //creatableTypes
            //    .EndingWith("InitializerService")
            //    .AsInterfaces()
            //    .RegisterAsLazySingleton();

            //creatableTypes
            //    .EndingWith("FillerService")
            //    .AsInterfaces()
            //    .RegisterAsLazySingleton();

            //Mvx.Resolve<IDataInitializerService>().Init();
            //Mvx.Resolve<IDataFillerService>().Init();
        }

        [Test]
        public void cTor()
        {
            base.Setup();

            var productCodeDataService = Mvx.Resolve<IProductCategoryDataService>();

            //var testee = new ProductSearchViewModel()
        }
    }
}
