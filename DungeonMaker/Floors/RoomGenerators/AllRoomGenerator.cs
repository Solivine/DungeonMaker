namespace DungeonMaker;

/// <summary>
/// Generates rooms in every possible square.
/// </summary>
/// <param name="floor"></param>
public class AllRoomGenerator(Floor floor) : RoomGenerator(floor)
{
    public override void GenerateRooms(RoomSizeBias roomSizeBias)
    {
        RoomFactory roomFactory = new RoomFactory();

        foreach (NodeSquare nodeSquare in base._floor.Grid)
        {
            // For each node square in the grid, generate a room inside it
            if (nodeSquare.IsAccessible) nodeSquare.Room = roomFactory.MakeRoom(RoomType.Basic, nodeSquare.Size);
        }
    }
}