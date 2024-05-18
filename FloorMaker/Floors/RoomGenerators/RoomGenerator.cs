namespace DungeonMaker;

public enum RoomGeneratorType
{
    All,
    Edge,
    Mixed,
    Middle
}

public abstract class RoomGenerator(Floor floor)
{
    protected readonly Floor _floor = floor;

    public virtual void GenerateRooms(RoomSizeBias roomSizeBias)
    {
        throw new NotImplementedException("Method GenerateRooms must be implemented in subclasses");
    }
}