using System;
using AngularAuthAPI.Context;
using AngularAuthAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthAPI.Controllers
{
	public class AppointmentController : Controller
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
                
            if (appointmentObj == null)
                return BadRequest();

            await _authContext.Appointments.AddAsync(appointmentObj);
            await _authContext.SaveChangesAsync();

            return Ok(new
            {
                Message = "Appointment Booked!"
            });


        }
    }
}

