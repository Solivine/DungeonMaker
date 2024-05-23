namespace DungeonMaker;

/// <summary>
/// A point of interest is a 1x1 room.
/// </summary>
class PointofInterest(Position pos) : Room(pos, new Size(1, 1))
{
}