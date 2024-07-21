class Knight : ChessPiece
{
    public Knight(Point position, int side, int id)
            : base(id, 
                   side == 1 ? 'N' : 'n',
                   side == 1 ? " \u2658" : " \u265E",
                   position, 
                   new int[,] { { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 }, { 1, 2 }, { -1, 2 }, { 1, -2 }, { -1, -2 } },
                   side,
                   3)
    {

    }

    public override List<Move> MoveRange(Board _position)
    {
        List<Move> moveRange = new List<Move>();

        Tile initialTile = _position.Tile(Position().x, Position().y);
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

            Tile finalTile = _position.Tile(finaltile_x, finaltile_y);

            if (finalTile.Piece().Side() == initialTile.Piece().Side()) continue;

            moveRange.Add(new Move(initialTile, finalTile));
        }

        return moveRange;
    }
}
