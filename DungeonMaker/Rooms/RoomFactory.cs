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

class RoomFactory ()
{
    /// <summary>
    /// Generates the size of a room to be in this node square, with a bias.
    /// </summary>
    /// <param name="roomSizeBias">Bias the size of this room to smaller or larger numbers, or have it fixed.</param>
    /// <param name="minSize">The minimum size of this room.</param>
    /// <param name="maxSize">The maximum size of this room.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Throws an exception if an invalid room size bias value is provided.</exception>
    /// <exception cref="Exception">If it's not possible to generate the room within this square is throws an exception.</exception>
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

    /// <summary>
    /// Generate the initial position of this room.
    /// </summary>
    /// <param name="nodeSquareSize">The side of the node square to place this room inside.</param>
    /// <param name="roomSize">The size of the room to be placed.</param>
    /// <returns>Returns the coordinates of the top left node of this room.</returns>
    /// <exception cref="Exception">Throws an exception if it is unable to find a suitable position for the room.</exception>
    private Position GenerateInitPosition(Size nodeSquareSize, Size roomSize)
    {
        Random rnd = new Random();

        int maxInitPosRows = nodeSquareSize.Height - 4 + 2 - roomSize.Height + 1;
        if (maxInitPosRows < 2) throw new Exception("Not enough space to place the square on the Y axis");
        int initPosRows = rnd.Next(2, maxInitPosRows);

        int maxInitPosCols = nodeSquareSize.Width - 4 + 2 - roomSize.Width + 1;
        if (maxInitPosCols < 2) throw new Exception("Not enough space to place the square on the X axis");
        int initPosCols = rnd.Next(2, maxInitPosCols);

        return new Position(initPosRows, initPosCols);
    }

    /// <summary>
    /// Makes a room object and returns it.
    /// </summary>
    /// <param name="roomType">The type of room to generate.</param>
    /// <param name="nodeSquareSize">The size of the node square to generate the room in.</param>
    /// <param name="roomSizeBias">The bias for what the room size will be.</param>
    /// <returns>Returns the newly created room object.</returns>
    /// <exception cref="NotSupportedException">Throws a not supported exception if an invalid room type is provided.</exception>
    public Room MakeRoom(RoomType roomType, Size nodeSquareSize, RoomSizeBias roomSizeBias = RoomSizeBias.Fixed)
    {
        // Generate the room
        Room room;
        Size roomSize;
        Position initPosition;
        switch(roomType)
        {
            case RoomType.Basic:
                roomSize = GenerateRoomSize(roomSizeBias, 4, nodeSquareSize.Height - 4);
                initPosition = GenerateInitPosition(nodeSquareSize, roomSize);
                room = new BasicRoom(initPosition, roomSize);
                break;
            case RoomType.PointofInterest:
                roomSize = new Size(1, 1);
                initPosition = GenerateInitPosition(nodeSquareSize, roomSize);
                room = new PointofInterest(initPosition);
                break;
            case RoomType.Ring:
                roomSize = GenerateRoomSize(roomSizeBias, 4, nodeSquareSize.Height - 4);
                initPosition = GenerateInitPosition(nodeSquareSize, roomSize);
                room = new RingRoom(initPosition, roomSize);
                break;
            default:
                throw new NotSupportedException("Room type not supported");
        }

        // Run necessary methods to instantiate
        room.GenerateRoomNodes();

        return room;
    }
}