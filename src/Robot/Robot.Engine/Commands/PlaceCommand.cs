﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robot.Domain;

namespace Robot.Engine.Commands;
public class PlaceCommand(Domain.Robot robot, int x, int y, Direction direction) : ICommand
{
    private readonly Domain.Robot _robot = robot ?? throw new ArgumentNullException(nameof(robot));
    private readonly int _x = x;
    private readonly int _y = y;
    private readonly Direction _direction = direction;
    public void Execute() => _robot.Place(_x, _y, _direction);
}
