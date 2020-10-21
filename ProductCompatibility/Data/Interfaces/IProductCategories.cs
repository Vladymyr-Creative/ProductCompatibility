using ProductCompatibility.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data.Interfaces
{
    public interface IProductCategories
    {
        IEnumerable<Category> AllCategories { get;}
    }
}
