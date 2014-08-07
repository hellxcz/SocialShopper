using System.Linq;
using Cirrious.MvvmCross.FieldBinding;
using Cirrious.MvvmCross.Plugins.Messenger;
using SocialShopper.Core.Entities;
using SocialShopper.Core.Services.Interface;
using SocialShopper.Core.Messages;
using Acr.MvvmCross.Plugins.BarCodeScanner;

namespace SocialShopper.Core.ViewModels
{
    public class ProductSearchViewModel : BaseViewModel
    {
        private readonly IProductCodeDataService _productCodeDataService;
        private readonly IProductDataService _productDataService;
        private readonly IMvxMessenger _messenger;
		private readonly IBarCodeScanner _scanner;

        public INC<string> ProductCode = new NC<string>();
        public INCList<Product> Products = new NCList<Product>();


        public ProductSearchViewModel(
            IProductCodeDataService productCodeDataService, 
            IProductDataService productDataService,
            IMvxMessenger messenger,
			IBarCodeScanner scanner)
        {
			_scanner = scanner;
            _productCodeDataService = productCodeDataService;
            _productDataService = productDataService;
            _messenger = messenger;

            _messenger.Subscribe<EntityMessage<Product>>(ProductChange);

        }
        
        public void Search()
        {
            var productIds = _productCodeDataService.Filter(code => code.Value.Contains(ProductCode.Value))
                .Select(code => code.ProductId);

            Products.Value = _productDataService.GetByIds(productIds);
        }

		public async void Scan()
		{
			try
			{
				SuppressVisibleEvent = true;

				//var scan = Mvx.Resolve<IBarCodeScanner>();

				_scanner.Configuration.AutoRotate = false;

				var codeResult = await _scanner.ReadAsync();

				if (!codeResult.Success)
				{
					return;
				}

				ProductCode.Value = codeResult.Code;
			}
			finally
			{
				SuppressVisibleEvent = false;
			}
		}

        public void NavigateTo(Product product)
        {
            ShowViewModel<ProductDetailViewModel>(
                new ProductDetailViewModel.Message()
                {
                    ProductId = product.Id
                });
        }

        private void ProductChange(EntityMessage<Product> message)
        {
            var changedProduct = message.Entity;

            if (changedProduct == null)
            {
                return;
            }

            var productList = Products.Value;

            var currentEntity = productList
                        .SingleOrDefault(product => product.Id == changedProduct.Id);

            switch (message.EntityChange)
            {
                case EntityChangeEnum.Insert:
                    productList.Add(message.Entity);

                    break;
                case EntityChangeEnum.Update:
                    if (currentEntity == null)
                    {
                        break;
                    }

                    productList[productList.IndexOf(currentEntity)] = changedProduct;

                    break;
                case EntityChangeEnum.Delete:
                    if (currentEntity == null)
                    {
                        break;
                    }
                    productList.Remove(currentEntity);

                    break;
            }
        }
    }
}