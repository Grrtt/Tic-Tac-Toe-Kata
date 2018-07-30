namespace TicTacToeKata.Tests
{
    using System;

    using NUnit.Framework;

    using TicTacToeKata.Model;

    [TestFixture]
    public class TestWinValidator
    {
        private WinValidator systemUnderTest;

        [SetUp]
        public void SetUp()
        {
            systemUnderTest = CreateSystemUnderTest();
        }

        [Test]
        public void ValidateWinCondition_WhenBoardIsFilledWithNulls_ReturnsFalse()
        {
            PlayerTokens?[,] gameBoard = { { null, null, null }, { null, null, null }, { null, null, null } };

            Assert.IsFalse(systemUnderTest.ValidateWinCondition(gameBoard));
        }

        [Test]
        public void ValidateWinCondition_WhenBoardIsNull_ThrowsException()
        {
            Assert.Throws(
                Is.TypeOf<ArgumentException>().And.Message.EqualTo("No board to validate."),
                () => systemUnderTest.ValidateWinCondition(null));
        }

        [Test]
        public void ValidateWinCondition_WhenFirstColumnIsFilledWithTheSamePlayerToken_ReturnsTrue()
        {
            PlayerTokens?[,] gameBoard =
                {
                    { PlayerTokens.X, PlayerTokens.Y, PlayerTokens.X },
                    { PlayerTokens.X, PlayerTokens.Y, PlayerTokens.X },
                    { PlayerTokens.X, PlayerTokens.X, PlayerTokens.Y }
                };

            Assert.IsTrue(systemUnderTest.ValidateWinCondition(gameBoard));
        }

        [Test]
        public void ValidateWinCondition_WhenFirstRowIsFilledWithTheSamePlayerToken_ReturnsTrue()
        {
            PlayerTokens?[,] gameBoard =
                {
                    { PlayerTokens.X, PlayerTokens.X, PlayerTokens.X },
                    { PlayerTokens.Y, PlayerTokens.Y, PlayerTokens.X },
                    { PlayerTokens.X, PlayerTokens.X, PlayerTokens.Y }
                };

            Assert.IsTrue(systemUnderTest.ValidateWinCondition(gameBoard));
        }

        [Test]
        public void ValidateWinCondition_WhenNegativeDiagonalIsFilledWithTheSamePlayerToken_ReturnsTrue()
        {
            PlayerTokens?[,] gameBoard =
                {
                    { PlayerTokens.Y, PlayerTokens.Y, PlayerTokens.X },
                    { PlayerTokens.Y, PlayerTokens.X, PlayerTokens.Y },
                    { PlayerTokens.X, PlayerTokens.Y, PlayerTokens.Y }
                };

            Assert.IsTrue(systemUnderTest.ValidateWinCondition(gameBoard));
        }

        [Test]
        public void ValidateWinCondition_WhenPositiveDiagonalIsFilledWithTheSamePlayerToken_ReturnsTrue()
        {
            PlayerTokens?[,] gameBoard =
                {
                    { PlayerTokens.X, PlayerTokens.Y, PlayerTokens.Y },
                    { PlayerTokens.Y, PlayerTokens.X, PlayerTokens.Y },
                    { PlayerTokens.Y, PlayerTokens.Y, PlayerTokens.X }
                };

            Assert.IsTrue(systemUnderTest.ValidateWinCondition(gameBoard));
        }

        [Test]
        public void ValidateWinCondition_WhenSecondColumnIsFilledWithTheSamePlayerToken_ReturnsTrue()
        {
            PlayerTokens?[,] gameBoard =
                {
                    { PlayerTokens.X, PlayerTokens.X, PlayerTokens.Y },
                    { PlayerTokens.Y, PlayerTokens.X, PlayerTokens.X },
                    { PlayerTokens.X, PlayerTokens.X, PlayerTokens.Y }
                };

            Assert.IsTrue(systemUnderTest.ValidateWinCondition(gameBoard));
        }

        [Test]
        public void ValidateWinCondition_WhenSecondRowIsFilledWithTheSamePlayerToken_ReturnsTrue()
        {
            PlayerTokens?[,] gameBoard =
                {
                    { PlayerTokens.X, PlayerTokens.Y, PlayerTokens.Y },
                    { PlayerTokens.X, PlayerTokens.X, PlayerTokens.X },
                    { PlayerTokens.Y, PlayerTokens.X, PlayerTokens.Y }
                };

            Assert.IsTrue(systemUnderTest.ValidateWinCondition(gameBoard));
        }

        [Test]
        public void ValidateWinCondition_WhenThirdColumnIsFilledWithTheSamePlayerToken_ReturnsTrue()
        {
            PlayerTokens?[,] gameBoard =
                {
                    { PlayerTokens.Y, PlayerTokens.X, PlayerTokens.X },
                    { PlayerTokens.Y, PlayerTokens.X, PlayerTokens.X },
                    { PlayerTokens.X, PlayerTokens.Y, PlayerTokens.X }
                };

            Assert.IsTrue(systemUnderTest.ValidateWinCondition(gameBoard));
        }

        [Test]
        public void ValidateWinCondition_WhenThirdRowIsFilledWithTheSamePlayerToken_ReturnsTrue()
        {
            PlayerTokens?[,] gameBoard =
                {
                    { PlayerTokens.X, PlayerTokens.X, PlayerTokens.Y },
                    { PlayerTokens.Y, PlayerTokens.Y, PlayerTokens.X },
                    { PlayerTokens.X, PlayerTokens.X, PlayerTokens.X }
                };

            Assert.IsTrue(systemUnderTest.ValidateWinCondition(gameBoard));
        }

        private WinValidator CreateSystemUnderTest()
        {
            return new WinValidator();
        }
    }
}