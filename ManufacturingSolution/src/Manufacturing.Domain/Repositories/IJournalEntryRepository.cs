using System;
using System.Threading.Tasks;
using Manufacturing.Domain.Entities.Finance;

namespace Manufacturing.Domain.Repositories
{
    public interface IJournalEntryRepository
    {
        Task<JournalEntry?> GetByIdAsync(Guid id);
        Task AddAsync(JournalEntry entry);
        Task SaveChangesAsync();
    }
}