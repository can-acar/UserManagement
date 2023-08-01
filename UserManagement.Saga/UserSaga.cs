using MassTransit;
using UserManagement.Core.Commands;
using UserManagement.Core.Events;

namespace UserManagement.Saga
{
    public class UserSaga : MassTransitStateMachine<UserSagaState>
    {
        public State UserCreatedState { get; private set; }

        // public State UserDeletedState { get; private set; }
        // public State UserUpdatedState { get; private set; }
        public State UserActivedState { get; private set; }


        public Event<UserCreateEvent> CreateUserEvent { get; private set; }

        // public Event<IUserUpdateEvent> UpdateUserEvent { get; private set; }
        // public Event<IUserDeleteEvent> DeleteUserEvent { get; private set; }
        public Event<UserActiveEvent> ActiveUserEvent { get; private set; }


        public UserSaga()
        {
            InstanceState(x => x.CurrentState);

            // Event(() => CreateUserEvent,
            //     x => x
            //         .CorrelateBy(state => state.Email, context => context.Message.Email)
            //         .SelectId(context => Guid.NewGuid()));
            // Event(() => UpdateUserEvent, x => x.CorrelateById(context => context.Message.UserId));
            // Event(() => DeleteUserEvent, x => x.CorrelateById(context => context.Message.UserId));

            Event(() => CreateUserEvent, x => x.CorrelateById(context => context.Message.UserId));
            Event(() => ActiveUserEvent, x => x.CorrelateById(context => context.Message.UserId));

            Initially(
                When(CreateUserEvent)
                    // .Then(context => context.Instance.UserId = context.Data.UserId)
                    // .ThenAsync(context => Console.Out.WriteLineAsync($"User Created: {context.Message.UserId}"))
                    .TransitionTo(UserCreatedState)
                    .Publish(context => new SendUserActivisionMailCommand(context.Message.UserId, context.Message.Email)));


            During(UserCreatedState,
                When(ActiveUserEvent)
                    .ThenAsync(context => Console.Out.WriteLineAsync($"User Activated: {context.Instance.UserId}"))
                    .Finalize()
            );

            SetCompletedWhenFinalized();
        }
    }
}