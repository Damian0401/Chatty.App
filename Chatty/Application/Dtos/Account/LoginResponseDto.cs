namespace Application.Dtos.Account;

public class LoginResponseDto
{
    public string UserName { get; set; } = default!;
    public string Token { get; set; } = default!;
}