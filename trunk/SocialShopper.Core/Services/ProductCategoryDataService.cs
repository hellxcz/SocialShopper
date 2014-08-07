using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using SocialShopper.Core.Entities;
using SocialShopper.Core.Services.Interface;

namespace SocialShopper.Core.Services
{
    public class ProductCategoryDataService
        : DataServiceBase<ProductCategory, int>, IProductCategoryDataService
    {
        public ProductCategoryDataService(ISQLiteConnectionFactory factory)
            : base(factory)
        {

        }
    }
}