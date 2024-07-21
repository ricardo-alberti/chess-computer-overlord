class Pawn : ChessPiece
{
    public Pawn(Point position, int side, int id)
        : base(id, 
               side == 1 ? 'P' : 'p', 
               side == 1 ? " \u2659" : " \u265F",
               position, 
               side == 0 ? new int[,] { { -1, -1 }, { -1, 1 }, { -1, 0 } } : new int[,] { { 1, 1 }, { 1, -1 }, { 1, 0 } }, 
               side, 
               1)
    {

    }

    public override List<Move> MoveRange(Board _position)
    {
        Board position = _position.Copy();
        List<Move> moveRange = new List<Move>();

        Tile initialTile = position.Tile(Position().x, Position().y);
        ChessPiece piece = initialTile.Piece();
        int[,] moveSet = piece.MoveSet();
        
        int moveset_x, moveset_y, finaltile_x, finaltile_y;

        for (int i = 0; i < moveSet.GetLength(0); ++i)
        {
            moveset_x = moveSet[i, 1];
            moveset_y = moveSet[i, 0];

            finaltile_x = initialTile.Position().x + moveset_x;
            finaltile_y = initialTile.Position().y + moveset_y;

            if (finaltile_x < 0 || finaltile_y < 0 || finaltile_y > 7 || finaltile_x > 7) continue;

            Tile finalTile = position.Tile(finaltile_x, finaltile_y);

            if (finalTile.Piece().Side() == initialTile.Piece().Side()) continue;
            if (moveset_x != 0 && finalTile.Piece().Side() == 2) continue;
            if (moveset_x == 0 && finalTile.Piece().Side() != 2) continue;

            if (finalTile.Position().y == 7 || finalTile.Position().y == 0)
            {
                piece = piece.Promote('Q');
                initialTile = initialTile.SetPiece(piece);
            }

            moveRange.Add(new Move(initialTile, finalTile));
        }

        return moveRange;
    }
}
