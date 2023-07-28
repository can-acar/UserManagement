

namespace UserManagement.API.Commands;

public class CreateUserCommand : IRequest<ServiceResponse>
{
    public string Username { get; }
    public string Password { get; }

    public CreateUserCommand(string requestUsername, string requestPassword)
    {
        Username = requestUsername;
        Password = requestPassword;
    }
}

public class RegisterUserProfileCommand : IRequest<ServiceResponse>
{
    public RegisterUserProfileCommand(Guid userId, string firstName, string lastName, string email, string? phoneNumber,
        string? address, string? city, string? state, string? zipCode, string? country, string? profilePictureUrl)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
        ProfilePictureUrl = profilePictureUrl;
    }

    public Guid UserId { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? Country { get; set; }
    public string? ProfilePictureUrl { get; set; }
}