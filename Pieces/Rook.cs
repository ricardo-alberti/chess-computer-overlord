class Rook : ChessPiece
{
    public Rook(Point position, int side, int id)
        : base(id,
               side == 1 ? 'R' : 'r',
               side == 1 ? " \u2656" : " \u265C",
               position,
               new int[,] { { -1, 0 }, { 1, 0 }, { 0, 1 }, { 0, -1 } },
               side,
               5)
    {

    }
}
