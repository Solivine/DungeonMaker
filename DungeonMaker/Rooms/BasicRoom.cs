namespace DungeonMaker;

/// <summary>
/// A basic room of minimum size 4x4.
/// </summary>
class BasicRoom : Room
{
    public BasicRoom(int initPosRow, int initPosCol, int height, int width) : base(initPosRow, initPosCol, height, width)
    {
        if (height < 4 || width < 4) throw new Exception("Room size too small for a basic room.");
    }
}