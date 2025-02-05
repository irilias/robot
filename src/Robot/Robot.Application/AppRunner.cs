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

    public Task RunAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("\n Application started. Type commands in this format: \n * To place a robot: {Place} 0,0,NORTH \n * To move: {Move} \n * To turn: {Left}, {Right} \n * To get status: {Status} \n * To quit: {Quit}.\n\n",
                CommandConstants.Place, CommandConstants.Move, CommandConstants.Left, CommandConstants.Right, CommandConstants.Status, CommandConstants.Quit);
        while (!cancellationToken.IsCancellationRequested)
        {
            _logger.LogInformation("Enter a command: ");
            string input = Console.ReadLine() ?? string.Empty;
            if (string.Equals(input.Trim(), CommandConstants.Quit, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation("Quit command received. Exiting...");
                break;
            }
            if (string.IsNullOrWhiteSpace(input))
            {
                continue;
            }
            Option<ICommand> commandOption = _commandParser.Parse(input);
            commandOption.Match(
                some: command =>
                {
                    try
                    {
                        command.Execute();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error executing command: {Input}", input);
                    }
                },
                none: () => _logger.LogWarning("Invalid command."));
        }
        return Task.CompletedTask;
    }
}
