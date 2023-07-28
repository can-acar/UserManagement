using FluentValidation;
using UserManagement.API.Handlers;
using UserManagement.API.Requests;

namespace UserManagement.API.Validations;

public class UpdatePasswordRequestValidator : AbstractValidator<UpdatePasswordRequest>
{
    public UpdatePasswordRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(6);
    }
}