using Application.Account;
using FluentValidation;

namespace Application.Validators;

public class LoginCommandValidator : AbstractValidator<Login.Command>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(c => c.Password)
            .NotEmpty();
    }
}
