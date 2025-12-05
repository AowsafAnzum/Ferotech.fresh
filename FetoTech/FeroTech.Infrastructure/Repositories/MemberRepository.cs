using FeroTech.Infrastructure.Application.Interfaces;
using FeroTech.Infrastructure.Data;
using FeroTech.Infrastructure.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FeroTech.Infrastructure.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;
        public MemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _context.Member.ToListAsync();
        }
        public async Task<Member?> GetByIdAsync(Guid id)
        {
            return await _context.Member.FindAsync(id);
        }
        public async Task AddAsync(Member member)
        {
            _context.Member.Add(member);
            await _context.SaveChangesAsync();
        }
    
        

        public async Task UpdateAsync(Member member)
        {
            _context.Member.Update(member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var member = await _context.Member.FindAsync(id);
            if (member != null) _context.Member.Remove(member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var member = await _context.Member.FindAsync(id);
            if (member != null) _context.Member.Remove(member);
            await _context.SaveChangesAsync();
        }



        //Task IMemberRepository.GetByIdAsync(Guid id)
        //{
        //    return GetByIdAsync(id);
        //}

        //Task<IEnumerable<object>> IMemberRepository.GetAllAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
