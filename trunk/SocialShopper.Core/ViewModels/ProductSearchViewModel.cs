using System.Linq;
using Cirrious.MvvmCross.FieldBinding;
using Cirrious.MvvmCross.Plugins.Messenger;
using SocialShopper.Core.Entities;
using SocialShopper.Core.Services.Interface;
using SocialShopper.Core.Messages;
using Acr.MvvmCross.Plugins.BarCodeScanner;
using System;
using Cirrious.CrossCore;
using System.Collections.Generic;

namespace SocialShopper.Core.ViewModels
{
    public class ProductSearchViewModel : BaseViewModel
    {
        private readonly IProductCodeDataService _productCodeDataService;
        private readonly IProductDataService _productDataService;
        private readonly IMvxMessenger _messenger;
		private readonly IBarCodeScanner _scanner;

		public INC<string> ProductCode = new NC<string>();
		public INCList<Product> Products = new NCList<Product>(new List<Product>()) ;


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
                .Select(code => code.ProductId).ToList();

            if (productIds.Count == 1)
            {
                var product = _productDataService.GetById(productIds.First());

                NavigateTo(product);                
            }
            else if (productIds.Count == 0)
            {
                // product does not exists yet, should be created
                // 

                NavigateTo(ProductCode.Value);
            }
            else
            {
                Products.Value = _productDataService.GetByIds(productIds);
            }
        }

		public async void Scan()
		{
			try
			{
				SuppressVisibleEvent = true;

				var _scanner = Mvx.Resolve<IBarCodeScanner>();

				_scanner.Configuration.AutoRotate = false;

				var codeResult = await _scanner.ReadAsync();

				if (!codeResult.Success)
				{
					return;
				}

				ProductCode.Value = codeResult.Code;

                Search();
			}

			catch(Exception e) 
			{

			}
			finally
			{
				SuppressVisibleEvent = false;
			}
		}

        public void NavigateTo(Product product)
        {
            NavigateTo(new ProductDetailViewModel.Message()
            {
                ProductId = product.Id
            });
        }

        protected void NavigateTo(string productCode)
        {
            NavigateTo(new ProductDetailViewModel.Message()
            {
                ProductCode = productCode
            });
        }

        protected void NavigateTo(ProductDetailViewModel.Message message)
        {
            ShowViewModel<ProductDetailViewModel>(message);
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