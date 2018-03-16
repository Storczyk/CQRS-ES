using System.Threading;
using System.Threading.Tasks;

namespace EventSourcing.Web
{
    public abstract class HostedService : IHostedService
    {
        private Task<bool> _executingTask;
        private CancellationTokenSource _cts;

        public Task<bool> StartAsync(CancellationToken cancellationToken)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _executingTask = ExecuteAsync(_cts.Token);

            if (_executingTask.IsCompleted)
            {
                return Task.FromResult(_executingTask.Result);
            }
            else
            {
                return new Task<bool>(() => false, default(CancellationToken), TaskCreationOptions.None);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop called without start
            if (_executingTask == null)
            {
                return;
            }
            _cts.Cancel();
            await Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken));
            cancellationToken.ThrowIfCancellationRequested();
        }
        public abstract Task<bool> ExecuteAsync(CancellationToken cancellationToken);
    }
}
