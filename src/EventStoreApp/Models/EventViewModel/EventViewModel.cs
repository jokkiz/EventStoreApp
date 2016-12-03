using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventStoreApp.Models.Abstract;
using EventStoreApp.Models.Entities;

namespace EventStoreApp.Models.EventViewModel
{
    public class EventListViewBuilder: IEventList
    {
        private readonly IEventRepository repository;

        public EventListViewBuilder(IEventRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Event> ListEvents(string searchString) => repository.Events
            .Where(i => string.IsNullOrEmpty(searchString) || i.Name.Contains(searchString))
            .ToList();

    }

    public class EventListViewModel
    {
        public IEnumerable<Event> EventList { get; set; }  
    }


}
