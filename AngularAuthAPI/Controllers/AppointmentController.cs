using System;
using AngularAuthAPI.Context;
using AngularAuthAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AngularAuthAPI.Controllers
{
	public class AppointmentController
	{
        private readonly AppDbContext _authContext;
        public AppointmentController(AppDbContext appDbContext)
		{
            _authContext = appDbContext;
        }

        [HttpPost("addAppointment")]

        public async Task<IActionResult> AddAppointment([FromBody] Appointment appointmentObj)
        {
            //    if (appointmentObj == null)
            //        return BadRequest();



            return Ok(new
            {
                Message = "Appointment Booked!"
            });


        }

        private IActionResult Ok(object value)
        {
            throw new NotImplementedException();
        }
    }
}

