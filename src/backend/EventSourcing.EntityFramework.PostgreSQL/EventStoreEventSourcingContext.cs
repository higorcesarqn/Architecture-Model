using System.Threading.Tasks;
using Core.Events;
using Microsoft.EntityFrameworkCore;

namespace EventSourcing.EntityFramework.PostgreSQL
{
    public class EventStoreEventSourcingContext : DbContext, IEventSourcingContext
    {
        public DbSet<StoredEvent> StoredEvent { get; set; }

        public EventStoreEventSourcingContext(DbContextOptions<EventStoreEventSourcingContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pgcrypto")
                .HasPostgresExtension("tablefunc");

            modelBuilder.Entity<StoredEvent>(entity =>
            {
                entity.ToTable("tbl_stored_event", "store_event");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.MessageType)
                    .HasColumnName("action");

                entity.Property(e => e.AggregateId)
                    .IsRequired()
                    .HasColumnName("aggregateid");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("creationdate");

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasColumnType("json");

                entity.Property(e => e.User)
                    .HasColumnName("usuario");
            });
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
