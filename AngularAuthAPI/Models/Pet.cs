using System;
using System.ComponentModel.DataAnnotations;

namespace AngularAuthAPI.Models
{
	public class Pet
	{
		//public Pet()
		//{
		//}
        [Key]
        public int Id { get; set; }
        public string Center { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
    }
}

