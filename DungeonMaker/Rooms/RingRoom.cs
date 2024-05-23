namespace DungeonMaker;

/// <summary>
/// A basic room of minimum size 4x4.
/// </summary>
class RingRoom : Room
{
    public RingRoom(Position initPosition, Size size) : base(initPosition, size)
    {
        if (size.Height < 4 || size.Width < 4) throw new Exception("Room size too small for a ring room.");
    }

    public override void GenerateRoomNodes()
    {
        for (int row = 0; row < Size.Height; row++)
        {
            for (int col = 0; col < Size.Width; col++)
            {
                Node newNode = new Node(new Position(row, col));
                if (row == 0 || row == Size.Height - 1 ||
                    col == 0 || col == Size.Width - 1)
                {
                    newNode.Content = "R";
                }
                _grid[row, col] = newNode;
            }
        }
    }
}