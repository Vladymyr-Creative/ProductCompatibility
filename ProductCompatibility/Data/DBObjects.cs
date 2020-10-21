using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ProductCompatibility.Data.Interfaces;
using ProductCompatibility.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data
{
    public class DBObjects
    {
        public static void Initial(AppDBContent content)
        {           
            if (!content.Category.Any()) {
                content.Category.AddRange(Categories.Select(c => c.Value));
            }

            if (!content.Product.Any()) {
                content.Product.AddRange(Products.Select(c => c.Value));
            }

            content.SaveChanges();
        }

        private static Dictionary<string, Category> _category;
        public static Dictionary<string, Category> Categories
        {
            get {
                if (_category == null) {
                    var categoryList = new List<Category> {
                        new Category { Name= "Щелочные"},
                        new Category { Name= "Кислотные"},
                        new Category { Name= "Нейтральные"},
                    };

                    _category = new Dictionary<string, Category>();
                    foreach (Category category in categoryList) {
                        _category.Add(category.Name, category);
                    }
                }
                return _category;
            }
            set { }
        }

        private static Dictionary<string, Compatibility> _compatibility;
        public static Dictionary<string, Compatibility> Compatibilities
        {
            get {
                if (_compatibility == null) {
                    var compatibilityList = new List<Compatibility> {
                        new Compatibility { Name= "Perfect"},
                        new Compatibility { Name= "Good"},
                        new Compatibility { Name= "Bad"},
                        new Compatibility { Name= "Awful"}
                    };

                    _compatibility = new Dictionary<string, Compatibility>();
                    foreach (Compatibility compatibility in compatibilityList) {
                        _compatibility.Add(compatibility.Name, compatibility);
                    }
                }
                return _compatibility;
            }
            set { }
        }

        private static Dictionary<string, Product> _product;
        public static Dictionary<string, Product> Products
        {
            get {
                if (_product == null) {
                    var productList = new List<Product> {
                        new Product { 
                            Name="Свекла", 
                            Category= Categories["Щелочные"]
                        },
                        new Product { 
                            Name="Огурцы",
                            Category= Categories["Нейтральные"]
                        },
                        new Product { 
                            Name="Молоко",
                            Category= Categories["Кислотные"]
                        }
                    };

                    _product = new Dictionary<string, Product>();
                    foreach (Product product in productList) {
                        _product.Add(product.Name, product);
                    }
                }
                return _product;
            }
            set { }
        }

    }
}
