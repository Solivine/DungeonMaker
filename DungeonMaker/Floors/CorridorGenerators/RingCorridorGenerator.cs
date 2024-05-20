namespace DungeonMaker;

/// <summary>
/// Connects corridors in a ring around the edge.
/// Middle rooms are randomly either horizontal only corridors, or vertical only corridors, should the map height or width exceed 2.
/// </summary>
/// <param name="floor"></param>
public class RingCorridorGenerator(Floor floor) : CorridorGenerator(floor)
{
    public override void GenerateCorridors()
    {
        Random rnd = new Random();
        bool verticalCorridors = rnd.Next(0, 2) == 0;

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

                // Case for middle squares
                if (row > 0 && row < _floor.Size.Height - 1 && col > 0 && col < _floor.Size.Width - 1)
                {
                    if (verticalCorridors)
                    {
                        // Then connect up and down
                        if (currentNodeSquare.Sides.IsUpSideOpen && !currentNodeSquare.Sides.IsSideAssignedToNode(NodeSquareSide.Up))
                        {
                            // Connect up
                            NodeSquare targetNodeSquare = _floor.GetNodeSquare(new Position(row - 1, col));
                            if (currentNodeSquare.HasRoom && targetNodeSquare.HasRoom) GenerateCorridor(currentNodeSquare, targetNodeSquare);
                        }
                        if (currentNodeSquare.Sides.IsDownSideOpen && !currentNodeSquare.Sides.IsSideAssignedToNode(NodeSquareSide.Down))
                        {
                            // Connect down
                            NodeSquare targetNodeSquare = _floor.GetNodeSquare(new Position(row + 1, col));
                            if (currentNodeSquare.HasRoom && targetNodeSquare.HasRoom) GenerateCorridor(currentNodeSquare, targetNodeSquare);
                        }
                    }
                    else
                    {
                        // Then connect left and right
                        if (currentNodeSquare.Sides.IsLeftSideOpen && !currentNodeSquare.Sides.IsSideAssignedToNode(NodeSquareSide.Left))
                        {
                            // Connect left
                            NodeSquare targetNodeSquare = _floor.GetNodeSquare(new Position(row, col - 1));
                            if (currentNodeSquare.HasRoom && targetNodeSquare.HasRoom) GenerateCorridor(currentNodeSquare, targetNodeSquare);
                        }
                        if (currentNodeSquare.Sides.IsRightSideOpen && !currentNodeSquare.Sides.IsSideAssignedToNode(NodeSquareSide.Right))
                        {
                            // Connect right
                            NodeSquare targetNodeSquare = _floor.GetNodeSquare(new Position(row, col + 1));
                            if (currentNodeSquare.HasRoom && targetNodeSquare.HasRoom) GenerateCorridor(currentNodeSquare, targetNodeSquare);
                        }
                    }
                }
            }
        }
    }
}