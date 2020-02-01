using Core.Events;
using MongoDB.Driver;
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
            var storedEventMongoCollectionmongoCollection = _eventStoreMongoDbContext.GetCollection<StoredEvent>(typeof(StoredEvent).Name);
             _eventStoreMongoDbContext.AddCommand(() => storedEventMongoCollectionmongoCollection.InsertOneAsync(theEvent));
             await _eventStoreMongoDbContext.SaveChanges();
        }

        public async Task<IEnumerable<StoredEvent>> All(Guid aggregateId)
        {
            var storedEventMongoCollectionmongoCollection = _eventStoreMongoDbContext.GetCollection<StoredEvent>(typeof(StoredEvent).Name);
            return await storedEventMongoCollectionmongoCollection.Find( x => x.AggregateId == aggregateId).ToListAsync();
        }
    }
}
