class Robot : Player
{
    private readonly int side;
    private readonly Board board;
    private readonly string name;
    private readonly Dictionary<string, Move> possiblePositions;

    public Robot() : this(0, new Board(), new Dictionary<string, Move>(), "robot") { }

    public Robot(int _side, Board _chessBoard, Dictionary<string, Move> _positions, string _name) : base(_side, _chessBoard, _name)
    {
        side = _side;
        possiblePositions = _positions;
        board = _chessBoard;
        name = _name;
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
            Move move = possiblePositions[FEN];
            return move;
        }

        Dictionary<int, MoveTree> moveMap = new Dictionary<int, MoveTree>();
        Robot me = new Robot(Side(), _position, new Dictionary<string, Move>(), "robot1");
        Robot enemy = new Robot(Side() == 1 ? 0 : 1, _position, new Dictionary<string, Move>(), "robot2");
        Board position = _position.Copy();

        Dictionary<int, MoveTree> movetrees = moveTrees(position, me, enemy, new Dictionary<int, MoveTree>(), level);

        MoveTree movetree = Side() == 1 ? movetrees[movetrees.Keys.Max()] : movetrees[movetrees.Keys.Min()];

        return movetree.Root().Value();
    }

    public Dictionary<int, MoveTree> moveTrees(Board _position, Robot me, Robot enemy, Dictionary<int, MoveTree> movetrees, int level)
    {
        List<ChessPiece> pieces = _position.SidePieces(me.Side()).Values.ToList();

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
                MoveTree movetree = moveTree(treeRoot, newPosition, enemy, me, level, treeRoot.Root(), level);

                movetrees[movetree.Root().Eval()] = movetree;
            }
        }

        return movetrees;
    }

    private MoveTree moveTree(MoveTree _movetree, Board _position, Robot me, Robot enemy, int level, Node _root, int initialLevel)
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
                node = new Node(move, newPosition);

                _movetree = _movetree.Insert(node, _root);
                _movetree = moveTree(_movetree, newPosition, enemy, me, level - 1, node, initialLevel);

                if (initialLevel == level) {
                    possiblePositions[newPosition.FEN()] = _movetree.bestChildNode(node).Value();
                }
            }
        }

        return _movetree;
    }
}
