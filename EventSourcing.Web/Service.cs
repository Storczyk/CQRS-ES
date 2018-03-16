using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourcing.Web
{
    public class Service : HostedService
    {
        //injection of service, this will delay so that rest program wont wait(?)
        public override async Task<bool> ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                //this Task.Delay is necessary (might be 0.1)
                await Task.Delay(TimeSpan.FromSeconds(0.05), cancellationToken);
                //operations
                return true;
            }
            return false;
        }
    }
}
