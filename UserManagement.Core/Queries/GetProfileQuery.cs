using MediatR;

namespace UserManagement.Core.Queries
{
    public class GetProfileQuery : IRequest<ServiceResponse>
    {
        public Guid Id { get; }

        public GetProfileQuery(Guid id)
        {
            Id = id;
        }
    }
}