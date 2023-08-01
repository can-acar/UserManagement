using FluentValidation;
using UserManagement.API.Requests;

namespace UserManagement.API.Validations;

public class RegisterUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    private readonly ILogger<RegisterUserRequestValidator> _logger;

    public RegisterUserRequestValidator(ILogger<RegisterUserRequestValidator> logger)
    {
        
         _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      
        _logger.LogInformation("Creating instance of RegisterUserRequestValidator");

        Validation();
    }

    private void Validation()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required").MaximumLength(50)
            .WithMessage("Username must not exceed 50 characters");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is invalid");
    }
}