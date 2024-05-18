using System.Text;

namespace DungeonMaker;

public enum NodeSquareSide
{
    Up,
    Down,
    Left,
    Right
}

public class NodeSquareSides
{

    private bool _upSideOpen = true;  // rename to edge?
    private Node? _upSide;
    private bool _downSideOpen = true;
    private Node? _downSide;
    private bool _leftSideOpen = true;
    private Node? _leftSide;
    private bool _rightSideOpen = true;
    private Node? _rightSide;

    public bool IsSideOpen(NodeSquareSide side)
    {
        return side switch
        {
            NodeSquareSide.Up => _upSideOpen,
            NodeSquareSide.Down => _downSideOpen,
            NodeSquareSide.Left => _leftSideOpen,
            NodeSquareSide.Right => _rightSideOpen,
            _ => throw new ArgumentException("Invalid side requested!"),
        };

    }

    public void SetSideOpen(NodeSquareSide side, bool isOpen)
    {
        switch(side)
        {
            case NodeSquareSide.Up:
                _upSideOpen = isOpen;
                break;
            case NodeSquareSide.Down:
                _downSideOpen = isOpen;
                break;
            case NodeSquareSide.Left:
                _leftSideOpen = isOpen;
                break;
            case NodeSquareSide.Right:
                _rightSideOpen = isOpen;
                break;
            default:
                throw new ArgumentException("Invalid side to be set");
        }
    }

    public bool IsSideAssignedToNode(NodeSquareSide side)
    {
        return side switch
        {
            NodeSquareSide.Up => _upSide != null,
            NodeSquareSide.Down => _downSide != null,
            NodeSquareSide.Left => _leftSide != null,
            NodeSquareSide.Right => _rightSide != null,
            _ => throw new ArgumentException("Invalid side requested!"),
        };
    }

    public Node GetNode(NodeSquareSide side)
    {
        if (IsSideAssignedToNode(side))
        {
            return side switch
            {
                NodeSquareSide.Up => _upSide!,
                NodeSquareSide.Down => _downSide!,
                NodeSquareSide.Left => _leftSide!,
                NodeSquareSide.Right => _rightSide!,
                _ => throw new ArgumentException("Invalid side requested!"),
            };
        }

        throw new Exception("Tried to get a node that didn't exist!");
    }

    public void SetSideToNode(NodeSquareSide side, Node node)
    {
        if (IsSideAssignedToNode(side))
        {
            throw new Exception("Side already assigned to node!");
        }

        switch(side)
        {
            case NodeSquareSide.Up:
                _upSide = node;
                break;
            case NodeSquareSide.Down:
                _downSide = node;
                break;
            case NodeSquareSide.Left:
                _leftSide = node;
                break;
            case NodeSquareSide.Right:
                _rightSide = node;
                break;
            default:
                throw new ArgumentException("Invalid side to be set");
        }
    }

    public Node? GetNodeFromSide(NodeSquareSide side)
    {
        return side switch
        {
            NodeSquareSide.Up => _upSide,
            NodeSquareSide.Down => _downSide,
            NodeSquareSide.Left => _leftSide,
            NodeSquareSide.Right => _rightSide,
            _ => throw new ArgumentException("Invalid side to be got"),
        };
    }

    public bool IsUpSideOpen
    {
        get => _upSideOpen;
        set => _upSideOpen = value;
    }

    public Node? UpSide
    {
        get => _upSide;
        set => _upSide = value;
    }

    public bool IsDownSideOpen
    {
        get => _downSideOpen;
        set => _downSideOpen = value;
    }

    public Node? DownSide
    {
        get => _downSide;
        set => _downSide = value;
    }

    public bool IsLeftSideOpen
    {
        get => _leftSideOpen;
        set => _leftSideOpen = value;
    }

    public Node? LeftSide
    {
        get => _leftSide;
        set => _leftSide = value;
    }

    public bool IsRightSideOpen
    {
        get => _rightSideOpen;
        set => _rightSideOpen = value;
    }

    public Node? RightSide
    {
        get => _rightSide;
        set => _rightSide = value;
    }
}

