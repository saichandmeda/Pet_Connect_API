using System;
using System.IdentityModel.Tokens.Jwt;
using AngularAuthAPI.Context;
using AngularAuthAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthAPI.Controllers
{
    [Route("[controller]")]
    [Authorize]
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
            var user2 = "";
            string authHeader = Request.Headers.Authorization;
            authHeader = authHeader.Replace("Bearer ", string.Empty);
            if (authHeader != null)
            {
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken token = handler.ReadJwtToken(authHeader);
                if (token != null)
                {
                   
                   user2= token.Claims.First(c => c.Type =="email").Value;
                }
            }

            if (appointmentObj == null)
                return BadRequest();

            //var user = await _authContext.Appointments
            //    .FirstOrDefaultAsync(x => x.Username == appointmentObj.Username);

            if (await CheckSameSameAsync(user2,appointmentObj.PetClinic,
                                                    appointmentObj.AppointmentTime,appointmentObj.AppointmentDate))
                return BadRequest(new { Message = "You already have an appointment here at this time" });
            if (await CheckDiffSameAsync(appointmentObj.PetClinic,
                                                    appointmentObj.AppointmentTime, appointmentObj.AppointmentDate))
                return BadRequest(new { Message = "Slot is full, Try a different slot" });
            if (await CheckSameDiffAsync(user2,appointmentObj.AppointmentTime,
                                                    appointmentObj.AppointmentDate))
                return BadRequest(new { Message = "You already have an appointment in a different hospital at this time" });



            appointmentObj.Username = user2;
            await _authContext.Appointments.AddAsync(appointmentObj);
            await _authContext.SaveChangesAsync();

            return Ok(new
            {
                Message = "Appointment Booked!"
            });


        }


        [HttpGet("getAppointmentList")]

        public async Task<ActionResult> Appointments()
        {

            var user2 = "";
            string authHeader = Request.Headers.Authorization;
            authHeader = authHeader.Replace("Bearer ", string.Empty);
            if (authHeader != null)
            {
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken token = handler.ReadJwtToken(authHeader);
                if (token != null)
                {

                    user2 = token.Claims.First(c => c.Type == "email").Value;
                }
            }


            var items = await _authContext.Appointments
            .Where(i => i.Username.Contains(user2)) // Adjust the condition as needed
            .Select(i => new
            {
                PetClinic = i.PetClinic,
                //AppointmentDate = i.AppointmentDate,
                AppointmentTime = i.AppointmentTime // Assume there's a Category property
            })
            .ToListAsync();

            return Ok(items);


            //return await _authContext.Appointments.ToListAsync();

        }


        private Task<bool> CheckSameSameAsync(string username, string petclinic, string time, DateOnly date)
            => _authContext.Appointments.AnyAsync(x => x.Username == username && x.PetClinic == petclinic &&
                                     x.AppointmentTime == time && x.AppointmentDate == date );
        private Task<bool> CheckDiffSameAsync(string petclinic, string time, DateOnly date)
            => _authContext.Appointments.AnyAsync(x =>x.PetClinic == petclinic &&
                                     x.AppointmentTime == time && x.AppointmentDate == date);
        private Task<bool> CheckSameDiffAsync(string username, string time, DateOnly date)
            => _authContext.Appointments.AnyAsync(x => x.Username == username &&
                                     x.AppointmentTime == time && x.AppointmentDate == date);
    }
}

