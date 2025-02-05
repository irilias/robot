using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core;
using Microsoft.Extensions.Logging;
using Robot.Domain;
using Robot.Engine.Commands;
using Robot.Engine.CommandStrategies;

namespace Robot.Engine;
public class CommandParser(ILogger<CommandParser> logger, IEnumerable<ICommandStrategy> strategies) : ICommandParser
{
    private readonly ILogger<CommandParser> _logger = logger;
    private readonly IEnumerable<ICommandStrategy> _strategies = strategies;
    public Option<ICommand> Parse(string input)
    {
        if (!InputValidator.IsValid(input))
        {
            _logger.LogWarning("Input contains invalid characters: {Input}", input);
            return Option<ICommand>.None();
        }


        string[] commandEntries = input!.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (commandEntries.Length == 0)
        {
            return Option<ICommand>.None();
        }

        string commandKey = commandEntries[0];
        ICommandStrategy? strategy = _strategies.FirstOrDefault(s => s.CanHandle(commandKey));
        if (strategy != null)
        {
            return strategy.Create(commandEntries);
        }

        _logger.LogWarning("No strategy found to handle command: {CommandKey}", commandKey);
        return Option<ICommand>.None();
    }
}
