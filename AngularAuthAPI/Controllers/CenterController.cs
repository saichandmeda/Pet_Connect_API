using System;
using AngularAuthAPI.Context;
using AngularAuthAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthAPI.Controllers
{
    [Route("[controller]")]
    public class CenterController : Controller
	{

        private readonly AppDbContext _authContext;

        public CenterController(AppDbContext appDbContext)
		{
            _authContext = appDbContext;
        }



        [HttpGet("centerList")]

        public async Task<ActionResult<IEnumerable<Center>>> GetCenters()
        {

            return await _authContext.Centers.ToListAsync();

        }


        [HttpGet("petList/{centerName}")]

        public async Task<IActionResult> GetPets(string centerName)
        {
            var items = await _authContext.Pets
            .Where(i => i.Center == centerName) // Adjust the condition as needed
            .Select(i => new
            {
                Id = i.Id,
                Breed = i.Breed,
                Age = i.Age,
                Color = i.Color,
                Center = i.Center,
                Image = i.Image
                
            })
            .ToListAsync();
            return Ok(items);

        }
    }
}

