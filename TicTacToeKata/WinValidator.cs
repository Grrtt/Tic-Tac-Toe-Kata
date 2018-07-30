namespace TicTacToeKata
{
    using System;

    using TicTacToeKata.Abstract;
    using TicTacToeKata.Model;

    public class WinValidator : IWinValidator
    {
        public bool ValidateWinCondition(PlayerTokens?[,] gameBoard)
        {
            if (gameBoard == null)
            {
                throw CreateArgumentException("No board to validate.");
            }

            if (HorizontalWin(gameBoard) || VerticalWin(gameBoard) || DiagonalWin(gameBoard))
            {
                return true;
            }

            return false;
        }

        private bool ColumnHasWin(int yCoordinate, PlayerTokens?[,] gameBoard)
        {
            PlayerTokens? firstInColumn = gameBoard[0, yCoordinate];
            for (int xCoordinate = 0; xCoordinate < 3; xCoordinate++)
            {
                PlayerTokens? currentBoardSpace = gameBoard[xCoordinate, yCoordinate];
                if (firstInColumn == null || currentBoardSpace != firstInColumn)
                {
                    return false;
                }
            }

            return true;
        }

        private ArgumentException CreateArgumentException(string message)
        {
            return new ArgumentException(message);
        }

        private bool DiagonalWin(PlayerTokens?[,] gameBoard)
        {
            if (PositiveDiagonalHasWin(gameBoard) || NegativeDiagonalHasWin(gameBoard))
            {
                return true;
            }

            return false;
        }

        private bool HorizontalWin(PlayerTokens?[,] gameBoard)
        {
            for (int x = 0; x < 3; x++)
            {
                if (RowHasWin(x, gameBoard))
                {
                    return true;
                }
            }

            return false;
        }

        private bool NegativeDiagonalHasWin(PlayerTokens?[,] gameBoard)
        {
            PlayerTokens? anchorPoint = gameBoard[2, 0];

            int yCoordinate = 0;
            for (int xCoordinate = 2; xCoordinate > 0; xCoordinate--)
            {
                PlayerTokens? currentBoardSpace = gameBoard[xCoordinate, yCoordinate];
                if (anchorPoint == null || currentBoardSpace != anchorPoint)
                {
                    return false;
                }

                yCoordinate++;
            }

            return true;
        }

        private bool PositiveDiagonalHasWin(PlayerTokens?[,] gameBoard)
        {
            PlayerTokens? anchorPoint = gameBoard[0, 0];

            for (int i = 0; i < 3; i++)
            {
                PlayerTokens? currentBoardSpace = gameBoard[i, i];
                if (anchorPoint == null || currentBoardSpace != anchorPoint)
                {
                    return false;
                }
            }

            return true;
        }

        private bool RowHasWin(int xCoordinate, PlayerTokens?[,] gameBoard)
        {
            PlayerTokens? firstInRow = gameBoard[xCoordinate, 0];
            for (int yCoordinate = 1; yCoordinate < 3; yCoordinate++)
            {
                PlayerTokens? currentBoardSpace = gameBoard[xCoordinate, yCoordinate];

                if (firstInRow == null || currentBoardSpace != firstInRow)
                {
                    return false;
                }
            }

            return true;
        }

        private bool VerticalWin(PlayerTokens?[,] gameBoard)
        {
            for (int y = 0; y < 3; y++)
            {
                if (ColumnHasWin(y, gameBoard))
                {
                    return true;
                }
            }

            return false;
        }
    }
}