using Microsoft.Extensions.Logging;

namespace Robot.Domain;

public sealed class Robot(Table table, ILogger<Robot> logger)
{
    private readonly Table _table = table ?? throw new ArgumentNullException(nameof(table));
    private readonly ILogger<Robot> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public int? X { get; private set; }
    public int? Y { get; private set; }
    public Direction? Direction { get; private set; }
    public void Place(int x, int y, Direction direction)
    {
        if (!_table.IsValidPosition(x, y))
        {
            _logger.LogWarning("Invalid placement: ({X},{Y}) is out of bounds.", x, y);
            return;
        }

        X = x;
        Y = y;
        Direction = direction;
        _logger.LogInformation("Robot placed at ({X},{Y}) direction {Direction}", x, y, direction);
    }
    public void Move()
    {
        if (!IsPlaced())
        {
            _logger.LogWarning("Robot not placed on table.");
            return;
        }
        (int newX, int newY) = Direction switch
        {
            Domain.Direction.North => (X!.Value, Y!.Value + 1),
            Domain.Direction.East => (X!.Value + 1, Y!.Value),
            Domain.Direction.South => (X!.Value, Y!.Value - 1),
            Domain.Direction.West => (X!.Value - 1, Y!.Value),
            _ => (X!.Value, Y!.Value)
        };

        if (_table.IsValidPosition(newX, newY))
        {
            X = newX;
            Y = newY;
            _logger.LogInformation("Robot moved to ({X},{Y}).", newX, newY);
        }
        else
        {
            _logger.LogWarning("Move prevented: ({X},{Y}) would be out of bounds.", newX, newY);
        }
    }
    public void TurnLeft()
    {
        if (!IsPlaced())
        {
            _logger.LogWarning("Robot not placed on table.");
            return;
        }

        Direction = Direction switch
        {
            Domain.Direction.North => Domain.Direction.West,
            Domain.Direction.West => Domain.Direction.South,
            Domain.Direction.South => Domain.Direction.East,
            Domain.Direction.East => Domain.Direction.North,
            _ => Direction
        };
        _logger.LogInformation("Robot turned left to the direction {Direction}.", Direction);
    }

    public void TurnRight()
    {
        if (!IsPlaced())
        {
            _logger.LogWarning("Robot not placed on table.");
            return;
        }

        Direction = Direction switch
        {
            Domain.Direction.North => Domain.Direction.East,
            Domain.Direction.East => Domain.Direction.South,
            Domain.Direction.South => Domain.Direction.West,
            Domain.Direction.West => Domain.Direction.North,
            _ => Direction
        };
        _logger.LogInformation("Robot turned right to the direction {Direction}.", Direction);
    }
    public void Status() => _logger.LogInformation("Output: {X}, {Y}  direction {Direction}", X, Y, Direction);
    private bool IsPlaced() =>
            X.HasValue && Y.HasValue && Direction.HasValue;
}
