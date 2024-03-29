using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularAuthAPI.Models
{
	public class Appointment
	{
		//public Appointment()
		//{
		//}
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int MobileNo { get; set; }
        public string City { get; set; }
        public int age { get; set; }
        public string PetClinic { get; set; }
        public bool FirstVisit { get; set; }
        public string Gender { get; set; }
        //[DataType(DataType.Date)]
        //[Column(TypeName = "Date")]
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
    }
}

