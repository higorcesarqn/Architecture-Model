using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infra.EntityFramework.InMemory
{
    public class SitInMemoryContext : DbContext
    {
        public SitInMemoryContext(DbContextOptions<SitInMemoryContext> options) : base(options)
        {

        }

        public SitInMemoryContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbName = Guid.NewGuid().ToString();

            optionsBuilder.UseInMemoryDatabase(dbName);
            optionsBuilder.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
