using Application.Dtos.Account;
using FluentValidation;

namespace Application.Validators;

public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterRequestDtoValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(c => c.Password)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(c => c.UserName)
            .NotEmpty()
            .MinimumLength(6);
    }
}
