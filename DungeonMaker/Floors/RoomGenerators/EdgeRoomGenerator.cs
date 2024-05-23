namespace DungeonMaker;

/// <summary>
/// Generates rooms only around the edge of the map, and points of interest in the middle.
/// </summary>
/// <param name="floor"></param>
public class EdgeRoomGenerator(Floor floor) : RoomGenerator(floor)
{
    public override void GenerateRooms(RoomSizeBias roomSizeBias)
    {
        RoomFactory roomFactory = new RoomFactory();

        foreach (NodeSquare nodeSquare in _floor.Grid)
        {
            // For each node square in the grid, generate a room inside it
            if (nodeSquare.IsAccessible)
            {
                if (
                    nodeSquare.Position.Row == 0 || nodeSquare.Position.Row == _floor.Size.Height - 1 ||
                    nodeSquare.Position.Col == 0 || nodeSquare.Position.Col == _floor.Size.Width - 1
                )
                {
                    nodeSquare.Room = roomFactory.MakeRoom(RoomType.Basic, nodeSquare.Size, roomSizeBias);
                }
                else
                {
                    nodeSquare.Room = roomFactory.MakeRoom(RoomType.PointofInterest, nodeSquare.Size);
                }
            }
        }
    }
}