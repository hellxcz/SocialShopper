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
	public class ProductDetailViewModelTEST : TestBase
	{
		[Test]
		public void cTor()
		{
			var testee = Ioc.Resolve<ProductDetailViewModel> ();

			Assert.IsNotNull (testee);
		}
	}

}
