namespace DungeonMaker;

/// <summary>
/// A position class has a row position and a column position.
/// </summary>
/// <param name="row">The row this position is at.</param>
/// <param name="col">The column this position is at.</param>
public class Position(int row, int col)
{
    private readonly int _row = row;
    private readonly int _col = col;

    public int Row
    {
        get => _row;
    }

    public int Col
    {
        get => _col;
    }

    /// <summary>
    /// Compares itself to the row and column of another position class, to see if they are located at the same point.
    /// </summary>
    /// <param name="pos">The position class to compare with.</param>
    /// <returns>Returns true is the row and column matches.</returns>
    public bool Equals(Position pos)
    {
        return Row == pos.Row && Col == pos.Col;
    }
}

/// <summary>
/// A node is a single square on the dungeon floor.
/// </summary>
/// <param name="pos">The position of this node.
public class Node(Position pos)
{
    private object _nodeContent = "_";
    private Position _position = pos;

    public Position Position
    {
        get => _position;
    }

    public double CalculatePythagoreanDistance(Position nodePosition)
    {
        return Math.Sqrt(Math.Pow(_position.Row + nodePosition.Row, 2) + Math.Pow(_position.Col + nodePosition.Col, 2));
    }

    public int CalculateManhattanDistance(Position nodePosition)
    {
        return Math.Abs(_position.Row - nodePosition.Row) + Math.Abs(_position.Col - nodePosition.Col);
    }

    public object Content
    {
        get => _nodeContent;
        set => VerifyContent(value);
    }

    /// <summary>
    /// Checks content is a string before setting it.
    /// </summary>
    /// <param name="newContent">Content to be set.</param>
    /// <returns>Returns the new content.</returns>
    /// <exception cref="InvalidDataException">Throws an invalid data exception if content is not a string.</exception>
    private object VerifyContent(object newContent)
    {
        if (newContent is string)
        {
            _nodeContent = newContent;
            return newContent;
        }
        else
        {
            throw new InvalidDataException("Cannot set content to a non string.");
        }
    }

    /// <summary>
    /// Used for defining what letters represent on the grid.
    /// </summary>
    public string ContentType
    {
        get => _nodeContent switch
        {
            "C" => "Corridor",
            "_" => "Wall",
            "R" => "Room",
            "S" => "Stairs",
            "T" => "Temp",
            "X" => "Border",
            _ => "Undefined",
        };
    }

    public override string ToString()
    {
        return (string) _nodeContent;
    }
}