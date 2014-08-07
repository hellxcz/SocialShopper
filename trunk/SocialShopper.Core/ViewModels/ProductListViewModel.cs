// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProductViewMode type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using SocialShopper.Core.Entities;
using System.Collections.Generic;
using SocialShopper.Core.Messages;
using SocialShopper.Core.Services;
using Cirrious.MvvmCross.FieldBinding;
using SocialShopper.Core.Services.Interface;

namespace SocialShopper.Core.ViewModels
{
    /// <summary>
    /// Define the ProductViewMode type.
    /// </summary>
    public class ProductListViewModel : BaseViewModel<ProductListViewModel.Message>
    {
        public class Message : InitMessage
        {
            public int? ProductCategoryId { get; set; }
        }

        private readonly IProductDataService _productDataService;
        private readonly IProductCategoryDataService _productCategoryDataService;
        private readonly IMvxMessenger _messenger;
        
        public ObservableCollection<Product> Products;

        public ProductListViewModel(
            IProductDataService productDataService, 
            IProductCategoryDataService productCategoryDataService,
            IMvxMessenger messenger)
        {
            _productDataService = productDataService;
            _productCategoryDataService = productCategoryDataService;
            _messenger = messenger;
            
            _messenger.Subscribe<EntityMessage<Product>>(ProductChange);
        }
        
        public void NavigateTo(Product product)
        {
            ShowViewModel<ProductDetailViewModel>(
                new ProductDetailViewModel.Message()
                {
                    ProductId = product.Id
                });
        }

        public void AddNewProduct()
        {
            ShowViewModel<ProductDetailViewModel>();
        }

        protected override void InitByMessage(Message message)
        {
            if (message == null || !message.ProductCategoryId.HasValue)
            {
                Products = new ObservableCollection<Product>(
                    _productDataService.GetAll()
                );

                return;
            }

            var productCategory = _productCategoryDataService.GetByIdWithChildren(message.ProductCategoryId.Value);
            Products = new ObservableCollection<Product>(productCategory.Products);
        }

        private void ProductChange(EntityMessage<Product> message)
        {
            var changedProduct = message.Entity;

            if (changedProduct == null)
            {
                return;
            }

            var productList = Products;

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

        //public void Init(Message message)
        //{
        //    if (message == null || !message.ProductCategoryId.HasValue)
        //    {
        //        Products = new ObservableCollection<Product>(
        //            _productDataService.GetAllWithChildren()
        //        );

        //        return;
        //    }

        //    var productCategory = _productCategoryDataService.GetByIdWithChildren(message.ProductCategoryId.Value);
        //    Products = new ObservableCollection<Product>(productCategory.Products);
        //}

        public void Filter()
        {
            
        }
    }
}
