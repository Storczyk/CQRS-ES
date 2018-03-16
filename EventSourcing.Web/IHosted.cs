using System.Threading;
using System.Threading.Tasks;

namespace EventSourcing.Web
{
    public interface IHostedService
    {
        Task<bool> StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
        Task<bool> ExecuteAsync(CancellationToken cancellationToken);
    }
}