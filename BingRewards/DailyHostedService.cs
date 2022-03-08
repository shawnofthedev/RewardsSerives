using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingRewards
{
    public class DailyHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<DailyHostedService> _logger;
        private Timer _timer = null!;

        public DailyHostedService(ILogger<DailyHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Timed Hosted Service running");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            try
            {
                var count = Interlocked.Increment(ref executionCount);
                _logger.LogInformation($"Timed Hosted Service is working. Count: {count}");
                var browserRun = new BingBrowserRun();
                browserRun.StartBrowser();
                //var currentPointCount = browserRun.GetCurrPoints();
                //_logger.LogInformation(currentPointCount.ToString());
                browserRun.Test();
                for (int i = 0; i < 40; i++)
                {
                    var searchStr = browserRun.RandomSearches();
                    _logger.LogInformation($"Searched for: {searchStr}");
                    Thread.Sleep(1000); 
                }

                browserRun.CloseBrowser();

            }
            catch (Exception)
            { 
                throw;
            }
        }

        public Task StopAsync(CancellationToken cancellation)
        {
            _logger.LogInformation($"Timed Hosted Service is stopping");
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask; 
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
