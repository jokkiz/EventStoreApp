using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventStoreApp.Models.Entities;

namespace EventStoreApp.Models.Abstract
{
    public interface IEventRepository
    {
        IEnumerable<Event> Events { get; }
        void SaveEvent(Event item);
        Event DeleteEvent(int id);
    }
}
