using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EventStoreApp.Models.Abstract;
using EventStoreApp.Models.Entities;

namespace EventStoreApp.Models.EventViewModel
{
    public class EventListViewBuilder: IEventList
    {
        private readonly IEventRepository repository;
        public int PageSize = 9; 

        public EventListViewBuilder(IEventRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Event> ListEvents(string searchString, int page = 1) => repository.Events
            .Where(i => string.IsNullOrEmpty(searchString) || i.Name.Contains(searchString))
            .OrderBy(i=>i.DateBegin)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToList();
    }

    public class EventListViewModel
    {
        public IEnumerable<Event> EventList { get; set; }  
    }

    public class EventViewModel
    {
        [Required]
        [Display(Name = "Короткое наименование")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина краткого наименования не может быть менее {2} и более {0} символов ")]
        public string ShortName { get; set; }

        [Display(Name = "Наименование мероприятия")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Полное наименование мероприятия не может быть короче {2} и длиннее {0}")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Дата начала")]
        public DateTime DateBegin { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Дата окончания")]
        public DateTime DateEnd { get; set; }

        [Display(Name="Автор")]
        public string OwnerName { get; set; }

        [Display(Name="Варианты пребывания")]
        public List<Amenity> Amenities { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }

    public class EventViewModelBuilder
    {
        public EventViewModel CreateEventViewModel(Event e) => new EventViewModel
        {
            Name = e.Name,
            ShortName = e.ShortName,
            DateBegin = e.DateBegin,
            DateEnd = e.DateEnd,
            Description = e.Description,
            ImageData = e.ImageData,
            ImageMimeType = e.ImageMimeType,
            OwnerName = e.Owner.UserName,
            Amenities = e.EventAmenities.Select(a => a.Amenity).ToList()
        };
        
    }
}
