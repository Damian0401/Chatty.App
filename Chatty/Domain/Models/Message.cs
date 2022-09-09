namespace Domain.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Body { get; set; } = default!;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public string? AuthorId { get; set; }
        public Guid RoomId { get; set; }
        
        public virtual ApplicationUser Author { get; set; } = default!;
        public virtual Room Room { get; set; } = default!;
    }
}