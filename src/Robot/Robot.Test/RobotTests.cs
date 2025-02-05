using Microsoft.Extensions.Logging.Abstractions;
using Robot.Domain;

namespace Robot.Test;

public class RobotTests
{
    [Fact]
    public void PlaceWithValidCoordinatesSetsPositionAndDirection()
    {
        var table = new Table(8, 8);
        var robot = new Domain.Robot(table, NullLogger<Domain.Robot>.Instance);

        robot.Place(2, 3, Direction.North);

        Assert.Equal(2, robot.X);
        Assert.Equal(3, robot.Y);
        Assert.Equal(Direction.North, robot.Direction);
    }
    [Fact]
    public void PlaceWithInvalidCoordinatesDoesNotSetPosition()
    {
        var table = new Table(8, 8);
        var robot = new Domain.Robot(table, NullLogger<Domain.Robot>.Instance);

        robot.Place(10, 10, Direction.East);

        Assert.Null(robot.X);
        Assert.Null(robot.Y);
        Assert.Null(robot.Direction);
    }

    [Fact]
    public void MoveWhenNotPlacedDoesNothing()
    {
        var table = new Table(8, 8);
        var robot = new Domain.Robot(table, NullLogger<Domain.Robot>.Instance);

        robot.Move();

        Assert.Null(robot.X);
        Assert.Null(robot.Y);
        Assert.Null(robot.Direction);
    }

    [Fact]
    public void MoveWithValidMoveUpdatesPosition()
    {
        var table = new Table(8, 8);
        var robot = new Domain.Robot(table, NullLogger<Domain.Robot>.Instance);
        robot.Place(0, 0, Direction.North);

        robot.Move();

        Assert.Equal(0, robot.X);
        Assert.Equal(1, robot.Y);
    }

    [Fact]
    public void TurnLeftChangesDirectionCorrectly()
    {
        var table = new Table(8, 8);
        var robot = new Domain.Robot(table, NullLogger<Domain.Robot>.Instance);
        robot.Place(0, 0, Direction.North);

        robot.TurnLeft();

        Assert.Equal(Direction.West, robot.Direction);
    }
}
