namespace DungeonMaker;

/// <summary>
/// A floor type where the top and bottom rows do not generate rooms except for the corners. Like a |---| shape.
/// </summary>
class BeetleFloor : Floor
{
    public BeetleFloor(Size size, int nodeSquareSize) : base(size, nodeSquareSize)
    {
        if (size.Width < 3) throw new Exception("Invalid floor size (width too small) for beetle");
        
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

                // Beetle shape
                if (row == 0)
                {
                    // go along top row
                    if (col == 0)
                    {
                        nodeSquare.CloseSide(NodeSquareSide.Right);
                    }
                    else if (col == Size.Width - 1)
                    {
                        nodeSquare.CloseSide(NodeSquareSide.Left);
                    }
                    else
                    {
                        nodeSquare.CloseAllSides();
                    }
                }

                if (row == Size.Height - 1)
                {
                    // go along bottom row
                    if (col == 0)
                    {
                        nodeSquare.CloseSide(NodeSquareSide.Right);
                    }
                    else if (col == Size.Width - 1)
                    {
                        nodeSquare.CloseSide(NodeSquareSide.Left);
                    }
                    else
                    {
                        nodeSquare.CloseAllSides();
                    }
                }
            }
        }
    }
}