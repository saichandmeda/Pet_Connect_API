using System;
using System.ComponentModel.DataAnnotations;

namespace AngularAuthAPI.Models
{
	public class Hospital
	{
		//public Hospital()
		//{
		//}
        [Key]
        public int Id { get; set; }
        public string Hospital_Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
    }
}

