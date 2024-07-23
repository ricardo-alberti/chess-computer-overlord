class Robot : Player
{
    private readonly int side;
    private readonly Dictionary<string, MoveTree> possiblePositions;

    public Robot() : this(0, new Dictionary<string, MoveTree>()) { }

    public Robot(int _side, Dictionary<string, MoveTree> _positions) : base(_side)
    {
        side = _side;
        possiblePositions = _positions;
    }

    private int[] PiecesIds(Dictionary<Point, ChessPiece> pieces)
    {
        int[] ids = new int[pieces.Count];
        for (int i = 0; i < pieces.Count; i++)
        {
            ids[i] = pieces.ElementAt(i).Value.Id();
        }

        return ids;
    }

    public Move Calculate(Board _position, int level)
    {
        string FEN = _position.FEN();

        if (possiblePositions.ContainsKey(FEN)) 
        {
            return possiblePositions[FEN].Root().Value();
        }

        Dictionary<int, MoveTree> moveMap = new Dictionary<int, MoveTree>();
        Robot me = new Robot(Side(), new Dictionary<string, MoveTree>());
        Robot enemy = new Robot(Side() == 1 ? 0 : 1, new Dictionary<string, MoveTree>());
        Board position = _position.Copy();

        Dictionary<int, MoveTree> movetrees = moveTrees(position, me, enemy, new Dictionary<int, MoveTree>(), level);

        MoveTree movetree = Side() == 1 ? movetrees[movetrees.Keys.Max()] : movetrees[movetrees.Keys.Min()];

        return movetree.Root().Value();
    }

    public Dictionary<int, MoveTree> moveTrees(Board _position, Robot me, Robot enemy, Dictionary<int, MoveTree> movetrees, int level)
    {
        List<ChessPiece> pieces = _position.SidePieces(me.Side()).Values.ToList();
        MoveTree movetree = new MoveTree();
        string FEN = _position.FEN();
        string lastFEN = "";

        foreach (ChessPiece piece in pieces)
        {
            Board copy = _position.Copy();
            List<Move> moveRange = piece.MoveRange(copy);

            foreach (Move move in moveRange)
            {
                copy = _position.Copy();
                Board newPosition = copy.Update(move);
                Node node = new Node(move, newPosition);

                MoveTree treeRoot = new MoveTree(node);

                movetree = moveTree(treeRoot, newPosition, enemy, me, level, treeRoot.Root());;

                movetrees[movetree.Root().Eval()] = movetree;
            }
        }

        return movetrees;
    }

    private MoveTree moveTree(MoveTree _movetree, Board _position, Robot me, Robot enemy, int level, Node _root)
    {
        if (level == 0)
        {
            return _movetree;
        }

        List<ChessPiece> pieces = _position.SidePieces(me.Side()).Values.ToList();
        Board newPosition = _position;
        Node node = new Node();

        foreach (ChessPiece piece in pieces)
        {
            Board copy = _position.Copy();
            List<Move> moveRange = piece.MoveRange(copy);

            foreach (Move move in moveRange)
            {
                copy = _position.Copy();
                newPosition = copy.Update(move);
                node = new Node(move, newPosition, _root);

                _movetree = _movetree.Insert(node, _root);
                _movetree = moveTree(_movetree, newPosition, enemy, me, level - 1, node);
            }
        }

        return _movetree;
    }
}
