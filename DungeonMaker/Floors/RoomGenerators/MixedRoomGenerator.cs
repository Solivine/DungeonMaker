namespace DungeonMaker;

/// <summary>
/// Generates a mix of rooms and points of interest in all possible squares.
/// </summary>
/// <param name="floor"></param>
public class MixedRoomGenerator(Floor floor) : RoomGenerator(floor)
{
    public override void GenerateRooms(RoomSizeBias roomSizeBias)
    {
        Random rnd = new Random();

        // 1. How many points of interest do you want?
        int totalSquares = _floor.Size.Height * _floor.Size.Width;
        int totalPointsOfInterest = rnd.Next(2, (int) totalSquares - 2);
        int totalBasicRooms = totalSquares - totalPointsOfInterest;

        // 2. Randomly place these throughout the grid
        int currentPointsOfInterest = 0;
        int currentBasicRooms = 0;

        RoomFactory roomFactory = new RoomFactory();

        foreach (NodeSquare nodeSquare in _floor.Grid)
        {
            if (nodeSquare.IsAccessible)
            {
                // For each node square in the grid, generate a room inside it
                bool isBasicRoom = rnd.Next(0, 2) == 0;
                if (isBasicRoom && currentBasicRooms < totalBasicRooms)
                {
                    nodeSquare.Room = roomFactory.MakeRoom(RoomType.Basic, nodeSquare.Size, roomSizeBias);
                    currentBasicRooms++;
                }
                else
                {
                    nodeSquare.Room = roomFactory.MakeRoom(RoomType.PointofInterest, nodeSquare.Size);
                    currentPointsOfInterest++;
                }
            }
        }
    }
}