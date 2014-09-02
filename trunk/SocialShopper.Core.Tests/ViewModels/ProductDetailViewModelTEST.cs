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
using Cirrious.MvvmCross.Plugins.Messenger;
using Acr.MvvmCross.Plugins.BarCodeScanner;

namespace SocialShopper.Core.Tests.ViewModels
{
	[TestFixture]
	public class ProductDetailViewModelTEST : BaseTest
	{
		private ProductDetailViewModel GetTestee()
		{
			return new ProductDetailViewModel (
				Ioc.Resolve<IMvxMessenger> (), 
				Ioc.Resolve<IProductDataService> (), 
				Ioc.Resolve<IProductCodeDataService> (), 
				Ioc.Resolve<IBarCodeScanner> ());
		}

		[Test]
		public void cTor()
		{
			base.Setup ();

			var testee = GetTestee();

			Assert.IsNotNull (testee);
		}
	}

}
