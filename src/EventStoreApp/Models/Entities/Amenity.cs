using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventStoreApp.Models.Entities
{
    [Table("Amenities")]
    public class Amenity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AmenityId { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Наименование варианта пребывания")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)] 
        public string Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Дата начала действия")]
        public DateTime DateBegin { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Дата окончания действия")]
        public DateTime DateEnd { get; set; }

        [Display(Name = "Цена")]
        public decimal Cost { get; set; }
    }


}
