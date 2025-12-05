using FeroTech.Infrastructure.Domain.Entities;
using FeroTech.Infrastructure.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeroTech.Infrastructure.Application.Interfaces
{
    public interface IFineRepository
    {
        Task<IEnumerable<Fine>> GetAllAsync();
        Task<Fine?> GetByIdAsync(Guid id);
        Task AddAsync(Fine fine);
        Task UpdateAsync(Fine fine);

        Task DeleteAsync(Guid id);
        //Task<Fine> GetByIdAsync(Guid fineId);
    }
}
