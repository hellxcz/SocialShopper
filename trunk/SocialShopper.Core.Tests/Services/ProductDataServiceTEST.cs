using System;
using NUnit.Framework;
using SocialShopper.Core.Services.Interface;
using System.Linq;
using SocialShopper.Core.Entities;

namespace SocialShopper.Core.Tests.Services
{
	[TestFixture]
	public class ProductDataServiceTEST : BaseTest
	{
		private IProductDataService GetTestee()
		{
			return Ioc.Resolve<IProductDataService> ();
		}

		[Test]
		public void cTor()
		{
			var testee = GetTestee();
		}

		[Test]
		public void SaveProductCode()
		{
			var productCodeDataService = Ioc.Resolve<IProductCodeDataService>();

			productCodeDataService.Save(new ProductCode(){ Value = "555" });

			productCodeDataService.Save(productCodeDataService.GetAll().First());
		}

		[Test]
		public void Get_First_Product()
		{
			var productDataService = GetTestee();
			var productCodeDataService = Ioc.Resolve<IProductCodeDataService>();

			var productId = productDataService.GetAll().ToList().First().Id;

			productCodeDataService.Save (new ProductCode (){ Value = "555", ProductId = productId });

			var product = productDataService.GetByIdWithChildren(productId);

			Assert.IsTrue (product.ProductCodes.Any (code => code.Value == "555"), "ProductCode is not added");

		}
	}
}

