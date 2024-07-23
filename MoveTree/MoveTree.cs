public sealed class MoveTree
{
    private readonly Node root;

    public MoveTree(Node _root = null)
    {
        root = _root;
    }

    public Node Root()
    {
        return root;
    }

    public MoveTree Insert(Node _move, Node _node = null)
    {
        if (root == null)
        {
            return new MoveTree(_move);
        }

        return new MoveTree(InsertIntoNode(_move, _node, root));
    }

    private Node InsertIntoNode(Node _move, Node _node, Node _root)
    {
        if (_root == null)
        {
            return null;
        }

        if (root.Children().Count > 0) {
            Node best = bestChildNode(root);
            root.Eval(best.Eval());
        }

        if (_root == _node)
        {
            _root.Children().Add(_move);
            Node best = bestChildNode(_root);
            _root.Eval(best.Eval());
            return _root;
        }

        foreach (var child in _root.Children())
        {
            var result = InsertIntoNode(_move, _node, child);
            if (result != null)
            {
                Node best = bestChildNode(_root);
                child.Eval(best.Eval());
                return _root;
            }
        }

        return null;
    }

    public Node bestChildNode(Node _root)
    {
        List<Node> children = _root.Children();
        Node bestNode = _root.Children().First(); 
        int side = bestNode.Value().Piece().Side();

        foreach (Node child in children)
        {
            if (side == 1 && bestNode.Eval() < child.Eval())
            {
                bestNode = child;
            }

            if (side == 0 && bestNode.Eval() > child.Eval())
            {
                bestNode = child;
            }
        }

        return bestNode;
    }

    public void Print(Node _root)
    {
        Console.Write("\n1. ");
        _root.Value().Print();
        Console.Write(_root.Eval());

        Node best = bestChildNode(_root);
        best.Value().Print();
        Console.Write(best.Eval());

        Node best2 = bestChildNode(best);
        best2.Value().Print();
        Console.Write(best2.Eval());
    }
    
    public void Log()
    {
        string text = "MATCH LOG" + Environment.NewLine;
        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        File.WriteAllText(Path.Combine(docPath, "log.tex"), text);
        string[] lines = { "New line 132", "New line 2" };
        File.AppendAllLines(Path.Combine(docPath, "log.tex"), lines);
    }
}
