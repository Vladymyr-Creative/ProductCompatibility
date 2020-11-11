﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "admin, user")]
        public IActionResult Index()
        {         
            var homeProducts = new HomeViewModel {
                AllProducts = _prodRep.Products
            };
            return View(homeProducts);
        }

        [Authorize(Roles = "admin")]
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
