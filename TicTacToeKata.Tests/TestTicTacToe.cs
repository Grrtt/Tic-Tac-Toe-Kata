namespace TicTacToeKata.Tests
{
    using NSubstitute;

    using NUnit.Framework;

    using TicTacToeKata.Abstract;
    using TicTacToeKata.Exceptions;
    using TicTacToeKata.Model;

    [TestFixture]
    public class TestTicTacToe
    {
        private TicTacToe systemUnderTest;

        private IWinValidator winValidatorMock;

        [Test]
        public void PlaceToken_OnEvenTurn_PlayerYIsCurrentPlayer()
        {
            InvokePlaceToken(0, 0);
            InvokePlaceToken(2, 2);
            InvokePlaceToken(0, 1);
            InvokePlaceToken(1, 2);

            Assert.That(systemUnderTest.Board[2, 2], Is.EqualTo(PlayerTokens.Y));
            Assert.That(systemUnderTest.Board[1, 2], Is.EqualTo(PlayerTokens.Y));
            Assert.That(systemUnderTest.Board[0, 0], Is.Not.EqualTo(PlayerTokens.Y));
            Assert.That(systemUnderTest.Board[0, 1], Is.Not.EqualTo(PlayerTokens.Y));
        }

        [Test]
        public void PlaceToken_OnFifthMove_BeginsCheckingForWinner()
        {
            winValidatorMock.ValidateWinCondition(Arg.Any<PlayerTokens?[,]>()).Returns(true);

            InvokePlaceToken(0, 0);
            InvokePlaceToken(0, 1);
            InvokePlaceToken(1, 0);
            InvokePlaceToken(0, 2);
            InvokePlaceToken(2, 0);

            winValidatorMock.Received(1).ValidateWinCondition(systemUnderTest.Board);
        }

        [Test]
        public void PlaceToken_WhenGameHasBeenWon_DoesNotAllowPlacementOfAnyTokens()
        {
            winValidatorMock.ValidateWinCondition(Arg.Any<PlayerTokens?[,]>()).Returns(true);

            InvokePlaceToken(0, 0);
            InvokePlaceToken(2, 2);
            InvokePlaceToken(0, 1);
            InvokePlaceToken(2, 1);
            InvokePlaceToken(0, 2);

            Assert.Throws(
                Is.TypeOf<GameOverException>().And.Message.EqualTo($"Game has ended. Player {PlayerTokens.X} has won."),
                () => InvokePlaceToken(2, 0));
        }

        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 0)]
        [TestCase(2, 2)]
        public void PlaceToken_WhenInvoked_PlacesTokenOnBoard(int xCoordinate, int yCoordinate)
        {
            InvokePlaceToken(xCoordinate, yCoordinate);

            Assert.That(systemUnderTest.Board[xCoordinate, yCoordinate], Is.EqualTo(PlayerTokens.X));
        }

        [Test]
        public void PlaceToken_WhenSpaceIsTaken_ThrowsInvalidPlacementException(
            [Values(0)] int xCoordinate,
            [Values(0)] int yCoordinate)
        {
            InvokePlaceToken(xCoordinate, yCoordinate);

            Assert.Throws(
                Is.TypeOf<InvalidPlacementException>().And.Message.EqualTo(
                    $"Coordinate '[{xCoordinate}, {yCoordinate}]' has already been played. Choose another coordinate."),
                () => InvokePlaceToken(xCoordinate, yCoordinate));
        }

        [Test]
        public void ResetGame_WhenInvoked_ClearsBoardOfPlayerTokens()
        {
            InvokeResetGame();

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    Assert.That(systemUnderTest.Board[x, y], Is.Null);
                }
            }
        }

        [Test]
        public void ResetGame_WhenInvoked_SetsWinnerToNull()
        {
            winValidatorMock.ValidateWinCondition(systemUnderTest.Board).Returns(true);

            InvokePlaceToken(0, 0);
            InvokePlaceToken(0, 1);
            InvokePlaceToken(0, 2);
            InvokePlaceToken(1, 0);
            InvokePlaceToken(1, 1);

            InvokeResetGame();

            Assert.That(systemUnderTest.Winner, Is.Null);
        }

        [SetUp]
        public void SetUp()
        {
            winValidatorMock = CreateWinValidatorMock();
            systemUnderTest = CreateSystemUnderTest();
        }

        private TicTacToe CreateSystemUnderTest()
        {
            return new TicTacToe(winValidatorMock);
        }

        private IWinValidator CreateWinValidatorMock()
        {
            return Substitute.For<IWinValidator>();
        }

        private void InvokePlaceToken(int xCoordinate, int yCoordinate)
        {
            systemUnderTest.PlaceToken(xCoordinate, yCoordinate);
        }

        private void InvokeResetGame()
        {
            systemUnderTest.ResetGame();
        }
    }
}