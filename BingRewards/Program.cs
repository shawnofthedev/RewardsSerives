using BingRewards;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<DailyHostedService>();
        //services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
