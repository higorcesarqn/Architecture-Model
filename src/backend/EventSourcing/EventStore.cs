using System.Threading.Tasks;
using Core.Events;
using Core.User;
using Newtonsoft.Json;

namespace EventSourcing
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IUser _user;

        public EventStore(IEventStoreRepository eventStoreRepository, IUser user)
        {
            _eventStoreRepository = eventStoreRepository;
            _user = user;
        }

        public Task Save(Event theEvent)
        {
            var serializedData = JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                _user.Username);

            return _eventStoreRepository.Store(storedEvent);
        }

    }
}
