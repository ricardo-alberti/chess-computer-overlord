public abstract class ChessPiece
{
    private readonly int id;
    private readonly char notation;
    private readonly int value;
    private readonly string shape;
    private readonly Point position;
    private readonly int[,] moveSet;
    private readonly int side;

    protected ChessPiece(int _id, char _notation, string _shape, Point _position, int[,] _moveSet, int _side, int _value)
    {
        id = _id;
        notation = _notation;
        shape = _shape;
        position = _position;
        moveSet = _moveSet;
        side = _side;
        value = _value;
    }

    public ChessPiece UpdatePosition(Point _position)
    {
        return CreatePiece(this.notation, _position, this.side, id);
    }

    public string Shape()
    {
        return shape;
    }

    public int Value()
    {
        return value;
    }

    public char Notation()
    {
        return notation;
    }

    public int Id()
    {
        return id;
    }

    public int Side()
    {
        return side;
    }

    public ChessPiece Promote(char notation)
    {
        return CreatePiece(notation, position, side, id);
    }

    public int[,] MoveSet()
    {
        return moveSet;
    }

    public Point Position()
    {
        return position;
    }

    public ChessPiece Copy()
    {
        return CreatePiece(notation, position, side, id);
    }

    public void PrintMoveRange(List<Move> moveRange)
    {
        foreach (Move move in moveRange)
        {
            move.Print();
        }
    }

    public virtual List<Move> MoveRange(Board _position)
    {
        Board position = _position.Copy();
        List<Move> moveRange = new List<Move>();

        Tile initialTile = _position.Tile(Position().x, Position().y);
        ChessPiece piece = initialTile.Piece();
        int[,] moveSet = piece.MoveSet();

        int moveset_x, moveset_y, finaltile_x, finaltile_y;
        Tile finalTile = new Tile(new Point());
        Move move = new Move();

        for (int i = 0; i < moveSet.GetLength(0); ++i)
        {
            moveset_x = moveSet[i, 0];
            moveset_y = moveSet[i, 1];

            finaltile_x = initialTile.Position().x + moveset_x;
            finaltile_y = initialTile.Position().y + moveset_y;

            while (finaltile_x >= 0 && finaltile_y >= 0 && finaltile_y <= 7 && finaltile_x <= 7)
            {
                finalTile = position.Tile(finaltile_x, finaltile_y);

                if (finalTile.Piece().Side() == initialTile.Piece().Side()) break;
                if (finalTile.Piece().Side() != initialTile.Piece().Side() && finalTile.Piece().Side() != 2)
                {
                    move = new Move(initialTile, finalTile);
                    moveRange.Add(move);

                    break;
                }

                move = new Move(initialTile, finalTile);
                moveRange.Add(move);

                finaltile_x += moveset_x;
                finaltile_y += moveset_y;
            }
        }

        return moveRange;
    }

    public static ChessPiece CreatePiece(char notation, Point position, int side, int id)
    {
        switch (notation)
        {
            case 'n':
                return new Knight(position, side, id);
            case 'r':
                return new Rook(position, side, id);
            case 'p':
                return new Pawn(position, side, id);
            case 'q':
                return new Queen(position, side, id);
            case 'k':
                return new King(position, side, id);
            case 'b':
                return new Bishop(position, side, id);

            case 'N':
                return new Knight(position, side, id);
            case 'R':
                return new Rook(position, side, id);
            case 'P':
                return new Pawn(position, side, id);
            case 'Q':
                return new Queen(position, side, id);
            case 'K':
                return new King(position, side, id);
            case 'B':
                return new Bishop(position, side, id);

            default:
                return new Piece();
        }
    }
}
