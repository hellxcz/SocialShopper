using SocialShopper.Core.Services.Interface;

namespace SocialShopper.Core.Services
{
    public class DataInitializerService : IDataInitializerService
    {
        private readonly IProduct_ProductCategoryDataService _productProductCategoryDataService;
        private readonly IProductCategoryDataService _productCategoryDataService;
        private readonly IProductDataService _productDataService;
        private readonly IProductCodeDataService _productCodeDataService;

        public DataInitializerService(
            IProduct_ProductCategoryDataService productProductCategoryDataService,
            IProductCategoryDataService productCategoryDataService,
            IProductDataService productDataService,
            IProductCodeDataService productCodeDataService)
        {
            _productProductCategoryDataService = productProductCategoryDataService;
            _productCategoryDataService = productCategoryDataService;
            _productDataService = productDataService;
            _productCodeDataService = productCodeDataService;
        }

        public virtual void Init()
        {
            _productProductCategoryDataService.Init();
            _productCategoryDataService.Init();
            _productDataService.Init();
            _productCodeDataService.Init();
        }
    }
}