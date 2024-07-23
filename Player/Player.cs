abstract class Player
{
    private readonly int side;

    public Player(int _side)
    {
        side = _side;
    }

    public int Side()
    {
        return side;
    }

    public Board Play(Move _move, Board _position)
    {
        Board newPosition = _position.Update(_move);

        return newPosition;
    }
}
