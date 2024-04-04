using System;
using AngularAuthAPI.Context;
using AngularAuthAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AngularAuthAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class HospitalController : Controller
    {
        private readonly AppDbContext _authContext;
        public HospitalController(AppDbContext appDbContext)
		{
            _authContext = appDbContext;
        }

        [HttpGet("hospitalList")]

        public async Task<ActionResult<IEnumerable<Hospital>>> GetHospitals()
        {

            return await _authContext.Hospitals.ToListAsync();

        }
    }
}

