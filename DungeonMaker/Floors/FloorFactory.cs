namespace DungeonMaker;

enum FloorType
{
    AllSquares,
    ClosedCorners,
    Hollow,
    Beetle
    //ClosedSpread  // picks a corner, closes it, and spreads randomly in rows and cols anything from an L to a triangle shape
}

class FloorFactory
{
    /// <summary>
    /// Generates rooms in every node square.
    /// </summary>
    /// <param name="floor">The floor to generate rooms on.</param>
    /// <param name="roomGeneratorType">The type of rooms to be generated.</param>
    /// <param name="roomSizeBias">What size these rooms will be biased toward.</param>
    /// <exception cref="NotSupportedException">If an invalid room generator type is supplied, a not supported exception will be thrown.</exception>
    public void GenerateRooms(Floor floor, RoomGeneratorType roomGeneratorType, RoomSizeBias roomSizeBias = RoomSizeBias.Any)
    {
        switch (roomGeneratorType)
        {
            case RoomGeneratorType.All:
                AllRoomGenerator allRoomGenerator = new AllRoomGenerator(floor);
                allRoomGenerator.GenerateRooms(roomSizeBias);
                break;
            case RoomGeneratorType.Edge:
                EdgeRoomGenerator edgeRoomGenerator = new EdgeRoomGenerator(floor);
                edgeRoomGenerator.GenerateRooms(roomSizeBias);
                break;
            case RoomGeneratorType.Mixed:
                MixedRoomGenerator mixedRoomGenerator = new MixedRoomGenerator(floor);
                mixedRoomGenerator.GenerateRooms(roomSizeBias);
                break;
            case RoomGeneratorType.Middle:
                MiddleRoomGenerator middleRoomGenerator = new MiddleRoomGenerator(floor);
                middleRoomGenerator.GenerateRooms(roomSizeBias);
                break;
            default:
                throw new NotSupportedException("Invalid room generator type");
        }
    }

    /// <summary>
    /// Generates corridors between the generated rooms.
    /// </summary>
    /// <param name="floor">The floor to generate corridors on.</param>
    /// <param name="corridorGeneratorType">The types of corridors to be generated.</param>
    /// <exception cref="NotSupportedException">If an invalid corridor generator type is supplied, a not supported exception will be thrown.</exception>
    public void GenerateCorridors(Floor floor, CorridorGeneratorType corridorGeneratorType)
    {
        switch (corridorGeneratorType)
        {
            case CorridorGeneratorType.All:
                AllCorridorGenerator allCorridorGenerator = new AllCorridorGenerator(floor);
                allCorridorGenerator.GenerateCorridors();
                break;
            case CorridorGeneratorType.Ring:
                RingCorridorGenerator ringCorridorGenerator = new RingCorridorGenerator(floor);
                ringCorridorGenerator.GenerateCorridors();
                break;
            default:
                throw new NotSupportedException("Invalid corridor generator type");
        }
    }

    /// <summary>
    /// Makes a floor object, and generates its rooms and corridors.
    /// </summary>
    /// <param name="floorType">The type of floor to be created.</param>
    /// <param name="dimensions">How many node squares tall and wide this floor will be.</param>
    /// <param name="nodeSquareSize">The size of each node square.</param>
    /// <param name="roomGeneratorType">The type of generator to be used for creating rooms.</param>
    /// <param name="roomSizeBias">Bias the generator to a certain size of room.</param>
    /// <param name="corridorGeneratorType">The type of generator to be used for creating corridors between rooms.</param>
    /// <returns>Returns the newly created floor object.</returns>
    /// <exception cref="NotSupportedException">Throws a not supported exception if an invalid floor type is supplied.</exception>
    public Floor MakeFloor(
        FloorType floorType,
        Size dimensions,
        int nodeSquareSize,
        RoomGeneratorType roomGeneratorType = RoomGeneratorType.All,
        RoomSizeBias roomSizeBias = RoomSizeBias.Any,
        CorridorGeneratorType corridorGeneratorType = CorridorGeneratorType.All
    )
    {
        // Generate the floor
        Floor floor = floorType switch
        {
            FloorType.AllSquares => new AllSquaresFloor(dimensions, nodeSquareSize),
            FloorType.Beetle => new BeetleFloor(dimensions, nodeSquareSize),
            FloorType.ClosedCorners => new ClosedCornersFloor(dimensions, nodeSquareSize),
            FloorType.Hollow => new HollowFloor(dimensions, nodeSquareSize),
            _ => throw new NotSupportedException("Floor type not supported"),
        };

        // Run necessary methods to instantiate
        GenerateRooms(floor, roomGeneratorType, roomSizeBias);
        GenerateCorridors(floor, corridorGeneratorType);

        return floor;
    }
}