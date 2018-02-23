using MediatR;

namespace EventSourcing.Web.Domain.Commands
{
    public interface ICommandHandler<in T> : IRequestHandler<T> where T : ICommand
    {
    }
}
