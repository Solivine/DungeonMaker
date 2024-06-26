using System.Data;

namespace DungeonMaker;

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

/// <summary>
/// The room base parent class for all children to inherit.
/// </summary>
/// <param name="initPosRow">The top left row position in the node square this room will generate from.</param>
/// <param name="initPosCol">The top left column position in the node square this room will generate from.</param>
/// <param name="height">How many tiles down to extend this room.</param>
/// <param name="width">How many tiles right to extend this room.</param>
public abstract class Room(Position initPosition, Size size)
{
    private readonly Position _initPosition = initPosition;
    private readonly Size _size = size;
    protected Node[,] _grid = new Node[size.Height, size.Width];

    public Position Position
    {
        get => _initPosition;
    }

    public Size Size
    {
        get => new Size(_size.Height, _size.Width);
    }

    public Node GetNode(Position pos)
    {
        return _grid[pos.Row - Position.Row, pos.Col - Position.Col];
    }

    public virtual void GenerateRoomNodes()
    {
        for (int row = 0; row < Size.Height; row++)
        {
            for (int col = 0; col < Size.Width; col++)
            {
                Node newNode = new Node(new Position(row, col))
                {
                    Content = "R"
                };
                _grid[row, col] = newNode;
            }
        }
    }

    /// <summary>
    /// Generates a corridor start node from one of the nodes on the edge of this room based on the node square side it will be connecting to.
    /// </summary>
    /// <param name="nodeSquareSide">The side of the room to find a suitable node on.</param>
    /// <returns></returns>
    /// <exception cref="Exception">Throws an exception if an invalid side is chosen.</exception>
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

        return corridorNodePosition;
    }
}