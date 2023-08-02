namespace UserManagement.Infrastructure
{
    public interface IMockMailProvider
    {
        void SendRegistrationConfirmation(string email, string activationLink);
    }
}