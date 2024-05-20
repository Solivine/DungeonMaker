namespace DungeonMaker;

/// <summary>
/// A floor type where the 4 corner rooms of the floor do not generate anything.
/// </summary>
class ClosedCornersFloor : Floor
{
    public ClosedCornersFloor(Size size, int nodeSquareSize, RoomGeneratorType roomGeneratorType = RoomGeneratorType.Edge, CorridorGeneratorType corridorGeneratorType = CorridorGeneratorType.All) : base(size, nodeSquareSize, roomGeneratorType, corridorGeneratorType)
    {
        if (size.Height < 3 || size.Width < 3) throw new Exception("Invalid floor size (too small) for closed corners");

        // 1c. Close Sides
        CloseSides();
    }

    protected override void CloseSides()
    {
        for (int row = 0; row < Size.Height; row++)
        {
            for (int col = 0; col < Size.Width; col++)
            {
                NodeSquare nodeSquare = GetNodeSquare(new Position(row, col));

                // 1. If a bordering room, close the edges
                if (row == 0)
                {
                    nodeSquare.CloseSide(NodeSquareSide.Up);
                    nodeSquare.CloseSide(NodeSquareSide.Left);
                    nodeSquare.CloseSide(NodeSquareSide.Right);
                }
                if (col == 0)
                {
                    nodeSquare.CloseSide(NodeSquareSide.Left);
                    nodeSquare.CloseSide(NodeSquareSide.Up);
                    nodeSquare.CloseSide(NodeSquareSide.Down);
                }
                if (row == Size.Height - 1)
                {
                    nodeSquare.CloseSide(NodeSquareSide.Down);
                    nodeSquare.CloseSide(NodeSquareSide.Left);
                    nodeSquare.CloseSide(NodeSquareSide.Right);
                }
                if (col == Size.Width - 1)
                {
                    nodeSquare.CloseSide(NodeSquareSide.Right);
                    nodeSquare.CloseSide(NodeSquareSide.Up);
                    nodeSquare.CloseSide(NodeSquareSide.Down);
                }
            }
        }

        // 2. Close off corner rooms and the node squares they border
        NodeSquare topLeft = GetNodeSquare(new Position(0, 0));
        topLeft.CloseAllSides();

        NodeSquare bottomLeft = GetNodeSquare(new Position(Size.Height - 1, 0));
        bottomLeft.CloseAllSides();

        NodeSquare topRight = GetNodeSquare(new Position(0, Size.Width - 1));
        topRight.CloseAllSides();

        NodeSquare bottomRight = GetNodeSquare(new Position(Size.Height - 1, Size.Width - 1));
        bottomRight.CloseAllSides();
    }
}