/// <summary>
/// A square contains a room or a point of interest.
/// </summary>
public class NodeSquare
{
    private readonly Node[,] _grid;
    private Room? _room;  // This node square can *only* be connected to if it has a room of some kind.
    private NodeSquareSides _nodeSquareSides = new NodeSquareSides();
    private Position _pos;

    public NodeSquare(Position pos, int squareSize)
    {
        _grid = new Node[squareSize, squareSize];
        _pos = pos;

        int maxRows = _grid.GetLength(0);
        int maxCols = _grid.GetLength(1);
        for (int row = 0; row < maxRows; row++)
        {
            for (int col = 0; col < maxCols; col++)
            {
                _grid[row, col] = new Node(row, col);
            }
        }
    }

    public Position Position
    {
        get => _pos;
    }

    public NodeSquareSides Sides
    {
        get => _nodeSquareSides;
    }

    public bool HasRoom
    {
        get => _room != null;
    }

    public Room? GetRoom
    {
        get => _room;
    }

    public Size Size
    {
        get => new Size(_grid.GetLength(0), _grid.GetLength(1));
    }

    public Node GetNode(Position pos)
    {
        return _grid[pos.Row, pos.Col];
    }

    public bool IsAccessible
    {
        get => Sides.IsUpSideOpen || Sides.IsDownSideOpen || Sides.IsLeftSideOpen || Sides.IsRightSideOpen;
    }

    public void CloseSide(NodeSquareSide side)
    {
        switch(side)
        {
            case NodeSquareSide.Up:
                Sides.SetSideOpen(NodeSquareSide.Up, false);
                break;
            case NodeSquareSide.Down:
                Sides.SetSideOpen(NodeSquareSide.Down, false);
                break;
            case NodeSquareSide.Left:
                Sides.SetSideOpen(NodeSquareSide.Left, false);
                break;
            case NodeSquareSide.Right:
                Sides.SetSideOpen(NodeSquareSide.Right, false);
                break;
            default:
                throw new Exception("Invalid side to be closed");
        }
    }

    public void CloseAllSides()
    {
        CloseSide(NodeSquareSide.Up);
        CloseSide(NodeSquareSide.Down);
        CloseSide(NodeSquareSide.Left);
        CloseSide(NodeSquareSide.Right);
    }

    private Size GenerateRoomSize(RoomSizeBias roomSizeBias, int minSize = 4, int maxSize = 4)
    {
        if (maxSize >= minSize)
        {
            Random rnd = new Random();

            // 1. Generate Room Size
            int width = minSize;
            int height = minSize;

            int width1 = rnd.Next(minSize, maxSize);
            int width2 = rnd.Next(minSize, maxSize);
            int width3 = rnd.Next(minSize, maxSize);
            int height1 = rnd.Next(minSize, maxSize);
            int height2 = rnd.Next(minSize, maxSize);
            int height3 = rnd.Next(minSize, maxSize);

            switch(roomSizeBias)
            {
                case RoomSizeBias.Smallest:
                    width = minSize;
                    height = minSize;
                    break;
                case RoomSizeBias.Small:
                    width = Math.Min(width1, width2);
                    height = Math.Min(height1, height2);
                    break;
                case RoomSizeBias.ExtraSmall:
                    width = Math.Min(width3, Math.Min(width1, width2));
                    height = Math.Min(height3, Math.Min(height1, height2));
                    break;
                case RoomSizeBias.Medium:
                    width = (int) (width1 + width2 / 2);
                    height = (int) (height1 + height2 / 2);
                    break;
                case RoomSizeBias.ExtraMedium:
                    width = (int) (width1 + width2 + width3 / 3);
                    height = (int) (height1 + height2 + height3 / 3);
                    break;
                case RoomSizeBias.Large:
                    width = Math.Max(width1, width2);
                    height = Math.Max(height1, height2);
                    break;
                case RoomSizeBias.ExtraLarge:
                    width = Math.Max(width3, Math.Max(width1, width2));
                    height = Math.Max(height3, Math.Max(height1, height2));
                    break;
                case RoomSizeBias.Largest:
                    width = maxSize;
                    height = maxSize;
                    break;
                case RoomSizeBias.Any:
                    width = width1;
                    height = height1;
                    break;
                case RoomSizeBias.Fixed:
                    // Do nothing.
                    break;
                default:
                    throw new ArgumentException("Room size bias value not implemented");
            }

            return new Size(height, width);
        }

        throw new Exception(String.Format("Node square too small to fit a room of any size with Min Size {0} when the Max Size is {1}", minSize, maxSize));        
    }

