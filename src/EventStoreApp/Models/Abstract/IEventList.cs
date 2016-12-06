using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventStoreApp.Models.Entities;

namespace EventStoreApp.Models.Abstract
{
    interface IEventList
    {
        IEnumerable<Event> ListEvents(string searchString);
    }
}
