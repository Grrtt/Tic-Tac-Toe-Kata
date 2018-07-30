namespace TicTacToeKata.Abstract
{
    using TicTacToeKata.Model;

    public interface IWinValidator
    {
        bool ValidateWinCondition(PlayerTokens?[,] gameBoard);
    }
}