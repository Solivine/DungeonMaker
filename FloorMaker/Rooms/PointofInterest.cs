namespace DungeonMaker;

/// <summary>
/// A point of interest is a 1x1 room.
/// </summary>
class PointofInterest(int row, int col) : Room(row, col, 1, 1)
{
}