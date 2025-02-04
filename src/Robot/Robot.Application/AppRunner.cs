using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Robot.Application;
internal sealed class AppRunner(ILogger<AppRunner> logger)
{
    private readonly ILogger<AppRunner> _logger = logger;
    public async Task RunAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Application started.");
        while (!cancellationToken.IsCancellationRequested)
        {
            _logger.LogInformation("Enter a command: ");
            string? input = Console.ReadLine();
            if (string.Equals(input?.Trim(), "Quit", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation("Quit command received. Exiting...");
                break;
            }

        }

        await Task.FromResult(Task.CompletedTask);
    }
}
