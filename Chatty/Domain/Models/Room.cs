using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Room
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        
        public virtual ICollection<Message> Messages { get; set; } = default!;
        public virtual ICollection<RoomApplicationUser> Users { get; set; } = default!;
    }
}