public class Pieces
{
    private readonly Dictionary<Point, ChessPiece> pieces;

    public Pieces() : this(new Dictionary<Point, ChessPiece>())
    {

    }

    public Pieces(Dictionary<Point, ChessPiece> _pieces)
    {
        pieces = _pieces;
    }

    public Dictionary<Point, ChessPiece> List()
    {
        return pieces;
    }
}
