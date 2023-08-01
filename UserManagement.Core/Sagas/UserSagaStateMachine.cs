using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Sagas;

public class UserSagaStateMachine : MassTransitStateMachine<UserSagaState>
{
    public State UserCreatedState { get; private set; }
    public State UserDeletedState { get; private set; }
    public State UserUpdatedState { get; private set; }
    public State UserActiveState { get; private set; }


    public Event<ICreateUserEvent> CreateUserEvent { get; private set; }
    public Event<IUpdateUserEvent> UpdateUserEvent { get; private set; }
    public Event<IDeleteUserEvent> DeleteUserEvent { get; private set; }
    public Event<IActiveUserEvent> ActiveUserEvent { get; private set; }


    public UserSagaStateMachine()
    {
        InstanceState(x => x.CurrentState);
        
    }
}