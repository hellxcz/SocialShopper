using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using SocialShopper.Core.Entities.Interface;
using SQLiteNetExtensions.Attributes;
namespace SocialShopper.Core.Entities
{
    public class ProductCode : IHaveStringValue, IHaveIntId
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Value { get; set; }

        [ForeignKey(typeof(Product))]
        public int ProductId { get; set; }
    }
}