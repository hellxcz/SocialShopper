using System;
using System.Collections.ObjectModel;
using SocialShopper.Core.Entities;
using SocialShopper.Core.Services.Interface;

namespace SocialShopper.Core.ViewModels
{
    public class ProductCategoryListViewModel : BaseViewModel
    {
        private readonly IProductCategoryDataService _productCategoryDataService;

        public ObservableCollection<ProductCategory> ProductCategories;

        public ProductCategoryListViewModel(IProductCategoryDataService productCategoryDataService)
        {
            _productCategoryDataService = productCategoryDataService;
            ProductCategories = new ObservableCollection<ProductCategory>(
                _productCategoryDataService.GetAll());
        }

        public void NavigateTo(ProductCategory productCategory)
        {
            ShowViewModel<ProductListViewModel>(new ProductListViewModel.Message()
            {
                ProductCategoryId = productCategory.Id
            });
        }
    }
}