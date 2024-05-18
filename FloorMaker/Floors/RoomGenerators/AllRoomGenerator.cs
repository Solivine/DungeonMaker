namespace DungeonMaker;

public class AllRoomGenerator(Floor floor) : RoomGenerator(floor)
{
    public override void GenerateRooms(RoomSizeBias roomSizeBias)
    {
        foreach (NodeSquare nodeSquare in base._floor.Grid)
        {
            // For each node square in the grid, generate a room inside it
            if (nodeSquare.IsAccessible) nodeSquare.GenerateRoom(RoomType.Basic, roomSizeBias);
        }
    }
}