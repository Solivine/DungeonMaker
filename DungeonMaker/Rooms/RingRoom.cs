namespace DungeonMaker;

/// <summary>
/// A basic room of minimum size 4x4.
/// </summary>
class RingRoom : Room
{
    public RingRoom(int initPosRow, int initPosCol, int height, int width) : base(initPosRow, initPosCol, height, width)
    {
        if (height < 4 || width < 4) throw new Exception("Room size too small for a ring room.");
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