namespace Application.Dtos.Account;

public class RegisterRequestDto
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string UserName { get; set; } = default!;
}
