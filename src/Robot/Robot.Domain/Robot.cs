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

    public void Status() => _logger.LogInformation("Output: {X}, {Y}  direction {Direction}", X, Y, Direction);
}
