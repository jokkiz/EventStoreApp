using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventStoreApp.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EventStoreApp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

       public List<Event> UserEvents { get; set; } 
    }
}
