

using Core;
using Microsoft.Extensions.Logging.Abstractions;
using Robot.Domain;
using Robot.Engine;
using Robot.Engine.Commands;
using Robot.Engine.CommandStrategies;
namespace Robot.Test;
public class CommandParserTests
{
    [Fact]
    public void ParseValidPlaceCommandReturnsPlaceCommand()
    {
        var robot = new Robot.Domain.Robot(new Table(8, 8), NullLogger<Robot.Domain.Robot>.Instance);
        var strategies = new List<ICommandStrategy>
            {
                new PlaceCommandStrategy(robot),
                new MoveCommandStrategy(robot),
                new LeftCommandStrategy(robot),
                new RightCommandStrategy(robot),
                new StatusCommandStrategy(robot)
            };
        var parser = new CommandParser(NullLogger<CommandParser>.Instance, strategies);

        Option<ICommand> commandOption = parser.Parse("PLACE 1,2,NORTH");

        Assert.True(commandOption.IsSome);
        commandOption.Match(
            some: command => Assert.IsType<PlaceCommand>(command),
            none: () => Assert.Fail("Expected a PlaceCommand"));
    }

    [Fact]
    public void ParseInvalidCommandReturnsNone()
    {
        var robot = new Robot.Domain.Robot(new Table(8, 8), NullLogger<Robot.Domain.Robot>.Instance);
        var strategies = new List<ICommandStrategy>
            {
                new PlaceCommandStrategy(robot),
                new MoveCommandStrategy(robot),
                new LeftCommandStrategy(robot),
                new RightCommandStrategy(robot),
                new StatusCommandStrategy(robot)
            };
        var parser = new CommandParser(NullLogger<CommandParser>.Instance, strategies);

        Option<ICommand> commandOption = parser.Parse("INVALID 1,2,NORTH");

        Assert.True(commandOption.IsNone);
    }
}
