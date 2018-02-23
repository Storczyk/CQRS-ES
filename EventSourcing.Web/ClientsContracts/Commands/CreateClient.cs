using EventSourcing.Web.ClientsContracts.ValueObjects;
using EventSourcing.Web.Domain.Commands;

namespace EventSourcing.Web.ClientsContracts.Commands
{
    public class CreateClient : ICommand
    { 
        public ClientInfo Data { get; }

        public CreateClient(ClientInfo data)
        {
            Data = data;
        }
    }
}
