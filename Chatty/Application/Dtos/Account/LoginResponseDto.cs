namespace Application.Dtos.Account;

public class LoginResponseDto
{
    public string Id { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Token { get; set; } = default!;
}