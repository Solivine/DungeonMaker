using System.Text;

namespace DungeonMaker;

/// <summary>
/// A dungeon floor is defined by how many squares tall, and how many squares wide it is, and what the size of the squares is.
/// A square is full of nodes, and each contains one room.
/// Subclasses of dungeon floor defines the content of the squares.
/// </summary>
public abstract class Floor
{
    protected readonly NodeSquare[,] _grid;

    public Floor(Size size, int nodeSquareSize)
    {
        // 1. Establish floor
        // 1a. Initialise Empty Grid
        _grid = new NodeSquare[size.Height, size.Width];
        int maxRows = _grid.GetLength(0);
        int maxCols = _grid.GetLength(1);
        for (int row = 0; row < maxRows; row++)
        {
            for (int col = 0; col < maxCols; col++)
            {
                NodeSquare newNodeSquare = new NodeSquare(new Position(row, col), nodeSquareSize);
                _grid[row, col] = newNodeSquare;
            }
        }

        // 1b. Add Border
    }

    public NodeSquare[,] Grid
    {
        get => _grid;
    }

    public NodeSquare GetNodeSquare(Position pos)
    {
        return _grid[pos.Row, pos.Col];
    }

    public Size Size
    {
        get => new Size(_grid.GetLength(0), _grid.GetLength(1));
    }

    protected virtual void CloseSides()
    {
        throw new NotImplementedException("Method CloseSides must be implemented by subclasses");
    }

    /// <summary>
    /// Prints out all the nodes on the dungeon floor in a string format that can be somewhat understood.
    /// </summary>
    /// <returns>Returns a string of the entire dungeon floor.</returns>
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder("", Size.Height * Size.Width);

        bool squareDividers = true;

        int maxRows = Size.Height;
        int maxCols = Size.Width;

        for (int row = 0; row < maxRows; row++)
        {
            if (squareDividers)
            {
                sb.Append("\n{X,");
                for (int scol = 0; scol < maxCols; scol++)
                {
                    for (int snrow = 0; snrow < _grid[0, 0].Size.Height; snrow++)
                    {
                        sb.Append("X,");

                    }
                    sb.Append("X,");
                }
                sb.Append('}');
            }

            for (int line = 0; line < _grid[0, 0].Size.Height; line++)
            {
                if (squareDividers)
                {
                    sb.Append("\n{X,");
                }
                else
                {
                    sb.Append("\n{");
                }

                for (int col = 0; col < maxCols; col++)
                {
                    sb.AppendFormat("{0}", _grid[row, col].LineToString(line));
                    if (squareDividers) sb.Append("X,");
                }
            
                sb.Append('}');
            }
        }

        if (squareDividers)
        {
            sb.Append("\n{X,");
            for (int scol = 0; scol < maxCols; scol++)
            {
                for (int snrow = 0; snrow < _grid[0, 0].Size.Height; snrow++)
                {
                    sb.Append("X,");
                }
                sb.Append("X,");
            }
            sb.Append('}');
        }

        sb.Replace(",}", "}").Remove(0, 1);
        return sb.ToString();
    }
}