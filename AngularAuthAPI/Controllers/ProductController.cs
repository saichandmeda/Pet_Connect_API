using System;
using System.Configuration;
using AngularAuthAPI.Context;
using AngularAuthAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthAPI.Controllers
{
    [Route("[controller]")]
    //[Authorize]
    public class ProductController : Controller
    {
        private readonly AppDbContext _authContext;

        private readonly string _connectionString;

        //public ProductController(IConfiguration configuration)
        //{
        //    //_authContext = appDbContext;
        //    _connectionString = configuration.GetConnectionString("DefaultConnection");
        //}


        public ProductController(AppDbContext appDbContext)
        {
            _authContext = appDbContext;
        }



        [HttpGet("productList")]

        public async Task<ActionResult<IEnumerable<Product>>> GetProductss()
        {

            return await _authContext.Products.ToListAsync();

        }



        //[HttpGet("getProductList")]

        //public async Task<ActionResult> Products()
        //{

        //    //return await _authContext.Products.ToListAsync();
        //    var products = new List<Product>();

        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();
        //        var command = new SqlCommand("SELECT * FROM products", connection);

        //        using (var reader = await command.ExecuteReaderAsync())
        //        {
        //            while (await reader.ReadAsync())
        //            {
        //                products.Add(new Product
        //                {
        //                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //                    Title = reader.GetString(reader.GetOrdinal("Title")),
        //                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
        //                    Description = reader.GetString(reader.GetOrdinal("Description")),
        //                    Category = reader.GetString(reader.GetOrdinal("Category")),
        //                    Image = reader.GetString(reader.GetOrdinal("Image")),
        //                    Rating = reader.GetDecimal(reader.GetOrdinal("Rating")),
        //                    Count = reader.GetInt32(reader.GetOrdinal("Count"))
        //                });
        //            }
        //        }
        //    }

        //    return Ok(products);
        //}





    }
}

