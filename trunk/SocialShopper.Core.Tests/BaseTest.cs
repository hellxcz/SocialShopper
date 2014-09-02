// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using Cirrious.CrossCore.IoC;
using SocialShopper.Core.Services.Interface;
using System;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.Community.Plugins.Sqlite.Wpf;
using Acr.MvvmCross.Plugins.BarCodeScanner;
using Moq;


namespace SocialShopper.Core.Tests
{
    using Cirrious.CrossCore.Core;
    using Cirrious.CrossCore.Platform;
    using Cirrious.MvvmCross.Platform;
    using Cirrious.MvvmCross.Test.Core;
    using Cirrious.MvvmCross.Views;
    using Mocks;
    using NUnit.Framework;

    /// <summary>
    /// Defines the BaseTest type.
    /// </summary>
    [TestFixture]
    public abstract class BaseTest : MvxIoCSupportingTest
    {
        /// <summary>
        /// The mock dispatcher.
        /// </summary>
        private MockDispatcher mockDispatcher;

		protected static IEnumerable<Type> CreatableTypes()
		{
			return MvxTypeExtensions.CreatableTypes(typeof(IDataInitializerService).Assembly);
		}


        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public virtual void SetUp()
        {
            this.ClearAll();

            this.mockDispatcher = new MockDispatcher();

            Ioc.RegisterSingleton<IMvxViewDispatcher>(this.mockDispatcher);
            Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(this.mockDispatcher);
            Ioc.RegisterSingleton<IMvxTrace>(new TestTrace());
            Ioc.RegisterSingleton<IMvxSettings>(new MvxSettings());

            this.Initialize();
            this.CreateTestableObject();

			// for navigation parsing
			Ioc.RegisterSingleton<IMvxStringToTypeParser>(new MvxStringToTypeParser());

			Ioc.RegisterSingleton<ISQLiteConnectionFactory>(new MvxWpfSqLiteConnectionFactory());
			Ioc.RegisterSingleton<IMvxMessenger>(new MvxMessengerHub());

			var barcodeScannerMock = new Mock<IBarCodeScanner> ();

			Ioc.RegisterSingleton<IBarCodeScanner>(barcodeScannerMock.Object);

			SocialShopper.Core.App.InitializeServices(CreatableTypes);

			SocialShopper.Core.App.InitializeData();
        }

        /// <summary>
        /// Creates the testable object.
        /// </summary>
        public virtual void CreateTestableObject()
        {
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public virtual void TearDown()
        {
            this.Terminate();
        }

        /// <summary>
        /// Initializes this instance.
        /// Any specific setup code for derived classes should override this method.
        /// </summary>
        protected virtual void Initialize()
        {
        }

        /// <summary>
        /// Terminates this instance.
        /// Any specific termination code for derived classes should override this method.
        /// </summary>
        protected virtual void Terminate()
        {
        }
    }
}
