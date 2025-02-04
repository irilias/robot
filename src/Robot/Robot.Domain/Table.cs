using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Domain;
public sealed class Table
{
    public int Width { get; }
    public int Height { get; }

    public Table(int width, int height)
    {
        if (width <= 0 || height <= 0)
        {
            throw new ArgumentException("Table dimensions must be greater than zero.");
        }

        Width = width;
        Height = height;
    }

    public bool IsValidPosition(int x, int y) =>
        x >= 0 && x < Width && y >= 0 && y < Height;
}
