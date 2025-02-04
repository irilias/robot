using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Microsoft.Extensions.Logging;
using Robot.Engine;
using Robot.Engine.Commands;

namespace Robot.Application;
internal sealed class AppRunner(ILogger<AppRunner> logger, ICommandParser commandParser)
{
    private readonly ILogger<AppRunner> _logger = logger;
    private readonly ICommandParser _commandParser = commandParser;

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Application started. Type commands or {Quit} to exit.", CommandConstants.Quit);
        while (!cancellationToken.IsCancellationRequested)
        {
            _logger.LogInformation("Enter a command: ");
            string? input = Console.ReadLine();
            if (string.Equals(input?.Trim(), CommandConstants.Quit, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation("Quit command received. Exiting...");
                break;
            }
           
            _logger.LogInformation("Running..");
            ICommand? command = _commandParser.Parse(input);
            command?.Execute();
        }

        await Task.FromResult(Task.CompletedTask);
    }
}
