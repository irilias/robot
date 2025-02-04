using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
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
            if (!InputValidator.IsValid(input))
            {
                _logger.LogWarning("Input contains invalid characters.");
                continue;
            }
            _logger.LogInformation("Running..");
        }

        await Task.FromResult(Task.CompletedTask);
    }
}
