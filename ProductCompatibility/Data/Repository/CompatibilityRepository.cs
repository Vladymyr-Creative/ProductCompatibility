using ProductCompatibility.Data.Interfaces;
using ProductCompatibility.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data.Repository
{
    public class CompatibilityRepository : IAllCompatibilities
    {
        private readonly AppDBContent _appDBContent;

        public CompatibilityRepository(AppDBContent appDBContent)
        {
            _appDBContent = appDBContent;
        }
        public IEnumerable<Compatibility> All => _appDBContent.Compatibility;                
    }
}
