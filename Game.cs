public sealed class Game
{
    private         Game() { }
    private static  Game _instance;
    public static   Game Instance()
    {
        if (_instance == null)
        {
            _instance = new Game();
        }
        return _instance;
    }

    private void WinnerPrompt(Board positionState)
    {
        string result = positionState.GameOverResult()[true];

        if (result == "tie")
        {
            Console.WriteLine("Tie");
        }

        else if (result == "white")
        {
            Console.WriteLine("White wins");
        }

        else
        {
            Console.WriteLine("Black wins");
        }
    }

    public void Start()
    {
        DefaultPiecesSet pieces = new DefaultPiecesSet();
        Board board             = new Board(pieces.White(), pieces.Black());
        Robot Robot0            = new Robot(1, new Dictionary<string, Move>());
        Robot Robot1            = new Robot(0, new Dictionary<string, Move>());
        Move move               = new Move();
        MoveTree movetree       = new MoveTree();
        View view               = new View();

        board.SetPieces(board.WhitePieces(), board.BlackPieces());
        view.PrintBoard(board);

        while (!board.GameOver())
        {
            move = Robot0.Calculate(board, 0);
            board = board.Update(move);
            view.PrintBoard(board);

            if (board.GameOver()) break;

            move = Robot1.Calculate(board, 0);
            board = board.Update(move);
            view.PrintBoard(board);
        }

        WinnerPrompt(board);
    }
}
