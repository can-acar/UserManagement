using UserManagement.Infrastructure.Commons;

namespace UserManagement.API.Queries;

public class GetProfileQuery : IRequest<ServiceResponse>
{
    public Guid Id { get; }

    public GetProfileQuery(Guid id)
    {
        Id = id;
    }
}