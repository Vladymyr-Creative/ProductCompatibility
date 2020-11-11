using ProductCompatibility.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data.Interfaces
{
    public interface IAllProducts
    {
        IEnumerable<Product> All { get; }
        Product FindById(int id);
        void Create(Product product);
        void Update(Product product);
    }
}
