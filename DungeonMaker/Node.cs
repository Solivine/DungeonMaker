namespace DungeonMaker;

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

    public bool Equals(Position pos)
    {
        return Row == pos.Row && Col == pos.Col;
    }
}

public class Node(int row, int col)
{
    private object _nodeContent = "_";
    private Position _position = new Position(row, col);

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

    private object VerifyContent(object newContent)
    {
        if (newContent is string)
        {
            _nodeContent = newContent;
            return newContent;
        }
        else
        {
            return Content;
        }
    }

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