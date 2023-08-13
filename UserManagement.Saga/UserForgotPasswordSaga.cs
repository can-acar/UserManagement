public class UserForgotPasswordSaga : MassTransitStateMachine<UserForgotPasswordSagaState>
{
    // Forgot password
    public State ForgotPassword { get; set; }

    public Event<IUserForgotPasswordEvent> UserForgotPasswordEvent { get; set; }

    public Event<IUserForgotPasswordMailSendEvent> UserForgotPasswordMailSendEvent { get; set; }

    public UserForgotPasswordSaga()
    {
        InstanceState(x => x.CurrentState);
        // Forgot password
        Event(() => UserForgotPasswordEvent, x => x.CorrelateBy(context => context.Email, context => context.Message.Email)
            .SelectId(context => NewId.NextGuid()));
        Event(() => UserForgotPasswordMailSendEvent, x => x.CorrelateBy(context => context.Email, context => context.Message.Email)
            .SelectId(context => NewId.NextGuid()));


        Initially(
            When(UserForgotPasswordEvent)
                .Then(context =>
                {
                    var user = context.Message;

                    context.Saga.Email = user.Email;
                })
                .TransitionTo(ForgotPassword)
                .PublishAsync(context => context.Init<IUserForgotPasswordMailSendEvent>(new
                {
                    Email = context.Saga.Email
                })));

        During(ForgotPassword,
            When(UserForgotPasswordMailSendEvent)
                .Then(context =>
                {
                    var mailEvent = context.Message;
                    Console.WriteLine($"Forgot password mail sent: Email={mailEvent.Email}");
                })
                .Finalize()
        );


        SetCompletedWhenFinalized();
    }
}