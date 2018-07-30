namespace TicTacToeKata
{
    using System.Collections.Generic;
    using System.Linq;

    using TicTacToeKata.Abstract;
    using TicTacToeKata.Exceptions;
    using TicTacToeKata.Model;

    public class TicTacToe : ITicTacToe
    {
        private readonly List<MoveRecord> MoveHistory;

        private readonly IWinValidator winValidator;

        public TicTacToe(IWinValidator winValidator)
        {
            this.winValidator = winValidator;

            Board = new PlayerTokens?[3, 3];
            MoveHistory = new List<MoveRecord>();
        }

        public PlayerTokens?[,] Board { get; private set; }

        public PlayerTokens? Winner { get; private set; }

        public void PlaceToken(int xCoordinate, int yCoordinate)
        {
            if (IsValidPlacement(xCoordinate, yCoordinate))
            {
                Board[xCoordinate, yCoordinate] = GetCurrentPlayer();
                AddMoveToHistory(xCoordinate, yCoordinate);

                if (MoveHistory.Count > 4)
                {
                    ValidateWin();
                }
            }
            else
            {
                RejectMove(xCoordinate, yCoordinate);
            }
        }

        public void ResetGame()
        {
            Board = new PlayerTokens?[3, 3];
            Winner = null;
        }

        private void AddMoveToHistory(int xCoordinate, int yCoordinate)
        {
            MoveRecord moveRecord = CreateMoveRecord(xCoordinate, yCoordinate);
            MoveHistory.Add(moveRecord);
        }

        private GameOverException CreateGameOverException(string message)
        {
            return new GameOverException(message);
        }

        private InvalidPlacementException CreateInvalidPlacementException(int xCoordinate, int yCoordinate)
        {
            return new InvalidPlacementException(
                $"Coordinate '[{xCoordinate}, {yCoordinate}]' has already been played. Choose another coordinate.");
        }

        private MoveRecord CreateMoveRecord(int xCoordinate, int yCoordinate)
        {
            return new MoveRecord(xCoordinate, yCoordinate);
        }

        private bool GameHasNotBeenWon()
        {
            return Winner == null;
        }

        private PlayerTokens GetCurrentPlayer()
        {
            if (MoveHistory.Count % 2 == 0)
            {
                return PlayerTokens.X;
            }

            return PlayerTokens.Y;
        }

        private PlayerTokens GetPreviousPlayer()
        {
            if (MoveHistory.Count % 2 == 1)
            {
                return PlayerTokens.X;
            }

            return PlayerTokens.Y;
        }

        private bool IsValidPlacement(int xCoordinate, int yCoordinate)
        {
            return !PlacementHasBeenPlayed(xCoordinate, yCoordinate) && GameHasNotBeenWon();
        }

        private bool PlacementHasBeenPlayed(int xCoordinate, int yCoordinate)
        {
            return MoveHistory.Any(record => record.X == xCoordinate && record.Y == yCoordinate);
        }

        private void RejectMove(int xCoordinate, int yCoordinate)
        {
            if (GameHasNotBeenWon())
            {
                throw CreateInvalidPlacementException(xCoordinate, yCoordinate);
            }

            throw CreateGameOverException($"Game has ended. Player {Winner} has won.");
        }

        private void ValidateWin()
        {
            if (winValidator.ValidateWinCondition(Board))
            {
                Winner = GetPreviousPlayer();
            }
        }
    }
}