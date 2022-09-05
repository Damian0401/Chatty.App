namespace Domain.Models
{
    public class RoomApplicationUser
    {
        public Guid RoomId { get; set; }
        public string UserId { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public bool IsAdministrator { get; set; } 
        public DateTime JoinDate { get; set; }

        public virtual Room Room { get; set; } = default!;
        public virtual ApplicationUser User { get; set; } = default!;
    }
}