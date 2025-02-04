using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Engine.Commands;
public class LeftCommand(Domain.Robot robot) : ICommand
{
    private readonly Domain.Robot _robot = robot ?? throw new ArgumentNullException(nameof(robot));

    public void Execute() => _robot.TurnLeft();
}
