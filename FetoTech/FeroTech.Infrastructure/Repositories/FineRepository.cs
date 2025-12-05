using FeroTech.Infrastructure.Application.Interfaces;
using FeroTech.Infrastructure.Data;
using FeroTech.Infrastructure.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeroTech.Infrastructure.Repositories
{
    public class FineRepository : IFineRepository
    {
        private readonly ApplicationDbContext _context;
        public FineRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Fine>> GetAllAsync()
        {
            return await _context.Fine.ToListAsync();
        }
        public async Task<Fine?> GetByIdAsync(Guid id)
        {
            return await _context.Fine.FindAsync(id);
        }
        public async Task AddAsync(Fine fine)
        {
            _context.Fine.AddAsync(fine);
            await _context.SaveChangesAsync();
        }



        public async Task UpdateAsync(Fine fine)
        {
            _context.Fine.Update(fine);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var fine = await _context.Fine.FindAsync(id);
            if (fine != null) _context.Fine.Remove(fine);
            await _context.SaveChangesAsync();
        }

        //public Task UpdateAsync(Member member)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Fine?> GetByIdAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<Fine> IFineRepository.GetByIdAsync(Guid Id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
