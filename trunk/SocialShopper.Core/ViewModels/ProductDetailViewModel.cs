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
        }

        private readonly IMvxMessenger _mvxMessenger;
        private readonly IProductDataService _productDataService;
        private readonly IBarCodeScanner _scanner;

        private Product product;

        public readonly INC<string> Name = new NC<string>("");
        public readonly INC<string> Description = new NC<string>("");
        public readonly INC<string> Code = new NC<string>(""); 
        public readonly INCList<ProductCode> ProductCodes = new NCList<ProductCode>(); 
        public readonly INCList<ProductCategory> ProductCategories = new NCList<ProductCategory>(); 

        
        public ProductDetailViewModel(
			IMvxMessenger mvxMessenger, 
			IProductDataService productDataService, 
			IBarCodeScanner scanner)
        {
            _mvxMessenger = mvxMessenger;
            _productDataService = productDataService;
            _scanner = scanner;
        }

        public async void ShowScan()
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

                Code.Value = codeResult.Code;
            }
            finally
            {
                SuppressVisibleEvent = false;
            }
        }

        public override void OnHide()
        {
            SaveOrUpdateProduct();
        }

        private void SaveOrUpdateProduct()
        {
            if (product == null)
            {
                product = new Product();
                product.ProductCodes = new List<ProductCode>();
                //TODO: add codes

                product.Description = Description.Value;
                product.Name = Name.Value;

                FillProduct();

                _productDataService.InsertWithChildren(product);

                _mvxMessenger.Publish(new EntityMessage<Product>(this, product, EntityChangeEnum.Insert));

                return;
            }

            FillProduct();

            _productDataService.UpdateWithChildren(product);

            _mvxMessenger.Publish(new EntityMessage<Product>(this, product, EntityChangeEnum.Update));
        }

        private void FillProduct()
        {
            product.Description = Description.Value;
            product.Name = Name.Value;
        }

        protected override void InitByMessage(Message message)
        {
            if (message == null || !message.ProductId.HasValue)
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