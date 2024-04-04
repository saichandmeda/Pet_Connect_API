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
        public string Username { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string City { get; set; }
        public int PetAge { get; set; }
        public string PetClinic { get; set; }
        public string FirstVisit { get; set; }
        public string Gender { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string Narration { get; set; }
    }
}

