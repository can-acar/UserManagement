namespace UserManagement.Infrastructure.Commons;

public class RabbitMqOptions
{
    private readonly IConfiguration _configuration;

    public RabbitMqOptions(IConfiguration configuration)
    {
        _configuration = configuration.GetSection("RabbitMq");
    }

    public string RabbitMqUri => _configuration["Host"];
    public string RabbitMqUserName => _configuration["UserName"];
    public string RabbitMqPassword => _configuration["Password"];
}