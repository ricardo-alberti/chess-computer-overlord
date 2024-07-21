class Queen : ChessPiece
{
    public Queen(Point position, int side, int id)
        : base(id, 
               side == 1 ? 'Q' : 'q',
               side == 1 ? " \u2655" : " \u265B",
               position,
               new int[,] { { 0, -1 }, { 0, 1 }, { 1, 0 }, { 1, 1 }, { -1, 0 }, { -1, -1 }, { 1, -1 }, { -1, 1 } },
               side,
               9)
    {

    }
}
