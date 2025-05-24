using System;
using System.Threading.Tasks;
using Manufacturing.Domain.Entities.Finance;
using Manufacturing.Domain.Repositories;

namespace Manufacturing.Application.Services
{
    public class FinanceService
    {
        private readonly IJournalEntryRepository _repo;
        public FinanceService(IJournalEntryRepository repo) => _repo = repo;

        public async Task<Guid> RecordEntryAsync(DateTime date, decimal amount, EntryType type, string desc)
        {
            var entry = new JournalEntry(Guid.NewGuid(), date, amount, type, desc);
            await _repo.AddAsync(entry);
            await _repo.SaveChangesAsync();
            return entry.Id;
        }
    }
}
