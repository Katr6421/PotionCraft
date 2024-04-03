public enum OperationType { RotateLeft, RotateRight, FlipColors }

public struct Operation
{
    public Node Node { get; set; }
    public OperationType OperationType { get; set; }

    public Operation(Node node, OperationType type)
    {
        Node = node;
        OperationType = type;
    }
}
