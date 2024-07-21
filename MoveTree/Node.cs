public class Node
{
    private readonly Move value;
    private readonly List<int> eval;
    private readonly Board position;
    private readonly List<Node> children;

    public Node() : this(new Move(), new List<Node>(), new Board()) { }

    public Node(Move _move) : this(_move, new List<Node>(), new Board()) { }

    public Node(Move _move, Board _position) : this(_move, new List<Node>(), _position) { }

    public Node(Move _move, List<Node> _children, Board _position)
    {
        value = _move;
        eval = new List<int> { _position.Evaluation() };
        children = _children;
        position = _position;
    }

    public int Eval()
    {
        return eval.First();
    }

    public void Eval(int _eval)
    {
        eval.Clear();
        eval.Add(_eval);
    }

    public Board Position()
    {
        return position;
    }

    public Move Value()
    {
        return value;
    }

    public Node Value(Move _move)
    {
        return new Node(_move, children, position);
    }

    public List<Node> Children()
    {
        return children;
    }

    public Node Child(int idx)
    {
        if (Children().Count > 0)
        {
            return new Node();
        }

        return children.ElementAt(idx);
    }
}
