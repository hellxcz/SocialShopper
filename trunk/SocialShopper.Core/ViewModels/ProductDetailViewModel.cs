using System;
using System.Collections.Generic;
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
        public readonly INCList<ProductCode> ProductCodes = new NCList<ProductCode>(); 
        public readonly INCList<ProductCategory> ProductCategories = new NCList<ProductCategory>(); 

        
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

			ProductCodes.Value = new List<ProductCode>();
			ProductCategories.Value = new List<ProductCategory>();
        }

		public void AddProductCode()
		{
			ProductCodes.Value.Add(
				new ProductCode()
				{
					Value = "new codeee",
					ProductId = product.Id
				}
			);

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
//				ProductCodes.Value.Add(
//					new ProductCode()
//					{
//						Value = codeResult.Code
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
				product.ProductCodes = ProductCodes.Value.ToList();
				product.ProductCategories = ProductCategories.Value.ToList();

                FillProduct();

                _productDataService.InsertWithChildren(product);

                _mvxMessenger.Publish(new EntityMessage<Product>(this, product, EntityChangeEnum.Insert));

                return;
            }

            FillProduct();

            _productDataService.UpdateWithChildren(product);
			_productCodeDataService.UpdateWithChildren(ProductCodes.Value);

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
                ProductCodes.Value = new List<ProductCode>();

                ProductCodes.Value.Add(
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
            ProductCodes.Value = product.ProductCodes;
            ProductCategories.Value = product.ProductCategories;
        }
    }
}