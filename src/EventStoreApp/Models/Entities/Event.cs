using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventStoreApp.Models.Entities
{
    [Table("Events")]
    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Короткое наименование")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина краткого наименования не может быть менее {2} и более {0} символов ")]
        public string ShortName { get; set; }

        [Display(Name = "Наименование мероприятия")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Полное наименование мероприятия не может быть короче {2} и длиннее {0}")]
        public string Name { get; set; }
        
        [Display(Name="Описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        
        [DataType(DataType.DateTime)]
        [Display(Name = "Дата начала")]
        public DateTime DateBegin { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Дата начала")]
        public DateTime DateEnd { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        public string AppUserOwnerId { get; set; }

        [Display(Name = "Автор")]
        [ForeignKey("AppUserOwnerId")]
        public ApplicationUser Owner { get; set; }

        [Display(Name = "Варианты пребывания")]
        public List<EventAmenity> EventAmenities { get; set; }
    }
}
