using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Commands;
using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Sagas;

public class UserSagaStateMachine : MassTransitStateMachine<UserSagaState>
{
    public State UserCreated { get; private set; }
    public State UserActivated { get; private set; }

    public Event<ICreateUserEvent> CreateUserEvent { get; private set; }
    public Event<IActiveUserEvent> ActiveUserEvent { get; private set; }


    public UserSagaStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => CreateUserEvent,
            x =>
                x.CorrelateById(state => state.UserId, context => context.Message.UserId)
                    .SelectId(selector => Guid.NewGuid()));

        Event(() => ActiveUserEvent,
            x =>
                x.CorrelateById(state => state.UserId, context => context.Message.UserId));


        Initially(
            When(CreateUserEvent)
                .Then(context =>
                {
                    context.Instance.UserId = context.Data.UserId;
                    context.Instance.Email = context.Data.Email;
                }).ThenAsync(context => Console.Out.WriteLineAsync($"User {context.Instance.UserId} created"))
                .TransitionTo(UserCreated)
                .Publish(context => new SendUserActivitionMailCommand(context.Instance.UserId, context.Instance.Email)));


        During(UserCreated,
            When(ActiveUserEvent)
                .ThenAsync(context => Console.Out.WriteLineAsync($"User {context.Instance.UserId} activation mail sent"))
                .Finalize()
                .TransitionTo(UserActivated));

        SetCompletedWhenFinalized();
    }
}