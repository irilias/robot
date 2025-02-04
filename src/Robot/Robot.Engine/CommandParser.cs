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

    public ICommand? Parse(string? input)
    {
        if (!InputValidator.IsValid(input))
        {
            _logger.LogWarning("Input contains invalid characters: {Input}", input);
            return default;
        }


        string[] commandEntries = input!.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (commandEntries.Length == 0)
        {
            return default;
        }

        string commandKey = commandEntries[0];
        switch (commandKey)
        {
            case CommandConstants.Place:
                if (commandEntries.Length < 2)
                {
                    _logger.LogWarning("Invalid PLACE command. Format: PLACE X,Y,DIRECTION");
                    return default;
                }
                string[] args = commandEntries[1].Split(',', StringSplitOptions.RemoveEmptyEntries);
                if (args.Length != 3)
                {
                    _logger.LogWarning("Invalid PLACE command. Format: PLACE X,Y,DIRECTION");
                    return default;
                }
                if (int.TryParse(args[0], out int x) &&
                    int.TryParse(args[1], out int y) &&
                    Enum.TryParse(args[2], true, out Direction direction))
                {
                    return new PlaceCommand(_robot, x, y, direction);
                }
                _logger.LogWarning("Invalid parameters for PLACE command.");
                return default;

            case "MOVE":
                return new MoveCommand(_robot);
            case "LEFT":
                return new LeftCommand(_robot);
            case "RIGHT":
                return new RightCommand(_robot);
            case "REPORT":
                return new StatusCommand(_robot);
            default:
                _logger.LogWarning("No strategy found to handle command: {CommandKey}", commandKey);
                return default;
        }
    }
}
