namespace DungeonMaker;

/// <summary>
/// A basic room of minimum size 4x4.
/// </summary>
class BasicRoom : Room
{
    public BasicRoom(Position initPosition, Size size) : base(initPosition, size)
    {
        if (size.Height < 4 || size.Width < 4) throw new Exception("Room size too small for a basic room.");
    }
}