using System.Text;

namespace DungeonMaker;

// Corridor generation should be different
enum FloorType
{
    AllSquares,
    ClosedCorners,
    //ClosedSpread  // picks a corner, closes it, and spreads randomly in rows and cols anything from an L to a triangle shape
}

/// <summary>
/// A dungeon floor is defined by how many squares tall, and how many squares wide it is, and what the size of the squares is.
/// A square is full of nodes, and each contains one room or point of interest.
/// Subclasses of dungeon floor defines the content of the squares.
/// </summary>
public abstract class Floor
{
    protected readonly NodeSquare[,] _grid;
    protected readonly RoomGeneratorType _roomGeneratorType;
    protected readonly CorridorGeneratorType _corridorGeneratorType;

    public Floor(Size size, int nodeSquareSize, RoomGeneratorType roomGeneratorType, CorridorGeneratorType corridorGeneratorType)
    {
        // Assign variables
        _roomGeneratorType = roomGeneratorType;
        _corridorGeneratorType = corridorGeneratorType;

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

    public void GenerateRooms(RoomSizeBias roomSizeBias = RoomSizeBias.Any)
    {
        switch (_roomGeneratorType)
        {
            case RoomGeneratorType.All:
                AllRoomGenerator allRoomGenerator = new AllRoomGenerator(this);
                allRoomGenerator.GenerateRooms(roomSizeBias);
                break;
            case RoomGeneratorType.Edge:
                EdgeRoomGenerator edgeRoomGenerator = new EdgeRoomGenerator(this);
                edgeRoomGenerator.GenerateRooms(roomSizeBias);
                break;
            case RoomGeneratorType.Mixed:
                MixedRoomGenerator mixedRoomGenerator = new MixedRoomGenerator(this);
                mixedRoomGenerator.GenerateRooms(roomSizeBias);
                break;
            case RoomGeneratorType.Middle:
                MiddleRoomGenerator middleRoomGenerator = new MiddleRoomGenerator(this);
                middleRoomGenerator.GenerateRooms(roomSizeBias);
                break;
            default:
                throw new ArgumentException("Invalid room generator type");
        }
    }

    

    public void GenerateCorridors()
    {
        switch (_corridorGeneratorType)
        {
            case CorridorGeneratorType.All:
                AllCorridorGenerator allCorridorGenerator = new AllCorridorGenerator(this);
                allCorridorGenerator.GenerateCorridors();
                break;
            case CorridorGeneratorType.Ring:
                RingCorridorGenerator ringCorridorGenerator = new RingCorridorGenerator(this);
                ringCorridorGenerator.GenerateCorridors();
                break;
            default:
                throw new ArgumentException("Invalid corridor generator type");
        }
    }

    /// <summary>
    /// Prints out all the nodes on the dungeon floor in a string format that can be somewhat understood.
    /// </summary>
    /// <returns>Returns a string of the entire dungeon floor.</returns>
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder("", Size.Height * Size.Width);

        bool squareDividers = false;

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