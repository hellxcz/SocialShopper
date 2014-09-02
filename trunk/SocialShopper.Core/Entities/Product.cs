using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using SocialShopper.Core.Entities.Interface;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace SocialShopper.Core.Entities
{
    public class Product : IHaveIntId, IHaveName
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<ProductCode> ProductCodes { get; set; }

		[OneToMany(CascadeOperations = CascadeOperation.CascadeDelete | CascadeOperation.CascadeInsert)]
		public List<ProductPrice> ProductPrices { get; set; }

        [ManyToMany(typeof(Product_ProductCategory))]
        public List<ProductCategory> ProductCategories { get; set; }
    
    }
}