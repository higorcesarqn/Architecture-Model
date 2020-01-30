using Core.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSourcing.Database.MongoDb
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly EventStoreMongoDbContext _eventStoreMongoDbContext;
        public EventStoreRepository(EventStoreMongoDbContext eventStoreMongoDbContext)
        {
            _eventStoreMongoDbContext = eventStoreMongoDbContext;
        }

        public async Task Store(StoredEvent theEvent)
        {
            var dbSet = _eventStoreMongoDbContext.GetCollection<StoredEvent>(typeof(StoredEvent).Name);
             _eventStoreMongoDbContext.AddCommand(() => dbSet.InsertOneAsync(theEvent));
        }

        public Task<IEnumerable<StoredEvent>> All(Guid aggregateId)
        {
            throw new NotImplementedException();
        }
    }
}
