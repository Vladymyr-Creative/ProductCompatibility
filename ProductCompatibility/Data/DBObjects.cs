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

            if (!content.Compatibility.Any()) {
                content.Compatibility.AddRange(Compatibilities.Select(c => c.Value));
            }

            /*if (!content.ProductsCompatibility.Any()) {
                content.ProductsCompatibility.AddRange(ProductsCompatibilities.Select(c => c));
            }*/

            content.SaveChanges();
        }

        private static Dictionary<string, Category> _categories;
        public static Dictionary<string, Category> Categories
        {
            get {
                if (_categories == null) {
                    var categoryList = new List<Category> {
                        new Category { Name= "Alkaline"},
                        new Category { Name= "Acidic"},
                        new Category { Name= "Neutral"},
                    };

                    _categories = new Dictionary<string, Category>();
                    foreach (Category category in categoryList) {
                        _categories.Add(category.Name, category);
                    }
                }
                return _categories;
            }
            set { }
        }

        private static Dictionary<string, Product> _products;
        public static Dictionary<string, Product> Products
        {
            get {
                if (_products == null) {
                    var productList = new List<Product> {
                        new Product {
                            Name="Beet",
                            Category= Categories["Alkaline"]
                        },
                        new Product {
                            Name="Cucumber",
                            Category= Categories["Neutral"]
                        },
                        new Product {
                            Name="Milk",
                            Category= Categories["Acidic"]
                        }
                    };

                    _products = new Dictionary<string, Product>();
                    foreach (Product product in productList) {
                        _products.Add(product.Name, product);
                    }
                }
                return _products;
            }
            set { }
        }

        private static Dictionary<string, Compatibility> _compatibilities;
        public static Dictionary<string, Compatibility> Compatibilities
        {
            get {
                if (_compatibilities == null) {
                    var compatibilityList = new List<Compatibility> {
                        new Compatibility { Name= "Perfect"},
                        new Compatibility { Name= "Good"},
                        new Compatibility { Name= "Bad"},
                        new Compatibility { Name= "Awful"}
                    };

                    _compatibilities = new Dictionary<string, Compatibility>();
                    foreach (Compatibility compatibility in compatibilityList) {
                        _compatibilities.Add(compatibility.Name, compatibility);
                    }
                }
                return _compatibilities;
            }
            set { }
        }

        private static List<ProductsCompatibility> _productsCompatibilities;
        public static List<ProductsCompatibility> ProductsCompatibilities
        {
            get {
                /*if (_productsCompatibilities == null) {
                    var productsCompatibilitiesList = new List<ProductsCompatibility> {
                        new ProductsCompatibility (){
                            Product1ID=Products["Beet"],
                            Product2ID=1,
                            CompatibilityID=1,
                            Product1=Products["Beet"],
                            Product2=Products["Beet"] ,
                            Compatibility=Compatibilities["Awful"]
                        },
                        new ProductsCompatibility {
                            Name="Cucumber",
                            Category= Categories["Neutral"]
                        },
                        new ProductsCompatibility {
                            Name="Milk",
                            Category= Categories["Acidic"]
                        }
                    };

                    _productsCompatibilities = new List<ProductsCompatibility>();
                    foreach (ProductsCompatibility productsCompatibility in productsCompatibilitiesList) {
                        _productsCompatibilities.Add(productsCompatibility);
                    }
                }*/
                return _productsCompatibilities;
            }
            set { }
        }
    }
}
