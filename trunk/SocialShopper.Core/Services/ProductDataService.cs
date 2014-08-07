using System.Linq;
using System.Threading;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using SocialShopper.Core.Entities;
using System.Collections.Generic;
using SocialShopper.Core.Services.Interface;
using SQLiteNetExtensions.Extensions;

namespace SocialShopper.Core.Services
{
    public class ProductDataService : DataServiceBase<Product, int>, IProductDataService
    {
        public ProductDataService(ISQLiteConnectionFactory factory) 
            : base(factory)
        {
            
        }
    }
}