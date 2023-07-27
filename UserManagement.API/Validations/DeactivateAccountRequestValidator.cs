using FluentValidation;
using UserManagement.API.Handlers;

namespace UserManagement.API.Validations;

public class DeactivateAccountRequestValidator : AbstractValidator<DeactivateAccountRequest>
{
    public DeactivateAccountRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}