using MediatR;

namespace EventSourcing.Web.Domain.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
