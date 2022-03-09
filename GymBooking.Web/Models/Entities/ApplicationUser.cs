using Microsoft.AspNetCore.Identity;
#nullable disable
namespace GymBooking.Web.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ApplicationUserGymClass> AttendingClasses { get; set; } //kopplingstabell
    }
}
