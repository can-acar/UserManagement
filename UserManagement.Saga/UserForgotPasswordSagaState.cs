public class UserForgotPasswordSagaState : SagaStateMachineInstance, ISagaVersion
{
    public Guid CorrelationId { get; set; }
    public State? CurrentState { get; set; }
    public string? Email { get; set; }

    public int Version { get; set; }
}