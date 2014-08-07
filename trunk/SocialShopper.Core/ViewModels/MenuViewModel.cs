using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.ViewModels;
using SocialShopper.Core.Entities;
using Cirrious.MvvmCross.FieldBinding;

namespace SocialShopper.Core.ViewModels
{
    public class MenuViewModel
        : MvxViewModel
    {
        public INCList<MenuEntry> MenuEntries = new NCList<MenuEntry>();
        
        public MenuViewModel()
        {
            MenuEntries.Value = new List<MenuEntry>()
            {
                new MenuEntry() {Name = "Scan", ViewModelT = typeof (ScanViewModel)},
                new MenuEntry() {Name = "Categories", ViewModelT = typeof (ProductCategoryListViewModel)},
                new MenuEntry() {Name = "Products", ViewModelT = typeof (ProductListViewModel)},
                new MenuEntry() {Name = "Product search", ViewModelT = typeof (ProductSearchViewModel)},
            };
        }

        public void NavigateTo(MenuEntry item)
        {
            ShowViewModel(item.ViewModelT);
        }
    }
}