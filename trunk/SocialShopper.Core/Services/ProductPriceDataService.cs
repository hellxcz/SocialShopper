using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using SocialShopper.Core.Entities;
using SocialShopper.Core.Services.Interface;

namespace SocialShopper.Core.Services
{

	public class ProductPriceDataService
		: DataServiceBase<ProductPrice, int>, IProductPriceDataService
	{
		public ProductPriceDataService (ISQLiteConnectionFactory factory)
			: base(factory)
		{
			
		}
	}
}