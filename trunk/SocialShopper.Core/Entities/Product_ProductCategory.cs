using SQLiteNetExtensions.Attributes;

namespace SocialShopper.Core.Entities
{
    public class Product_ProductCategory
    {
        [ForeignKey(typeof(ProductCategory))]
        public int ProductCategoryId { get; set; }
        
        [ForeignKey(typeof(Product))]
        public int ProductId { get; set; }

    }
}