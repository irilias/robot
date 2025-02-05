using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Robot.Engine.Commands;

namespace Robot.Engine.CommandStrategies;
public class RightCommandStrategy(Domain.Robot robot) : ICommandStrategy
{
    private readonly Domain.Robot _robot = robot;

    public bool CanHandle(string command) => string.Equals(command, CommandConstants.Right, StringComparison.OrdinalIgnoreCase);

    public Option<ICommand> Create(string[] commandEntries)
    {
        if (commandEntries.Length > 1)
        {
            return Option<ICommand>.None();
        }
        return Option<ICommand>.Some(new RightCommand(_robot));
    }
}
