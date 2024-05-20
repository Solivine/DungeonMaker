namespace DungeonMaker;

class Program
{
    static void Main(string[] args)
    {
        // Thunderwave Cave
        Floor thunderwaveCave = new AllSquaresFloor(new Size(3, 3), 12, RoomGeneratorType.Mixed);
        thunderwaveCave.GenerateRooms();
        thunderwaveCave.GenerateCorridors();

        // Monster house floor
        Floor monsterHouseFloor = new AllSquaresFloor(new Size(1, 1), 36);
        monsterHouseFloor.GenerateRooms(RoomSizeBias.Largest);

        // Beetle shape floor
        Floor beetleFloor = new BeetleFloor(new Size(3, 3), 12);
        beetleFloor.GenerateRooms();
        beetleFloor.GenerateCorridors();

        // Sky Summit
        Floor skySummit = new ClosedCornersFloor(new Size(4, 5), 12);
        skySummit.GenerateRooms();
        skySummit.GenerateCorridors();

        // Mt. Steel
        Floor mtSteelVar1 = new AllSquaresFloor(new Size(2, 4), 12, RoomGeneratorType.Mixed, CorridorGeneratorType.Ring);
        mtSteelVar1.GenerateRooms();
        mtSteelVar1.GenerateCorridors();

        // Hollow Floor
        Floor hollowFloor = new HollowFloor(new Size(3, 4), 12);
        hollowFloor.GenerateRooms();
        hollowFloor.GenerateCorridors();

        // Straight Line
        Floor straightLine = new AllSquaresFloor(new Size(1, 5), 12);
        straightLine.GenerateRooms();
        straightLine.GenerateCorridors();

        // Outer Square
        Floor outerSquare = new AllSquaresFloor(new Size(4, 6), 12, RoomGeneratorType.Middle);
        outerSquare.GenerateRooms();
        outerSquare.GenerateCorridors();

        // Mt. Steel
        Floor mtSteelVar2 = new AllSquaresFloor(new Size(4, 4), 10, RoomGeneratorType.All, CorridorGeneratorType.Ring);
        mtSteelVar2.GenerateRooms();
        mtSteelVar2.GenerateCorridors();

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
