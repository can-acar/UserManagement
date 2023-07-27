using FluentValidation;
using UserManagement.API.Handlers;

namespace UserManagement.API.Validations;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotEmpty().MaximumLength(100).EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
}