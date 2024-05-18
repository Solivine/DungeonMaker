namespace DungeonMaker;

class AllSquaresFloor : Floor
{
    public AllSquaresFloor(Size size, int nodeSquareSize, RoomGeneratorType roomGeneratorType = RoomGeneratorType.All, CorridorGeneratorType corridorGeneratorType = CorridorGeneratorType.All) : base(size, nodeSquareSize, roomGeneratorType, corridorGeneratorType)
    {
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
                if (row == 0)
                {
                    nodeSquare.CloseSide(NodeSquareSide.Up);
                }
                if (col == 0)
                {
                    nodeSquare.CloseSide(NodeSquareSide.Left);
                }
                if (row == Size.Height - 1)
                {
                    nodeSquare.CloseSide(NodeSquareSide.Down);
                }
                if (col == Size.Width - 1)
                {
                    nodeSquare.CloseSide(NodeSquareSide.Right);
                }
            }
        }
    }
}