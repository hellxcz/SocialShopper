using SocialShopper.Core.Services.Interface;
using System.Collections.Generic;

namespace SocialShopper.Core.Services
{
    public class DataInitializerService : IDataInitializerService
    {
		private readonly IList<IHaveInit> ToInit;

        public DataInitializerService(
            IProduct_ProductCategoryDataService productProductCategoryDataService,
            IProductCategoryDataService productCategoryDataService,
            IProductDataService productDataService,
            IProductCodeDataService productCodeDataService,
			IProductPriceDataService productPriceDataService)
        {
			ToInit = new List<IHaveInit> {
				productProductCategoryDataService,
				productCategoryDataService,
				productDataService,
				productCodeDataService,
				productPriceDataService
			};
        }

        public virtual void Init()
        {
			foreach (var item in ToInit) 
			{
				item.Init();
			}
        }
    }
}