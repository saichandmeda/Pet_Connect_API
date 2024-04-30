using System;
using System.ComponentModel.DataAnnotations;

namespace AngularAuthAPI.Models
{
	public class Center
	{
		//public Center()
		//{
		//}
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Image { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
    }
}

