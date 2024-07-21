public sealed class Move
{
    private readonly ChessPiece piece;
    private readonly ChessPiece captured;
    private readonly Tile initialTile;
    private readonly Tile finalTile;

    public Move() : this(new Tile(new Point(0, 0)), new Tile(new Point(0, 0))) { }

    public Move(Tile _initialTile, Tile _finalTile)
    {
        piece = _initialTile.Piece();
        captured = _finalTile.Piece();
        initialTile = _initialTile.SetPiece(new Piece());
        finalTile = _finalTile.SetPiece(piece.UpdatePosition(_finalTile.Position()));
    }

    public Tile[] Tiles()
    {
        return new Tile[2] { initialTile, finalTile };
    }

    public void Print(string prefix = "")
    {
        Console.Write(prefix);
        Console.WriteLine(piece.Side() == 1 ? "White moved: " : "Black moved: ");
        Console.Write(prefix);
        Console.WriteLine($"{piece.Shape()} from ({initialTile.Position().x}, {initialTile.Position().y}) to ({finalTile.Position().x}, {finalTile.Position().y})");
    }

    public void Print()
    {
        Console.WriteLine(piece.Side() == 1 ? "White moved: " : "Black moved: ");
        Console.WriteLine($"{piece.Shape()} from ({initialTile.Position().x}, {initialTile.Position().y}) to ({finalTile.Position().x}, {finalTile.Position().y})");
    }

    public ChessPiece Piece()
    {
        return piece;
    }

    public Move Undo()
    {
        return new Move(finalTile, initialTile);
    }

    public char Notation()
    {
        return piece.Notation();
    }
}
