using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Robot.Engine.Commands;

namespace Robot.Engine;
public interface ICommandParser
{
    Option<ICommand> Parse(string input);
}
