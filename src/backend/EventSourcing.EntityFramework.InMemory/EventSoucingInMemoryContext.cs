using System;
using System.Threading.Tasks;
using Core.Events;
using Microsoft.EntityFrameworkCore;

namespace EventSourcing.EntityFramework.InMemory
{
    public class EventSoucingInMemoryContext : DbContext
    {
        public DbSet<StoredEvent> StoredEvent { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbName = Guid.NewGuid().ToString();

            optionsBuilder.UseInMemoryDatabase(dbName);

            base.OnConfiguring(optionsBuilder);
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
