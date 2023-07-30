using MassTransit;
using UserManagement.Core.Interfaces;

namespace UserManagement.API.Sagas;

public class UserSagaStateMachine : MassTransitStateMachine<UserSagaState>
{
    public State UserCreated { get; private set; }
    public State UserActivated { get; private set; }

    public Event<IActiveUserEvent> CreateUserEvent { get; private set; }
    public Event<IActiveUserEvent> ActiveUserEvent { get; private set; }

    public UserSagaStateMachine()
    {
        InstanceState(x => x.CurrentState);
    }
}