    private Node FindCorridorEndNode(NodeSquareSide nodeSquareSide, Position originRoomNodePosition)
    {
        Node endNode = nodeSquareSide switch
        {
            NodeSquareSide.Up => GetNode(new Position(0, originRoomNodePosition.Col)),
            NodeSquareSide.Down => GetNode(new Position(Size.Height - 1, originRoomNodePosition.Col)),
            NodeSquareSide.Left => GetNode(new Position(originRoomNodePosition.Row, 0)),
            NodeSquareSide.Right => GetNode(new Position(originRoomNodePosition.Row, Size.Width - 1)),
            _ => throw new ArgumentException("Invalid node square side for finding corridor end node"),
        };
        //Console.WriteLine("Setting side {0} with a corridor end node", nodeSquareSide);
        Sides.SetSideToNode(nodeSquareSide, endNode);
        return endNode;
    }

    public void DrawCorridorFromRoom(NodeSquareSide nodeSquareSide, Node startNode)
    {
        //Console.WriteLine("Draw corridor from room");
        Pathfinder corridorPathfinder = new CorridorPathfinder();
        Node endNode = FindCorridorEndNode(nodeSquareSide, startNode.Position);

        // While active node is not end node
        Node previousNode = GetNode(new Position(0, 0));
        Node activeNode = startNode;
        while (!activeNode.Position.Equals(endNode.Position))
        {
            Position nextNodePosition = corridorPathfinder.NextNode(previousNode, activeNode, endNode);
            previousNode = activeNode;
            activeNode = GetNode(nextNodePosition);

            //Console.WriteLine(String.Format("Prev {0}, {1} Act {2}, {3} End {4}, {5}", previousNode.Position.Col, previousNode.Position.Row, activeNode.Position.Col, activeNode.Position.Row, endNode.Position.Col, endNode.Position.Row));

            if (activeNode.ContentType == "Wall")
            {
                activeNode.Content = "C";
            }
        }
    }

    public Node FindAndSetCorridorStartNode(Position neighbouringNodePosition)
    {
        //Console.WriteLine(String.Format("Finding corridor start node for {0}, {1}", neighbouringNodePosition.Col, neighbouringNodePosition.Row));
        Node node;
        NodeSquareSide nodeSquareSide;

        if (neighbouringNodePosition.Row == Size.Height - 1)
        {
            // Y was maxed out, therefore reset Y
            // This is upside
            node = GetNode(new Position(0, neighbouringNodePosition.Col));
            nodeSquareSide = NodeSquareSide.Up;
        }
        else if (neighbouringNodePosition.Row == 0)
        {
            // Y was minned out, therefore reset Y
            // This is downside
            node = GetNode(new Position(Size.Height - 1, neighbouringNodePosition.Col));
            nodeSquareSide = NodeSquareSide.Down;
        }
        else if (neighbouringNodePosition.Col == Size.Height - 1)
        {
            // X was maxed out, therefore reset X
            // This is leftside
            node = GetNode(new Position(neighbouringNodePosition.Row, 0));
            nodeSquareSide = NodeSquareSide.Left;
        }
        else if (neighbouringNodePosition.Col == 0)
        {
            // X was minned out, therefore reset X
            // This is rightside
            node = GetNode(new Position(neighbouringNodePosition.Row, Size.Height - 1));
            nodeSquareSide = NodeSquareSide.Right;
        }
        else
        {
            throw new Exception("Can't find corridor start node");
        }

        node.Content = "C";
        //Console.WriteLine("Setting side {0} with a corridor start node at {1}, {2}", nodeSquareSide, node.Position.Row, node.Position.Col);
        Sides.SetSideToNode(nodeSquareSide, node);

        return node;
    }

