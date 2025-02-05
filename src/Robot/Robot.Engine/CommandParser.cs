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

namespace Robot.Engine;
public class CommandParser(ILogger<CommandParser> logger, Domain.Robot robot) : ICommandParser
{
    private readonly ILogger<CommandParser> _logger = logger;
    private readonly Domain.Robot _robot = robot;

    public Option<ICommand> Parse(string? input)
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

        string commandKey = commandEntries[0].ToUpperInvariant();
        switch (commandKey)
        {
            case CommandConstants.Place:
                if (commandEntries.Length < 2)
                {
                    _logger.LogWarning("Invalid PLACE command. Format: PLACE X,Y,DIRECTION");
                    return Option<ICommand>.None();
                }
                string[] args = commandEntries[1].Split(',', StringSplitOptions.RemoveEmptyEntries);
                if (args.Length != 3)
                {
                    _logger.LogWarning("Invalid PLACE command. Format: PLACE X,Y,DIRECTION");
                    return Option<ICommand>.None();
                }
                if (int.TryParse(args[0], out int x) &&
                    int.TryParse(args[1], out int y) &&
                    Enum.TryParse(args[2], true, out Direction direction))
                {
                    return Option<ICommand>.Some(new PlaceCommand(_robot, x, y, direction));
                }
                _logger.LogWarning("Invalid parameters for PLACE command.");
                return Option<ICommand>.None();

            case "MOVE":
                return Option<ICommand>.Some(new MoveCommand(_robot));
            case "LEFT":
                return Option<ICommand>.Some(new LeftCommand(_robot));
            case "RIGHT":
                return Option<ICommand>.Some(new RightCommand(_robot));
            case "STATUS":
                return Option<ICommand>.Some(new StatusCommand(_robot));
            default:
                _logger.LogWarning("No strategy found to handle command: {CommandKey}", commandKey);
                return Option<ICommand>.None();
        }
    }
}
