﻿using ProductCompatibility.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> AllProducts { get; set; }
        public IEnumerable<ProductsCompatibility> AllProductsCompatibilities { get; set; }

    }
}
