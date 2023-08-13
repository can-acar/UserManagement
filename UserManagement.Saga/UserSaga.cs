namespace UserManagement.Saga
{
    public class UserSaga : MassTransitStateMachine<UserSagaState>
    {
        public State Registered { get; set; }

        public Event<IUserRegisteredEvent> UserRegisterEvent { get; set; }
        public Event<IUserRegisterActivateMailSendEvent> UserRegisterActivateMailSendEvent { get; set; }

    

        public UserSaga()
        {
            InstanceState(x => x.CurrentState);

            Event(() => UserRegisterEvent, x => x.CorrelateById(context => context.Message.UserId));
            Event(() => UserRegisterActivateMailSendEvent, x => x.CorrelateById(context => context.Message.UserId));
           

            Initially(
                When(UserRegisterEvent)
                    .Then(context =>
                    {
                        var user = context.Message;
                        context.Saga.UserId = user.UserId;
                        context.Saga.Username = user.Username;
                        context.Saga.Password = user.Password;
                        context.Saga.Email = user.Email;
                        context.Saga.ActivationCode = user.ActivationCode;
                    })
                    .TransitionTo(Registered)
                    .PublishAsync(context => context.Init<IUserRegisterActivateMailSendEvent>(new
                    {
                        UserId = context.Saga.UserId,
                        Username = context.Saga.Username,
                        Email = context.Saga.Email,
                        ActivationCode = context.Saga.ActivationCode
                    })));

  

            During(Registered,
                When(UserRegisterActivateMailSendEvent)
                    .Then(context =>
                    {
                        var mailEvent = context.Message;
                        Console.WriteLine($"Activation mail sent: UserId={mailEvent.UserId}, Username={mailEvent.Username}, Email={mailEvent.Email}");
                    })
                    .Finalize()
            );

            

            SetCompletedWhenFinalized();
        }
    }
}