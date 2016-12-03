using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventStoreApp.Data;
using EventStoreApp.Models.Abstract;
using EventStoreApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EventStoreApp.Models.Concrete
{
    public class EventRepository: IEventRepository
    {
        private ApplicationDbContext context;

        public EventRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Event> Events => context.Events;

        public void SaveEvent(Event item)
        {
            if (item.Id == 0)
            {
                context.Events.Add(item);
            }
            else
            {
                var dbEvent = context.Events.FirstOrDefault(i => i.Id == item.Id);
                if (dbEvent != null)
                {
                    dbEvent.Name = item.Name;
                    dbEvent.ShortName = item.Name;
                    dbEvent.Description = item.Description;
                    dbEvent.DateBegin = item.DateBegin;
                    dbEvent.DateEnd = item.DateEnd;
                    dbEvent.ImageData = item.ImageData;
                    dbEvent.ImageMimeType = item.ImageMimeType;
                    dbEvent.EventAmenities = item.EventAmenities;
                }
            }
            context.SaveChanges();
        }

        public Event DeleteEvent(int id)
        {
            Event dbEvent = context.Events.FirstOrDefault(i => i.Id == id);
            if (dbEvent != null)
            {
                context.Events.Remove(dbEvent);
                context.SaveChanges();
            }
            return dbEvent;
        }



    }
}
