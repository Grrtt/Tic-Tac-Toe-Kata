namespace TicTacToeKata.Model
{
    public struct MoveRecord
    {
        public MoveRecord(int xCoordinate, int yCoordinate)
        {
            X = xCoordinate;
            Y = yCoordinate;
        }

        public int X { get; private set; }

        public int Y { get; private set; }
    }
}