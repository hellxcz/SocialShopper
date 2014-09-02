using System;
using System.Collections.Generic;
using SocialShopper.Core.Entities;
using SocialShopper.Core.Services.Interface;

namespace SocialShopper.Core.Services
{
    public class DataFillerService : IDataFillerService
    {
        private readonly IProductCategoryDataService _productCategoryDataService;
        private readonly IProductDataService _productDataService;
        private readonly IProductCodeDataService _productCodeDataService;

        private List<ProductCategory> productCategories;

        public DataFillerService(
            IProductCategoryDataService productCategoryDataService,
            IProductDataService productDataService,
            IProductCodeDataService productCodeDataService)
        {
            _productCategoryDataService = productCategoryDataService;
            _productDataService = productDataService;
            _productCodeDataService = productCodeDataService;
        }

        public void Init()
        {
            InitProductCategories();

            InitProducts();
        }

        private void InitProductCategories()
        {
            _productCategoryDataService.DeleteAll();

            productCategories = new List<ProductCategory>
            {
                new ProductCategory()
                {
                    Name = "Drinks",
                    Description = "just drinks"
                },
                new ProductCategory()
                {
                    Name = "Sweets",
                    Description = "just sweets"
                },
                new ProductCategory()
                {
                    Name = "Instant Food",
                    Description = "bleh fast to prepare"
                },
                new ProductCategory()
                {
                    Name = "Meat",
                    Description = "Raw meat"
                },
                new ProductCategory()
                {
                    Name = "Milk products",
                    Description = "cheese, milk, yoghurts"
                },
            };

			_productCategoryDataService.Save(productCategories);
        }

        private void InitProducts()
        {
            _productCodeDataService.DeleteAll();
            _productDataService.DeleteAll();

            var products = new List<Product>()
            {
                new Product()
                {
                    ProductCategories = new List<ProductCategory>()
                    {
                        productCategories[1]
                    },
                    ProductCodes = new List<ProductCode> 
                    {
                        new ProductCode() {Value = "123"},
                        new ProductCode() {Value = "321"},
					},
					Description = @"some really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really really looooong description",                    
					Name = "Tatranky",
					ProductPrices = new List<ProductPrice>
					{
						new ProductPrice() { Value = 10, CreationDate = DateTime.Now - TimeSpan.FromHours(1)},
						new ProductPrice() { Value = 12, CreationDate = DateTime.Now - TimeSpan.FromHours(2)},
						new ProductPrice() { Value =  9, CreationDate = DateTime.Now - TimeSpan.FromHours(3)},
						new ProductPrice() { Value = 10, CreationDate = DateTime.Now - TimeSpan.FromHours(4)},
						new ProductPrice() { Value = 11, CreationDate = DateTime.Now - TimeSpan.FromHours(5)},
						new ProductPrice() { Value = 13, CreationDate = DateTime.Now - TimeSpan.FromHours(6)},
						new ProductPrice() { Value = 14, CreationDate = DateTime.Now - TimeSpan.FromHours(7)},
						new ProductPrice() { Value = 15, CreationDate = DateTime.Now - TimeSpan.FromHours(8)},
						new ProductPrice() { Value = 20, CreationDate = DateTime.Now - TimeSpan.FromHours(9)},
						new ProductPrice() { Value = 10, CreationDate = DateTime.Now - TimeSpan.FromHours(10)},
						new ProductPrice() { Value =  8, CreationDate = DateTime.Now - TimeSpan.FromHours(11)},
						new ProductPrice() { Value = 10, CreationDate = DateTime.Now },
					}
                },
                new Product()
                {
                    ProductCategories = new List<ProductCategory>()
                    {
                        productCategories[1]
                    },
                    ProductCodes = new List<ProductCode> {new ProductCode() {Value = "123"}},
                    Description = "some",
                    Name = "Horicke trubicky",
                },
                new Product()
                {
                    ProductCategories = new List<ProductCategory>()
                    {
                        productCategories[0]
                    },
                    ProductCodes = new List<ProductCode> {new ProductCode() {Value = "123"}},
                    Description = "some",
                    Name = "Cervene vino"
                },
                new Product()
                {
                    ProductCategories = new List<ProductCategory>()
                    {
                        productCategories[0]
                    },
                    ProductCodes = new List<ProductCode> {new ProductCode() {Value = "123"}},
                    Description = "some",
                    Name = "Bile vino"
                },
            };

            _productDataService.SaveWithChildren(products);
        }
    }
}