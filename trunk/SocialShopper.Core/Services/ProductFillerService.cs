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

            _productCategoryDataService.Insert(productCategories);
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
                        new ProductCode() {Value = "313"},
                        new ProductCode() {Value = "324"},
						new ProductCode() {Value = "327"},
						new ProductCode() {Value = "328"},
						new ProductCode() {Value = "329"},
						new ProductCode() {Value = "330"},
						new ProductCode() {Value = "331"},
						new ProductCode() {Value = "332"},
						new ProductCode() {Value = "343"},
						new ProductCode() {Value = "354"},
						new ProductCode() {Value = "365"},
                        new ProductCode() {Value = "312"},
                    },
                    Description = "some",
                    Name = "Tatranky"
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

            _productDataService.InsertWithChildren(products);
        }
    }
}