namespace UserManagement.Core.Sagas;

public class UserSagaState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public State CurrentState { get; set; }
    public Guid UserId { get; set; }
    public string Email { get; set; }
}