namespace DungeonMaker;

/// <summary>
/// Just connects all possible corridors assuming the sides are open.
/// This can technically be 'tricked' into other corridor generation types should the room generator method close sides (see sky summit example).
/// </summary>
/// <param name="floor"></param>
public class AllCorridorGenerator(Floor floor) : CorridorGenerator(floor)
{
    public override void GenerateCorridors()
    {
        // 1. Generate all corridors
        for (int row = 0; row < _floor.Size.Height; row++)
        {
            for (int col = 0; col < _floor.Size.Width; col++)
            {
                NodeSquare currentNodeSquare = _floor.GetNodeSquare(new Position(row, col));
                //Console.WriteLine(String.Format("Connecting all sides of node square {0}, {1} with maxRows {2} and maxCols {3}", row, col, Size.Height, Size.Width));
                //Console.WriteLine(String.Format("Up: {0}, Down: {1}, Left: {2}, Right: {3}", currentNodeSquare.Sides.IsSideAssignedToNode(NodeSquareSide.Up), currentNodeSquare.Sides.IsSideAssignedToNode(NodeSquareSide.Down), currentNodeSquare.Sides.IsSideAssignedToNode(NodeSquareSide.Left), currentNodeSquare.Sides.IsSideAssignedToNode(NodeSquareSide.Right)));
                
                // Connect all connections
                if (currentNodeSquare.Sides.IsUpSideOpen && !currentNodeSquare.Sides.IsSideAssignedToNode(NodeSquareSide.Up))
                {
                    NodeSquare targetNodeSquare = _floor.GetNodeSquare(new Position(row - 1, col));
                    if (currentNodeSquare.HasRoom && targetNodeSquare.HasRoom) GenerateCorridor(currentNodeSquare, targetNodeSquare);
                }
                if (currentNodeSquare.Sides.IsDownSideOpen && !currentNodeSquare.Sides.IsSideAssignedToNode(NodeSquareSide.Down))
                {
                    NodeSquare targetNodeSquare = _floor.GetNodeSquare(new Position(row + 1, col));
                    if (currentNodeSquare.HasRoom && targetNodeSquare.HasRoom) GenerateCorridor(currentNodeSquare, targetNodeSquare);
                }
                if (currentNodeSquare.Sides.IsLeftSideOpen && !currentNodeSquare.Sides.IsSideAssignedToNode(NodeSquareSide.Left))
                {
                    NodeSquare targetNodeSquare = _floor.GetNodeSquare(new Position(row, col - 1));
                    if (currentNodeSquare.HasRoom && targetNodeSquare.HasRoom) GenerateCorridor(currentNodeSquare, targetNodeSquare);
                }
                if (currentNodeSquare.Sides.IsRightSideOpen && !currentNodeSquare.Sides.IsSideAssignedToNode(NodeSquareSide.Right))
                {
                    NodeSquare targetNodeSquare = _floor.GetNodeSquare(new Position(row, col + 1));
                    if (currentNodeSquare.HasRoom && targetNodeSquare.HasRoom) GenerateCorridor(currentNodeSquare, targetNodeSquare);
                }
            }
        }

        /*
        Console.WriteLine(String.Format(
            "NodeSquare 0, 0 has downSide? {0} and downSide node {1}, {2}",
            GetNodeSquare(new Position(0, 0)).Sides.IsDownSideOpen,
            GetNodeSquare(new Position(0, 0)).Sides.DownSide!.Position.Row,
            GetNodeSquare(new Position(0, 0)).Sides.DownSide!.Position.Col
        ));
        */
    }
}