namespace DungeonMaker;

/// <summary>
/// Defines what a pathfinder child must have. Subject to change.
/// </summary>
public abstract class Pathfinder
{
    public virtual Position NextNode(Node previousNode, Node originNode, Node targetNode)
    {
        throw new NotImplementedException("The NextNode method must be overridden in subclasses.");
    }
}