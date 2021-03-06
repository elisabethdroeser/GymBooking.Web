namespace GymBooking.Web.Models.Entities;
#nullable disable
public class GymClass
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTime EndTime => StartTime + Duration; 
    public string Description { get; set; }

    public ICollection<ApplicationUserGymClass> AttendingMembers { get; set; } = new List<ApplicationUserGymClass>();

}
