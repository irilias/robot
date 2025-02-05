using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Robot.Domain;
using Robot.Engine.Commands;

namespace Robot.Engine.CommandStrategies;
public class PlaceCommandStrategy(Domain.Robot robot) : ICommandStrategy
{
    private readonly Domain.Robot _robot = robot;

    public bool CanHandle(string command) => string.Equals(command, CommandConstants.Place, StringComparison.OrdinalIgnoreCase);

    public Option<ICommand> Create(string[] commandEntries)
    {
        if (commandEntries.Length < 2)
        {
            return Option<ICommand>.None();
        }

        string[] args = commandEntries[1].Split(',', StringSplitOptions.RemoveEmptyEntries);
        if (args.Length != 3)

        {
            return Option<ICommand>.None();
        }

        if (int.TryParse(args[0], out int x) &&
            int.TryParse(args[1], out int y) &&
            Enum.TryParse<Direction>(args[2], true, out Direction direction))
        {
            return Option<ICommand>.Some(new PlaceCommand(_robot, x, y, direction));
        }

        return Option<ICommand>.None();
    }
}
