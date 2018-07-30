namespace TicTacToeKata.Exceptions
{
    using System;

    public class InvalidPlacementException : Exception
    {
        public InvalidPlacementException(string message)
            : base(message)
        {
        }
    }
}