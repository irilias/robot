using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Robot.Engine.Commands;

namespace Robot.Engine.CommandStrategies;
public interface ICommandStrategy
{
    bool CanHandle(string command);
    Option<ICommand> Create(string[] commandEntries);
}

