using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Robot.Application;

internal static class Program
{
    private static void Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();
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
       {
          
       })
       .ConfigureLogging(logging =>
       {
           logging.ClearProviders();
           logging.AddConsole();
       });
}
