using System;
using Xunit;

namespace BankManagementLibrary
{
    public class CardTests
    {
        [Fact]
        public void CreateCreditCard_CorrectInputData_ShouldCreateCard()
        {
            //Arrange
            Card card;
            var expected = "0000 0000 0000 0000";

            //Act
            card = new Card("0000 0000 0000 0000");

            //Assert
            Assert.Equal(expected, card.Number);
        }

        [Theory]
        [InlineData("0000 0000 0000 000")]
        [InlineData("0000000000000000")]
        [InlineData("0000 0000 0000 0001a")]
        [InlineData("000a 0000 0000 0001")]
        public void CreateCreditCard_WrongInputData_ShouldThrowException(string number)
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new Card(number));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CreateCreditCard_WrongInputData_ShouldThrowNullException(string number)
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new Card(number));
        }

        [Fact]
        public void UpdateBalanceInCard_CorrectInputData_ShouldAddMoneyInCard()
        {
            //Arrange
            var card = new Card("0000 0000 0000 0001");

            var expected = true;

            //Act
            var actual = card.UpdateBalanceInCard(50);


            //Assert
            Assert.Equal(expected, actual);
        }



        [Theory]
        [InlineData(-1000)]
        [InlineData(-101)]


        public void UpdateBalanceInCard_WrongInputData_ShouldNotUpdateBalanceInCard(int money)
        {
            //Arrange
            var card = new Card("0000 0000 0000 0001", 100);

            var expected = false;

            //Act
            var actual = card.UpdateBalanceInCard(money);


            //Assert
            Assert.Equal(expected, actual);
        }


    }
}
