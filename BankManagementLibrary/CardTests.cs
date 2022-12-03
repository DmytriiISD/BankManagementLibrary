using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BankManagementLibrary
{
    public class CardTests
    {
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
            var card = new Card("0000 0000 0000 0001",100);

            var expected = false;

            //Act
            var actual = card.UpdateBalanceInCard(money);


            //Assert
            Assert.Equal(expected, actual);
        }

    }
}
