using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data.Models
{
    public class ProductsCompatibility
    {
        private readonly List<string> _compatibilityList = new List<string> { "Perfect", "Good", "Bad", "Awful" };
        private string _compatibility;

        public ProductsCompatibility(int productId1, int productId2, string prodCompatibility)
        {         
            ProductId1 = productId1;
            ProductId2 = productId2;
            ProdCompatibility = prodCompatibility;
        }

        public int ProductId1 { get; set; }
        public int ProductId2 { get; set; }
        public string ProdCompatibility {
            get { 
                return _compatibility; 
            } 
            set {
                if (_compatibilityList.Contains(value)) {
                    _compatibility = value;
                } else {
                    _compatibility = "default";
                }
            }
        }
    }
}
