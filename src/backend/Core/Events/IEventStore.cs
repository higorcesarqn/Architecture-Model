using System.Threading.Tasks;

namespace Core.Events
{
    public interface IEventStore
    {
        Task Save(Event theEvent);
    }
}