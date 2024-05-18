namespace DungeonMaker;

/// <summary>
/// A point of interest is a 1x1 room.
/// </summary>
class BasicRoom(int initPosRow, int initPosCol, int height, int width) : Room(initPosRow, initPosCol, height, width)
{
}