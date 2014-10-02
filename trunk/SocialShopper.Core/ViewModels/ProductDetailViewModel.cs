using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using Acr.MvvmCross.Plugins.BarCodeScanner;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.FieldBinding;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using SocialShopper.Core.Entities;
using SocialShopper.Core.Messages;
using SocialShopper.Core.Services;
using SocialShopper.Core.Services.Interface;
using System.Threading.Tasks;
using SocialShopper.Core.Core;
using System.Collections.ObjectModel;

namespace SocialShopper.Core.ViewModels
{

    public class ProductDetailViewModel : BaseViewModel<ProductDetailViewModel.Message>
    {
        public class Message : InitMessage
        {
            public int? ProductId { get; set; } 
            public string ProductCode { get; set; }
        }

        private readonly IMvxMessenger _mvxMessenger;
        private readonly IProductDataService _productDataService;
		private readonly IProductCodeDataService _productCodeDataService;
        private readonly IBarCodeScanner _scanner;

        private Product product;

        public readonly INC<string> Name = new NC<string>("");
        public readonly INC<string> Description = new NC<string>("");
		public ObservableCollection<ProductCode> ProductCodes;// = new MyNCList<ProductCode>(); 
		public ObservableCollection<ProductCategory> ProductCategories;// = new MyNCList<ProductCategory>(); 
		public ObservableCollection<ProductPrice> ProductPrices;
		public readonly INC<float> CurrentProductPrice = new NC<float> (0);

        public ProductDetailViewModel(
			IMvxMessenger mvxMessenger, 
			IProductDataService productDataService, 
			IProductCodeDataService productCodeDataService,
			IBarCodeScanner scanner)
        {
            _mvxMessenger = mvxMessenger;
            _productDataService = productDataService;
			_productCodeDataService = productCodeDataService;
            _scanner = scanner;

			ProductCodes = new ObservableCollection<ProductCode>();
			ProductCategories = new ObservableCollection<ProductCategory>();
			ProductPrices = new ObservableCollection<ProductPrice> ();

			//CurrentProductPrice.Changed += CurrentProductPrice_Changed;

        }

		public void AddProductPrice()
		{
			ProductPrices.Add (new ProductPrice (){ CreationDate = DateTime.Now, Value = CurrentProductPrice.Value });
		}

		public void AddProductCode()
		{
			var productCode = new ProductCode () {
				Value = "new codeee",
				ProductId = product != null ? product.Id : 0
			};

			ProductCodes.Add (productCode);
		}

//		public async void AddProductCode()
//        {
//			try
//            {
//                SuppressVisibleEvent = true;
//
//                _scanner.Configuration.AutoRotate = false;
//
//				var codeResult = await _scanner.ReadAsync();
//
//                if (!codeResult.Success)
//                {
//                    return;
//                }
//
//				ProductCodes.Add(
//					new ProductCode()
//					{
//						Value = codeResult.Code,
//						ProductId = product.Id
//					}
//				);
//            }
//            finally
//            {
//                SuppressVisibleEvent = false;
//            }
//        }

        public override void OnHide()
        {
            SaveOrUpdateProduct();
        }

        private void SaveOrUpdateProduct()
        {
            if (product == null)
            {
                product = new Product();
				product.ProductCodes = ProductCodes.ToList();
				product.ProductCategories = ProductCategories.ToList();
				product.ProductPrices = ProductPrices.ToList ();

                FillProduct();

                _productDataService.SaveWithChildren(product);

                _mvxMessenger.Publish(new EntityMessage<Product>(this, product, EntityChangeEnum.Insert));

                return;
            }

            FillProduct();

            _productDataService.SaveWithChildren(product);
			_productCodeDataService.Save(ProductCodes);

            _mvxMessenger.Publish(new EntityMessage<Product>(this, product, EntityChangeEnum.Update));
        }

        private void FillProduct()
        {
            product.Description = Description.Value;
            product.Name = Name.Value;
        }

        protected override void InitByMessage(Message message)
        {
            if (message == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(message.ProductCode))
            {
				ProductCodes = new ObservableCollection<ProductCode>();

                ProductCodes.Add(
                    new ProductCode()
                    {
                        Value = message.ProductCode
                    });

                return;
            }

            if (!message.ProductId.HasValue)
            {
                return;
            }

            this.product = _productDataService.GetByIdWithChildren(message.ProductId.Value);

            Name.Value = product.Name;
            Description.Value = product.Description;
			ProductCodes = new ObservableCollection<ProductCode> (product.ProductCodes);
			ProductCategories = new ObservableCollection<ProductCategory>(product.ProductCategories);
			ProductPrices = new ObservableCollection<ProductPrice> (product.ProductPrices);
			CurrentProductPrice.Value = product.ProductPrices.LastOrDefault ().Value;
        }
    }
}