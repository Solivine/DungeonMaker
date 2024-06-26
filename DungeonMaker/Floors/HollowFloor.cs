namespace DungeonMaker;

/// <summary>
/// A floor type where squares in the middle do not generate any rooms.
/// </summary>
class HollowFloor : Floor
{
    public HollowFloor(Size size, int nodeSquareSize) : base(size, nodeSquareSize)
    {
        if (size.Height < 3 || size.Width < 3) throw new Exception("Invalid floor size (width too small) for hollow floor");
        
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

                // Border
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

                // Hollow shape
                if ((row != 0 && row != Size.Height - 1) && (col != 0 && col != Size.Width - 1))
                {
                    nodeSquare.CloseAllSides();
                }
            }
        }
    }
}