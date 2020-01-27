using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Events;

namespace EventSourcing
{
    public  interface IEventStoreRepository
    {
        Task Store(StoredEvent theEvent);

        Task<IEnumerable<StoredEvent>> All(Guid aggregateId);
    }
}
