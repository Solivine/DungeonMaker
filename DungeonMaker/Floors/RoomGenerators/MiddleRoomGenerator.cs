namespace DungeonMaker;

/// <summary>
/// Generates rooms in the centre squares of the floor, and points of interest around the edges.
/// The floor height and width must exceed 2 for this reason, or no rooms will generate.
/// </summary>
/// <param name="floor"></param>
public class MiddleRoomGenerator(Floor floor) : RoomGenerator(floor)
{
    public override void GenerateRooms(RoomSizeBias roomSizeBias)
    {
        if (_floor.Size.Height < 3 || _floor.Size.Width < 3) throw new Exception("Can't generate any middle rooms, this floor would be invalid!");

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
                    nodeSquare.GenerateRoom(RoomType.PointofInterest);
                }
                else
                {
                    nodeSquare.GenerateRoom(RoomType.Basic, roomSizeBias);
                }
            }
        }
    }
}