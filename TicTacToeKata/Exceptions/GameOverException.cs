namespace TicTacToeKata.Exceptions
{
    using System;

    public class GameOverException : Exception
    {
        public GameOverException(string message)
            : base(message)
        {
        }
    }
}