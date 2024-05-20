namespace DungeonMaker;

public enum CorridorGeneratorType
{
    All,
    //Snake,
    Ring
    //CriticalPath
}

/// <summary>
/// The base parent class for corridor generators, it contains the one method all children must inherit of how to connect a corridor between 2 squares.
/// </summary>
/// <param name="floor">The floor for corridors to be generated on.</param>
public abstract class CorridorGenerator(Floor floor)
{
    protected readonly Floor _floor = floor;

    protected static void GenerateCorridor(NodeSquare originNodeSquare, NodeSquare neighbouringNodeSquare)
    {
        // -1. Error Checks
        if (!originNodeSquare.HasRoom) throw new NullReferenceException("Origin node square does not have a room!");
        if (!neighbouringNodeSquare.HasRoom) throw new NullReferenceException("Neighbouring node square does not have a room!");

        // 0. Work out direction of origin -> neighbouring
        NodeSquareSide originJoinSide;
        NodeSquareSide targetJoinSide;
        if (originNodeSquare.Position.Col == neighbouringNodeSquare.Position.Col)
        {
            // Equal on columns, up or down?
            bool isOriginAbove = (originNodeSquare.Position.Row - neighbouringNodeSquare.Position.Row) < 0;  // If negative then origin is above
            originJoinSide = isOriginAbove ? NodeSquareSide.Down : NodeSquareSide.Up;
            targetJoinSide = isOriginAbove ? NodeSquareSide.Up : NodeSquareSide.Down;
        }
        else if (originNodeSquare.Position.Row == neighbouringNodeSquare.Position.Row)
        {
            // Equal on rows, left or right?
            bool isOriginLeft = (originNodeSquare.Position.Col - neighbouringNodeSquare.Position.Col) < 0;  // If negative then origin is left
            originJoinSide = isOriginLeft ? NodeSquareSide.Right : NodeSquareSide.Left;
            targetJoinSide = isOriginLeft ? NodeSquareSide.Left : NodeSquareSide.Right;
        }
        else
        {
            throw new Exception("Cannot connect corridor between non connecting node squares");
        }

        // 1. Get room in origin node square, choose random node on correct side, mark node
        Position originRoomNodePosition = originNodeSquare.GetRoom!.GenerateCorridorNodePosition(originJoinSide);

        // 2. Draw to edge of origin node square, mark node
        originNodeSquare.DrawCorridorFromRoom(originJoinSide, originNodeSquare.GetNode(originRoomNodePosition));

        // 3. Get node on same axis in neighbouring node square
        Node neighbouringNodeSquareStartNode = neighbouringNodeSquare.FindAndSetCorridorStartNode(originNodeSquare.Sides.GetNodeFromSide(originJoinSide)!.Position);

        // 4. Get room in neighbouring node square, choose random node on correct side, mark node
        Position targetRoomNodePosition = neighbouringNodeSquare.GetRoom!.GenerateCorridorNodePosition(targetJoinSide);

        // 5. Manhattan pathfind to target node
        neighbouringNodeSquare.DrawCorridorToRoom(neighbouringNodeSquareStartNode, neighbouringNodeSquare.GetNode(targetRoomNodePosition));
    }

    public virtual void GenerateCorridors()
    {
        throw new NotImplementedException("Method GenerateCorridors must be implemented in subclasses");
    }
}