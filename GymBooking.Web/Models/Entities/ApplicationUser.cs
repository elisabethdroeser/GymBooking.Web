using Microsoft.AspNetCore.Identity;
#nullable disable
namespace GymBooking.Web.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set;  }

        public string LastName { get; set; }

        public string FullName => @"{FirstName} {LastName}";
        public DateTime TimeOfRegistration { get; set; }
        public ICollection<ApplicationUserGymClass> AttendingClasses { get; set; } //kopplingstabell
    }
}
