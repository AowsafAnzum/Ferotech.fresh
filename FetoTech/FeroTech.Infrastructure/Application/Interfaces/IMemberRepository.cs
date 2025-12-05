using FeroTech.Infrastructure.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeroTech.Infrastructure.Application.Interfaces
{
    public interface IMemberRepository
    {
        Task DeleteAsync(Guid memberId);
        Task<IEnumerable<Member>> GetAllAsync();
        //Task GetByIdAsync(Guid id);

    //    public interface IMemberRepository
    //{
    //    //Task<IEnumerable<Member>> GetAllAsync();
    Task<Member> GetByIdAsync(Guid id);
    Task AddAsync(Member member);
    Task UpdateAsync(Member member);
       //Task DeleteAsync(Guid id);
        
    //}
    }
}
