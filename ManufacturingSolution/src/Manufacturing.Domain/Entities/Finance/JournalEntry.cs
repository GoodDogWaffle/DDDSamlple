using System;

namespace Manufacturing.Domain.Entities.Finance
{
    public enum EntryType { Debit, Credit }

    public class JournalEntry
    {
        public Guid Id { get; private set; }
        public DateTime EntryDate { get; private set; }
        public decimal Amount { get; private set; }
        public EntryType Type { get; private set; }
        public string Description { get; private set; }

        private JournalEntry() { }

        public JournalEntry(Guid id, DateTime date, decimal amount, EntryType type, string desc)
        {
            if(amount <= 0) throw new ArgumentException("Amount must be positive");
            if(string.IsNullOrWhiteSpace(desc)) throw new ArgumentException("Description required");

            Id = id;
            EntryDate = date;
            Amount = amount;
            Type = type;
            Description = desc;
        }
    }
}
