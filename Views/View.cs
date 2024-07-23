public class View
{
    public View()
    {

    }

    public void PrintBoard(Board _board)
    {
        Console.Clear();

        Console.WriteLine("     A B C D E F G H");
        Console.WriteLine("    -----------------");

        for (int i = 0; i < 8; i++)
        {
            Console.Write($" {i + 1} |");

            for (int j = 0; j < 8; ++j)
            {
                PrintTile(_board.Tile(j, i));
            }

            Console.WriteLine(" |");
        }

        Console.WriteLine("    -----------------");
        Console.Write("FEN: ");
        Console.WriteLine(_board.FEN());
    }

    private void PrintTile(Tile _tile)
    {
        Console.Write(_tile.Piece().Shape());
    }
}
