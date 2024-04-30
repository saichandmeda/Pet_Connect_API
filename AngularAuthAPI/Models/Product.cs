using System;
using System.ComponentModel.DataAnnotations;

namespace AngularAuthAPI.Models
{
	public class Product
	{
		//public Product()
		//{
		//}
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public Decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public Decimal Rating { get; set; }
        public int Count { get; set; }
        //public Rating Rating { get; set; }
    }
}

