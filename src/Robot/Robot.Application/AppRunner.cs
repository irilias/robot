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
internal sealed class AppRunner(ILogger<AppRunner> logger, Domain.Robot robot)
{
    private readonly ILogger<AppRunner> _logger = logger;
    private readonly Domain.Robot robot = robot;

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
            if (!InputValidator.IsValid(input))
            {
                _logger.LogWarning("Input contains invalid characters.");
                continue;
            }
            _logger.LogInformation("Running..");
            new PlaceCommand(robot, 1, 1, Domain.Direction.East).Execute();
            new StatusCommand(robot).Execute();
            robot.TurnRight();
            robot.Move();
            new StatusCommand(robot).Execute();

        }

        await Task.FromResult(Task.CompletedTask);
    }
}
