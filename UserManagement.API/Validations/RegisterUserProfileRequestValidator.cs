using FluentValidation;
using UserManagement.API.Requests;

namespace UserManagement.API.Validations;

public class RegisterUserProfileRequestValidator : AbstractValidator<RegisterProfileRequest>
{
    public RegisterUserProfileRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.PhoneNumber).MaximumLength(20);
        RuleFor(x => x.Address).MaximumLength(200);
        RuleFor(x => x.City).MaximumLength(50);
        RuleFor(x => x.State).MaximumLength(50);
        RuleFor(x => x.ZipCode).MaximumLength(10);
        RuleFor(x => x.Country).MaximumLength(50);
        RuleFor(x => x.ProfilePictureUrl).MaximumLength(200);
    }
}