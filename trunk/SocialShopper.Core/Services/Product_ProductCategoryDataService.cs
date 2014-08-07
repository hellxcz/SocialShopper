using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using SocialShopper.Core.Entities;
using SocialShopper.Core.Services.Interface;

namespace SocialShopper.Core.Services
{
    public class Product_ProductCategoryDataService 
        : DataServiceBase<Product_ProductCategory>, IProduct_ProductCategoryDataService
    {
        public Product_ProductCategoryDataService(ISQLiteConnectionFactory factory)
            : base(factory)
        {
            
        }
    }
}