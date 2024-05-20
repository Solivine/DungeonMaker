namespace DungeonMaker;

/// <summary>
/// A pathfinder class for finding the next node for a corridor using the manhattan distance.
/// </summary>
public class CorridorPathfinder : Pathfinder
{
    public override Position NextNode(Node previousNode, Node originNode, Node targetNode)
    {
        Position previous = previousNode.Position;
        Position origin = originNode.Position;

        double bestScore = -1;
        Position bestCoords = origin;

        for (int row = origin.Row - 1; row < origin.Row - 1 + 3; row++)
        {
            for (int col = origin.Col - 1; col < origin.Col - 1 + 3; col++)
            {
                if (row == origin.Row || col == origin.Col)
                {
                    //Console.WriteLine(String.Format("Checking node... {0}, {1}", x, y));
                    // Valid Node to Check
                    bool hasTurned = (row != origin.Row && origin.Row == previous.Row) || (col != origin.Col && origin.Col == previous.Col);
                    double heuristic = targetNode.CalculateManhattanDistance(new Position(row, col));
                
                    if (heuristic < bestScore || bestScore == -1)
                    {
                        bestScore = heuristic;
                        bestCoords = new Position(row, col);
                    }
                    else if (heuristic == bestScore)
                    {
                        if (!hasTurned)
                        {
                            bestCoords = new Position(row, col);
                        }
                    }
                }
            }
        }

        return bestCoords;
    }
}