using FluentValidation;
using UserManagement.API.Handlers;
using UserManagement.API.Requests;

namespace UserManagement.API.Validations;

public class RegisterUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
}