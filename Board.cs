public class Board
{
    private readonly Pieces whitePieces;
    private readonly Pieces blackPieces;
    private readonly Tile[,] tiles;
    private readonly Dictionary<bool, string> gameOver;

    public Board() 
        : this(new Pieces(), new Pieces(), new Tile[8, 8], new Dictionary<bool, string> { [false] = "" })
    {

    }

    public Board(Pieces _whitePieces, Pieces _blackPieces) 
        : this(_whitePieces, _blackPieces, new Tile[8, 8], new Dictionary<bool, string> { [false] = "" })
    {

    }

    public Board(Pieces _whitePieces, Pieces _blackPieces, Tile[,] _tiles, Dictionary<bool, string> _gameOver)
    {
        whitePieces = _whitePieces;
        blackPieces = _blackPieces;
        tiles = _tiles;
        gameOver = _gameOver;
    }

    public bool GameOver()
    {
        return gameOver.First().Key;
    }

    public Board UpdateGameOverProperty(string result)
    {
        return new Board(whitePieces, blackPieces, tiles, new Dictionary<bool, string> { [true] = result });
    }

    public Dictionary<bool, string> GameOverResult()
    {
        return gameOver;
    }

    public string FEN()
    {
        string fen = "";

        for (int i = 0; i < 8; i++)
        {
            int empty = 0;
            for (int j = 0; j < 8; ++j)
            {
                if (tiles[i, j].Piece().Side() != 2)
                {
                    if (empty != 0) {
                        fen += empty.ToString();
                    }

                    fen += tiles[i, j].Piece().Notation();
                    empty = 0;
                    continue;
                }

                empty++;
            }

            if (empty != 0) {
                fen += empty.ToString();
                fen += '/';
                continue;
            }

            fen += '/';
        }

        return fen;
    }

    public int Evaluation()
    {
        int whiteQuality = 0;
        int blackQuality = 0;

        foreach (var pair in whitePieces.List())
        {
            whiteQuality += pair.Value.Value();
        }

        foreach (var pair in blackPieces.List())
        {
            blackQuality += pair.Value.Value();
        }

        return (whiteQuality) - (blackQuality);
    }

    public Board Update(Move _move)
    {
        Tile[] newTiles = _move.Tiles();

        Tile[,] updatedTiles = tiles;

        ChessPiece pieceUpdated = _move.Tiles()[1].Piece();

        for (int i = 0; i < newTiles.Length; ++i)
        {
            int x = newTiles[i].Position().x;
            int y = newTiles[i].Position().y;

            updatedTiles[y, x] = newTiles[i];
        }

        Dictionary<Point, ChessPiece> whitePiecesUpdated = whitePieces.List();
        Dictionary<Point, ChessPiece> blackPiecesUpdated = blackPieces.List();
        Dictionary<bool, string> gameOverUpdated = gameOver;

        if (pieceUpdated.Side() == 1)
        {
            whitePiecesUpdated.Remove(newTiles[0].Position());
            whitePiecesUpdated[newTiles[1].Position()] = pieceUpdated;

            if (blackPiecesUpdated.ContainsKey(newTiles[1].Position()))
            {
                if (blackPiecesUpdated[newTiles[1].Position()].Notation() == 'k')
                {
                    gameOverUpdated = new Dictionary<bool, string> { [true] = "white" };
                }

                blackPiecesUpdated.Remove(newTiles[1].Position());
            }
        }
        else
        {
            blackPiecesUpdated.Remove(newTiles[0].Position());
            blackPiecesUpdated[newTiles[1].Position()] = pieceUpdated;

            if (whitePiecesUpdated.ContainsKey(newTiles[1].Position()))
            {
                if (whitePiecesUpdated[newTiles[1].Position()].Notation() == 'K')
                {
                    gameOverUpdated = new Dictionary<bool, string> { [true] = "black" };
                }

                whitePiecesUpdated.Remove(newTiles[1].Position());
            }
        }

        return new Board(new Pieces(whitePiecesUpdated), new Pieces(blackPiecesUpdated), updatedTiles, gameOverUpdated);
    }

    public Board Copy()
    {
        Dictionary<Point, ChessPiece> whitepiecesCopy = new Dictionary<Point, ChessPiece>();
        Dictionary<Point, ChessPiece> blackpiecesCopy = new Dictionary<Point, ChessPiece>();
        Tile[,] tilesCopy = new Tile[8, 8];

        foreach (var pair in whitePieces.List())
        {
            whitepiecesCopy[pair.Key] = pair.Value;
        }

        foreach (var pair in blackPieces.List())
        {
            blackpiecesCopy[pair.Key] = pair.Value;
        }

        foreach (Tile tile in tiles)
        {
            tilesCopy[tile.Position().y, tile.Position().x] = tile;
        }

        Board board = new Board(new Pieces(whitepiecesCopy), new Pieces(blackpiecesCopy), tilesCopy, gameOver);
        board.SetPieces(whitepiecesCopy, blackpiecesCopy);

        return board;
    }

    public Tile Tile(int x, int y)
    {
        return tiles[y, x];
    }

    public Tile Tile(Point axis)
    {
        return tiles[axis.y, axis.x];
    }

    public Dictionary<Point, ChessPiece> SidePieces(int _side)
    {
        if (_side == 1)
        {
            return whitePieces.List();
        }

        return blackPieces.List();
    }

    public Dictionary<Point, ChessPiece> WhitePieces()
    {
        return whitePieces.List();
    }

    public Dictionary<Point, ChessPiece> BlackPieces()
    {
        return blackPieces.List();
    }

    public void SetPieces(Dictionary<Point, ChessPiece> _whitePieces, Dictionary<Point, ChessPiece> _blackPieces)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Tile tile = new Tile(new Point(j, i));
                tiles[i, j] = tile;

                for (int k = 0; k < 16; k++)
                {
                    if (_whitePieces.Count - 1 >= k 
                        && _whitePieces.ElementAt(k).Value.Position().y == i
                        && _whitePieces.ElementAt(k).Value.Position().x == j)
                    {
                        tiles[i, j] = new Tile(_whitePieces.ElementAt(k).Value, new Point(j, i));
                        break;
                    }

                    if (_blackPieces.Count - 1 >= k
                        && _blackPieces.ElementAt(k).Value.Position().y == i
                        && _blackPieces.ElementAt(k).Value.Position().x == j)
                    {
                        tiles[i, j] = new Tile(_blackPieces.ElementAt(k).Value, new Point(j, i));
                        break;
                    }
                }
            }
        }
    }
}
