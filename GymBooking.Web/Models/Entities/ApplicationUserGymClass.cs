namespace GymBooking.Web.Models.Entities
{
    public class ApplicationUserGymClass
    {
        public int GymClassId { get; set; }
        public string ApplicationUserId { get; set; } //identityuser är alltid string. Ärver från IdentityUser klassen.

        public GymClass GymClass { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}