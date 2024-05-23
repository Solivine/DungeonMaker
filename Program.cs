namespace DungeonMaker;

class Program
{
    static void Main(string[] args)
    {
        FloorFactory floorFactory = new FloorFactory();

        // Thunderwave Cave
        Floor thunderwaveCave = floorFactory.MakeFloor(FloorType.AllSquares, new Size(3, 3), 12, roomGeneratorType: RoomGeneratorType.Mixed);

        // Monster house floor
        Floor monsterHouseFloor = floorFactory.MakeFloor(FloorType.AllSquares, new Size(1, 1), 36, roomSizeBias: RoomSizeBias.Largest);

        // Beetle shape floor
        Floor beetleFloor = floorFactory.MakeFloor(FloorType.Beetle, new Size(3, 3), 12);

        // Sky Summit
        Floor skySummit = floorFactory.MakeFloor(FloorType.ClosedCorners, new Size(4, 5), 12);

        // Mt. Steel
        Floor mtSteelVar1 = floorFactory.MakeFloor(FloorType.AllSquares, new Size(2, 4), 12, roomGeneratorType: RoomGeneratorType.Mixed, corridorGeneratorType: CorridorGeneratorType.Ring);

        // Hollow Floor
        Floor hollowFloor = floorFactory.MakeFloor(FloorType.Hollow, new Size(3, 4), 12);

        // Straight Line
        Floor straightLine = floorFactory.MakeFloor(FloorType.AllSquares, new Size(1, 5), 12);

        // Outer Square
        Floor outerSquare = floorFactory.MakeFloor(FloorType.AllSquares, new Size(4, 6), 12, roomGeneratorType: RoomGeneratorType.Middle);

        // Mt. Steel
        Floor mtSteelVar2 = floorFactory.MakeFloor(FloorType.AllSquares, new Size(4, 4), 10, roomGeneratorType: RoomGeneratorType.Edge, corridorGeneratorType: CorridorGeneratorType.Ring);

        Console.WriteLine(mtSteelVar2.ToString());
    }

    private static void PrintNodeSquares(Floor dungeonfloor)
    {
        NodeSquare topLeft = dungeonfloor.GetNodeSquare(new Position(0, 0));
        Console.WriteLine("Top Left Node Square");
        Console.WriteLine(topLeft.ToString());
        Console.WriteLine(String.Format("Stats: [Up? {0} Down? {1} Left? {2} Right? {3}]", topLeft.Sides.IsUpSideOpen, topLeft.Sides.IsDownSideOpen, topLeft.Sides.IsLeftSideOpen, topLeft.Sides.IsRightSideOpen));
        Console.WriteLine(String.Format("Stats: []"));

        NodeSquare bottomLeft = dungeonfloor.GetNodeSquare(new Position(1, 0));
        Console.WriteLine("Bottom Left Node Square");
        Console.WriteLine(bottomLeft.ToString());
        Console.WriteLine(String.Format("Stats: [Up? {0} Down? {1} Left? {2} Right? {3}]", bottomLeft.Sides.IsUpSideOpen, bottomLeft.Sides.IsDownSideOpen, bottomLeft.Sides.IsLeftSideOpen, bottomLeft.Sides.IsRightSideOpen));
        Console.WriteLine(String.Format("Stats: [UpNode: {0}, {1} RightNode: {2}, {3}]", bottomLeft.Sides.UpSide!.Position.Row, bottomLeft.Sides.UpSide!.Position.Col, bottomLeft.Sides.RightSide!.Position.Row, bottomLeft.Sides.RightSide!.Position.Col));
    }
}
