using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Robot.Application;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();
        AppRunner app = host.Services.GetRequiredService<AppRunner>();
        ILogger<IProgramLogger> logger = host.Services.GetRequiredService<ILogger<IProgramLogger>>();
        try
        {
            await app.RunAsync(CancellationToken.None);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception occurred.");
        }
    }
    private static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
       .ConfigureAppConfiguration((hostingContext, config) =>
       {
           config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
           config.AddEnvironmentVariables();
           config.AddCommandLine(args);
       })
       .ConfigureServices((context, services) =>
           services.AddTransient<AppRunner>())
       .ConfigureLogging(logging =>
       {
           logging.ClearProviders();
           logging.AddConsole();
       });
}
internal interface IProgramLogger { }
