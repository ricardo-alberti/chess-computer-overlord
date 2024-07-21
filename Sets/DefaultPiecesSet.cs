public class DefaultPiecesSet
{
    private readonly Pieces white;
    private readonly Pieces black;

    public DefaultPiecesSet()
    {
        white = new Pieces(defaultPieces(1));
        black = new Pieces(defaultPieces(0));
    }

    public Pieces White()
    {
        return white;
    }

    public Pieces Black()
    {
        return black;
    }

    private static Dictionary<Point, ChessPiece> defaultPieces(int _side)
    {
        Dictionary<Point, ChessPiece> pieces = new Dictionary<Point, ChessPiece>();

        int id = 1;

        char[] notations = {
                'R','R','N','N','B','B','K','Q',
                'P','P','P','P','P','P','P','P',
            };

        Point[] position = {
                new Point(0, 7), new Point(7, 7), new Point(1, 7), new Point(6, 7), new Point(2, 7), new Point(5, 7), new Point(4, 7), new Point(3, 7),
                new Point(0, 6), new Point(1, 6), new Point(2, 6), new Point(3, 6), new Point(4, 6), new Point(5, 6), new Point(6, 6), new Point(7, 6),
                new Point(0, 0), new Point(7, 0), new Point(1, 0), new Point(6, 0), new Point(2, 0), new Point(5, 0), new Point(4, 0), new Point(3, 0),
                new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(3, 1), new Point(4, 1), new Point(5, 1), new Point(6, 1), new Point(7, 1)
            };

        for (int i = 0; i < 16; i++)
        {
            ChessPiece piece = ChessPiece.CreatePiece(notations[i], position[(_side * 16) + i], _side, (id++) + (16 * _side));
            pieces.Add(piece.Position(), piece);
        }

        return pieces;
    }
}
