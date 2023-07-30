namespace UserManagement.API.Sagas;

public class UserSagaState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}