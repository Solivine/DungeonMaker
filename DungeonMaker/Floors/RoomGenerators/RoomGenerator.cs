namespace DungeonMaker;

public enum RoomGeneratorType
{
    All,
    Edge,
    Mixed,
    Middle
}

/// <summary>
/// Base parent class for room generators. It contains the floor that all children must use.
/// </summary>
/// <param name="floor">The floor for rooms to be generated on.</param>
public abstract class RoomGenerator(Floor floor)
{
    protected readonly Floor _floor = floor;

    public virtual void GenerateRooms(RoomSizeBias roomSizeBias)
    {
        throw new NotImplementedException("Method GenerateRooms must be implemented in subclasses");
    }
}