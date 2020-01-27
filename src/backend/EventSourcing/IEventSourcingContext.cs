using System.Threading.Tasks;
using Core.Events;
using Microsoft.EntityFrameworkCore;

namespace EventSourcing
{
    public interface IEventSourcingContext
    {
        DbSet<StoredEvent> StoredEvent { get; set; }
        Task<int> SaveChangesAsync();
    }
}
