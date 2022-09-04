using Application.Account;
using FluentValidation;

namespace Application.Validators;

public class RegisterCommandValidator : AbstractValidator<Register.Command>
{
    public RegisterCommandValidator()
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
