using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using SocialShopper.Core.Entities;
using SocialShopper.Core.Services.Interface;

namespace SocialShopper.Core.Services
{
    public class ProductCodeDataService 
        : DataServiceBase<ProductCode, int>, IProductCodeDataService
    {
        public ProductCodeDataService(ISQLiteConnectionFactory factory) 
            : base(factory)
        {
            
        }
    }
}