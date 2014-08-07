using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using SocialShopper.Core.Entities.Interface;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace SocialShopper.Core.Entities
{

	public class ProductPrice : IHaveIntId, IHaveFloatValue
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public float Value { get;set; }

		[ForeignKey(typeof(Product))]
		public int ProductId { get; set; }
	}
}