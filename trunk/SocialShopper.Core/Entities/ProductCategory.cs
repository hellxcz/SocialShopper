using System.Collections.Generic;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using SocialShopper.Core.Entities.Interface;
using SQLiteNetExtensions.Attributes;

namespace SocialShopper.Core.Entities
{
    public class ProductCategory : IHaveIntId, IHaveName
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        [ManyToMany(typeof(Product_ProductCategory))]
        public List<Product> Products { get; set; }
    }
}