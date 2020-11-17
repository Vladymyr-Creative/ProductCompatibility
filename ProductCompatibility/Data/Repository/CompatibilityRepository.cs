using Microsoft.EntityFrameworkCore;
using ProductCompatibility.Data.Interfaces;
using ProductCompatibility.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data.Repository
{
    public class CompatibilityRepository : IRepository<Compatibility>
    {
        private readonly AppDBContent _appDBContent;

        public CompatibilityRepository(AppDBContent appDBContent)
        {
            _appDBContent = appDBContent;
        }

        public async Task<IEnumerable<Compatibility>> GetAllAsync()
        {
            return await _appDBContent.Compatibility.ToListAsync();
        }

        public Task AddAsync(Compatibility entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Compatibility entity)
        {
            throw new NotImplementedException();
        }

        public Task<Compatibility> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Compatibility entity)
        {
            throw new NotImplementedException();
        }
    }
}
