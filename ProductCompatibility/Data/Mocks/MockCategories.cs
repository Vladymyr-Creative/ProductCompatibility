using ProductCompatibility.Data.Interfaces;
using ProductCompatibility.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Mocks
{
    public class MockCategories : IProductCategories
    {
        public IEnumerable<Category> AllCategories {
            get {
                return new List<Category> {
                    new Category { Name= "Щелочные"},
                    new Category { Name= "Кислотные"},
                    new Category { Name= "Нейтральные"},
                };
            }          
        }
    }
}
