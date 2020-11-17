using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCompatibility.Data.Interfaces;
using ProductCompatibility.Data.Models;

namespace ProductCompatibility.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> _repoProd;

        public ProductsController(IRepository<Product> repoProd)
        {
            _repoProd = repoProd;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            var products = await _repoProd.GetAllAsync();            
            return products;
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> Get(int id)
        {
            return new JsonResult(await _repoProd.FindByIdAsync(id));
        }
    }
}
