namespace AppRunner
{
    using TicTacToeKata;
    using TicTacToeKata.Abstract;

    class Program
    {
        static void Main()
        {
            IWinValidator winValidator = new WinValidator();
            ITicTacToe game = new TicTacToe(winValidator);
            game.PlaceToken(0, 0);
        }
    }
}