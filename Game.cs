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

    public void Start()
    {
        DefaultPiecesSet pieces = new DefaultPiecesSet();
        Board board             = new Board(pieces.White(), pieces.Black());
        Robot whiteBot          = new Robot(1, new Dictionary<string, MoveTree>());
        Robot blackBot          = new Robot(0, new Dictionary<string, MoveTree>());
        Move move               = new Move();
        MoveTree movetree       = new MoveTree();
        View view               = new View();

        board.SetPieces(board.WhitePieces(), board.BlackPieces());
        view.PrintBoard(board);

        while (!board.GameOver())
        {
            move = whiteBot.Calculate(board, 1);
            board = whiteBot.Play(move, board);
            view.PrintBoard(board);

            if (board.GameOver()) break;

            move = blackBot.Calculate(board, 2);
            board = blackBot.Play(move, board);
            view.PrintBoard(board);
        }

        WinnerPrompt(board);
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
}