    public void DrawCorridorToRoom(Node startNode, Node endNode)
    {
        //Console.WriteLine("Draw corridor to room");
        Pathfinder corridorPathfinder = new CorridorPathfinder();
        //Node endNode = FindCorridorEndNode(nodeSquareSide, startNode.Position);

        // While active node is not end node
        Node previousNode = GetNode(new Position(0, 0));
        Node activeNode = startNode;
        while (!activeNode.Position.Equals(endNode.Position))
        {
            Position nextNodePosition = corridorPathfinder.NextNode(previousNode, activeNode, endNode);
            previousNode = activeNode;
            activeNode = GetNode(nextNodePosition);

            //Console.WriteLine(String.Format("Prev {0}, {1} Act {2}, {3} End {4}, {5}", previousNode.Position.Row, previousNode.Position.Col, activeNode.Position.Row, activeNode.Position.Col, endNode.Position.Row, endNode.Position.Col));

            if (activeNode.ContentType == "Wall")
            {
                activeNode.Content = "C";
            }
        }
    }

    private void DrawRoom()
    {
        if (_room != null)
        {
            for (int row = _room.Position.Row; row < _room.Size.Height + _room.Position.Row; row++)
            {
                for (int col = _room.Position.Col; col < _room.Size.Width + _room.Position.Col; col++)
                {
                    GetNode(new Position(row, col)).Content = "R";
                }
            }
        }
        else
        {
            throw new Exception("Draw method called before room assigned");
        }
    }

    public void GenerateRoom(RoomType roomType, RoomSizeBias roomSizeBias = RoomSizeBias.Fixed)
    {
        Random rnd = new Random();

        int maxInitPosRows;
        int maxInitPosCols;
        int initPosRows;
        int initPosCols;
        Size roomSize;
        switch(roomType)
        {
            case RoomType.Basic:
                roomSize = GenerateRoomSize(roomSizeBias, 4, Size.Height - 4);
                maxInitPosRows = Size.Height - 4 + 2 - roomSize.Height + 1;
                if (maxInitPosRows < 2) throw new Exception("Not enough space to place the square on the Y axis");
                initPosRows = rnd.Next(2, maxInitPosRows);

                maxInitPosCols = Size.Width - 4 + 2 - roomSize.Width + 1;
                if (maxInitPosCols < 2) throw new Exception("Not enough space to place the square on the X axis");
                initPosCols = rnd.Next(2, maxInitPosCols);

                _room = new BasicRoom(initPosRows, initPosCols, roomSize.Height, roomSize.Width);
                //Console.WriteLine(String.Format("Room assigned with pos {0}, {1} and size {2}, {3}", initPosX, initPosY, roomSize.Col, roomSize.Row));
                DrawRoom();
                break;
            case RoomType.PointofInterest:
                maxInitPosRows = Size.Height - 4 + 2 - 1 + 1;
                if (maxInitPosRows < 2) throw new Exception("Not enough space to place the square on the Y axis");
                initPosRows = rnd.Next(2, maxInitPosRows);

                maxInitPosCols = Size.Width - 4 + 2 - 1 + 1;
                if (maxInitPosCols < 2) throw new Exception("Not enough space to place the square on the X axis");
                initPosCols = rnd.Next(2, maxInitPosCols);

                _room = new PointofInterest(initPosRows, initPosCols);
                DrawRoom();
                break;
            default:
                throw new NotImplementedException("Room type not implemented");
        }
    }

    public string LineToString(int lineNumber)
    {
        StringBuilder sb = new StringBuilder("", Size.Width);
        for (int col = 0; col < Size.Width; col++)
        {
            sb.AppendFormat("{0},", GetNode(new Position(lineNumber, col)));
        }

        return sb.ToString();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder("", Size.Width);
        for (int row = 0; row < Size.Height; row++)
        {
            sb.Append("\n{");

            for (int col = 0; col < Size.Width; col++)
            {
                sb.AppendFormat("{0},", GetNode(new Position(row, col)));
            }
            
            sb.Append('}');
        }

        sb.Replace(",}", "}").Remove(0, 1);
        return sb.ToString();
    }
}