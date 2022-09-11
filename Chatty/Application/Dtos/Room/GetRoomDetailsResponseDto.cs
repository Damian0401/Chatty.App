using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Account;
using Application.Dtos.Message;

namespace Application.Dtos.Room;

public class GetRoomDetailsResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public List<MessageForGetRoomDetailsResponseDto> Messages { get; set; } = default!;
    public List<UserForGetRoomDetailsResponseDto> Users { get; set; } = default!;
}

public class MessageForGetRoomDetailsResponseDto
{
    public Guid Id { get; set; }
    public string Body { get; set; } = default!;
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? AuthorId { get; set; }
    public Guid RoomId { get; set; }
}

public class UserForGetRoomDetailsResponseDto
{
    public string Id { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public bool IsAdministrator { get; set; }
}