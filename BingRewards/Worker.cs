namespace BingRewards
{
    public class Worker : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<Worker> _logger;
        private Timer _timer = null;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker starting at: {time}", DateTimeOffset.Now);
            _timer = new Timer(GetBusy, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
            return Task.CompletedTask;
        }

        private void GetBusy(object? state)
        {
            try
            {
                var count = Interlocked.Increment(ref executionCount);
                _logger.LogInformation($"Timed Hosted Service is working. Count: {count}");
                BatMine mine = new BatMine();
                mine.StartBrave();
                //mine.OpenHome();


            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thrown {ex}"); 
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