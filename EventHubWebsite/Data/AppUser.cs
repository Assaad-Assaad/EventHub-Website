using EventHubWebsite.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EventHubWebsite.Data
{
    public class AppUser : IdentityUser<int>
    {
        [StringLength(250)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [StringLength(250)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        public List<UserEvent> UserEvents { get; set; } = new List<UserEvent>();
    }

}
