using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EventStoreApp.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Поле {0} не может быть менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Введите один и тот же пароль оба раза.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name="Имя")]
        public string LastName { get; set; }

        [Display(Name = "Имя пользователя")]
        [StringLength(20, ErrorMessage = "Поле {0} не может быть менее {2} символов.", MinimumLength = 6)]
        public string UserName { get; set; }

        [Display(Name = "День рождения")]
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }
    }
}
