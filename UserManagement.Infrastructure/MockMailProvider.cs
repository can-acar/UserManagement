namespace UserManagement.Infrastructure
{
    // MockMailProvider.cs
    public class MockMailProvider : IMockMailProvider
    {
        public void SendRegistrationConfirmation(string email, string activationLink)
        {
            // Instead of sending an actual email, log the activation link for testing
            Console.WriteLine($"Activation link for {email}: {activationLink}");
        }
    }
}