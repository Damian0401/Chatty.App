namespace Application.Dtos.Account;

public class RegisterResponseDto
{
    public string Id { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Token { get; set; } = default!;
}