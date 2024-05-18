namespace DungeonMaker;

public class RingCorridorGenerator(Floor floor) : CorridorGenerator(floor)
{
    public override void GenerateCorridors()
    {
        // 1. Generate all corridors
        for (int row = 0; row < _floor.Size.Height; row++)
        {
            for (int col = 0; col < _floor.Size.Width; col++)
            {
                NodeSquare currentNodeSquare = _floor.GetNodeSquare(new Position(row, col));

                bool isHorizontalEdge = !(currentNodeSquare.Sides.IsLeftSideOpen && currentNodeSquare.Sides.IsRightSideOpen);
                bool isVerticalEdge = !(currentNodeSquare.Sides.IsUpSideOpen && currentNodeSquare.Sides.IsDownSideOpen);
                
                // Connect all connections
                if (isHorizontalEdge && currentNodeSquare.Sides.IsUpSideOpen && !currentNodeSquare.Sides.IsSideAssignedToNode(NodeSquareSide.Up))
                {
                    NodeSquare targetNodeSquare = _floor.GetNodeSquare(new Position(row - 1, col));
                    if (currentNodeSquare.HasRoom && targetNodeSquare.HasRoom) GenerateCorridor(currentNodeSquare, targetNodeSquare);
                }
                if (isHorizontalEdge && currentNodeSquare.Sides.IsDownSideOpen && !currentNodeSquare.Sides.IsSideAssignedToNode(NodeSquareSide.Down))
                {
                    NodeSquare targetNodeSquare = _floor.GetNodeSquare(new Position(row + 1, col));
                    if (currentNodeSquare.HasRoom && targetNodeSquare.HasRoom) GenerateCorridor(currentNodeSquare, targetNodeSquare);
                }
                if (isVerticalEdge && currentNodeSquare.Sides.IsLeftSideOpen && !currentNodeSquare.Sides.IsSideAssignedToNode(NodeSquareSide.Left))
                {
                    NodeSquare targetNodeSquare = _floor.GetNodeSquare(new Position(row, col - 1));
                    if (currentNodeSquare.HasRoom && targetNodeSquare.HasRoom) GenerateCorridor(currentNodeSquare, targetNodeSquare);
                }
                if (isVerticalEdge && currentNodeSquare.Sides.IsRightSideOpen && !currentNodeSquare.Sides.IsSideAssignedToNode(NodeSquareSide.Right))
                {
                    NodeSquare targetNodeSquare = _floor.GetNodeSquare(new Position(row, col + 1));
                    if (currentNodeSquare.HasRoom && targetNodeSquare.HasRoom) GenerateCorridor(currentNodeSquare, targetNodeSquare);
                }
            }
        }
    }
}