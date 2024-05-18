namespace DungeonMaker;

public enum RoomType
{
    Basic,
    PointofInterest,
    Ring
}

public enum RoomSizeBias
{
    Smallest,
    Small,
    ExtraSmall,
    Medium,
    ExtraMedium,
    Large,
    ExtraLarge,
    Largest,
    Any,
    Fixed
}

public class Size(int height, int width)
{
    private readonly int _height = height;
    private readonly int _width = width;

    public int Height
    {
        get => _height;
    }

    public int Width
    {
        get => _width;
    }
}

public class Room(int initPosRow, int initPosCol, int height, int width)
{
    private int _initPosRow = initPosRow;
    private int _initPosCol = initPosCol;
    private int _width = width;
    private int _height = height;
    private List<Position> _corridorNodePositions = new List<Position>();

    public Position Position
    {
        get => new Position(_initPosRow, _initPosCol);
    }

    public Size Size
    {
        get => new Size(_height, _width);
    }

    public Position GenerateCorridorNodePosition(NodeSquareSide nodeSquareSide)
    {
        Random rnd = new Random();
        Position corridorNodePosition = nodeSquareSide switch
        {
            NodeSquareSide.Up => new Position(Position.Row, rnd.Next(Position.Col, Position.Col + Size.Width)),
            NodeSquareSide.Down => new Position(Position.Row + Size.Height - 1, rnd.Next(Position.Col, Position.Col + Size.Width)),
            NodeSquareSide.Left => new Position(rnd.Next(Position.Row, Position.Row + Size.Height), Position.Col),
            NodeSquareSide.Right => new Position(rnd.Next(Position.Row, Position.Row + Size.Height), Position.Col + Size.Width - 1),
            _ => throw new Exception("Invalid room side for corridor node position"),
        };
        _corridorNodePositions.Add(corridorNodePosition);

        //Console.WriteLine(String.Format("New room corridor node position started at {0}, {1}", corridorNodePosition.Row, corridorNodePosition.Col));
        return corridorNodePosition;
    }
}