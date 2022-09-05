using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Message> Messages { get; set; } = default!;  
        public virtual ICollection<RoomApplicationUser> Rooms { get; set; } = default!;
    }
}