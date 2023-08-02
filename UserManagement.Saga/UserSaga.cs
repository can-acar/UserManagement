using MassTransit;
using UserManagement.Core.Commands;
using UserManagement.Core.Events;

namespace UserManagement.Saga
{
    public class UserSaga : MassTransitStateMachine<UserSagaState>
    {
        public State UserCreatedState { get; private set; }
        public State UserMailSentState { get; private set; }


        public Event<UserCreateEvent> CreateUserEvent { get; private set; }


        public Event<UserActiveEvent> UserMailSendingState { get; private set; }


        public UserSaga()
        {
            InstanceState(x => x.CurrentState);


            Event(() => CreateUserEvent, x =>
            {
                x.CorrelateById(context => context.Message.UserId);
                x.SelectId(context => context.Message.UserId);
            });

            Event(() => UserMailSendingState, x =>
            {
                x.CorrelateById(context => context.Message.UserId);
                x.SelectId(context => context.Message.UserId);
            });


            Initially(
                When(CreateUserEvent)
                    .Then(UserAddedHandler)
                    .ThenAsync(context => Console.Out.WriteLineAsync($"User Created:{context.Message.UserId}"))
                    .TransitionTo(UserCreatedState)
                    .Publish(context => new SendUserActivisionMailCommand(context.Message.UserId, context.Message.Email))
                //, When(UserMailSendingState)
                //     .Then(context => Console.WriteLine($"Activation email received for user: {context.Saga.Email}"))
                //     .TransitionTo(UserMailSentState)
            );
            
            During(UserCreatedState,
                When(UserMailSendingState)
                    .Then(context => Console.WriteLine($"Activation email received for user: {context.Saga.Email}"))
                    .TransitionTo(UserMailSentState)
            );

            During(UserMailSentState,
                When(UserMailSendingState)
                    .Then(context => Console.WriteLine($"Activation email received for user: {context.Saga.Email}"))
                    .Finalize()
            );


            SetCompletedWhenFinalized();
        }

        private void UserAddedHandler(BehaviorContext<UserSagaState, UserCreateEvent> context)
        {
            context.Saga.Email = context.Message.Email;
            context.Saga.UserId = context.Message.UserId;
            context.Saga.Username = context.Message.Username;
            context.Saga.Password = context.Message.Password;
            context.Saga.CurrentState = UserCreatedState;
        }
    }
}