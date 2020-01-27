using Egl.Sit.Infra.EntityFramework.UnitOfWork.Entities;
using Microsoft.EntityFrameworkCore;

namespace Egl.Sit.Infra.EntityFramework.UnitOfWork
{
    public class InMemoryTestContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("test");
        }
    }
}
