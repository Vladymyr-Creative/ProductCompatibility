using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductCompatibility.Data.Interfaces;
using ProductCompatibility.Data.Models;
using ProductCompatibility.ViewModels;

namespace ProductCompatibility.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAllProducts _prodRep;        

        public HomeController(IAllProducts prodRep)
        {
            _prodRep = prodRep;            
        }
        public ViewResult Index()
        {
            var homeProducts = new HomeViewModel {
                AllProducts = _prodRep.Products
            };
            return View(homeProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
