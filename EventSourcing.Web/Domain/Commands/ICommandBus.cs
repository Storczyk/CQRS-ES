using System.Threading.Tasks;

namespace EventSourcing.Web.Domain.Commands
{
    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
