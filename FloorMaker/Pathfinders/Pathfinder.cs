namespace DungeonMaker;

public abstract class Pathfinder
{
    public virtual Position NextNode(Node previousNode, Node originNode, Node targetNode)
    {
        throw new NotImplementedException("The NextNode method must be overridden in subclasses.");
    }
}