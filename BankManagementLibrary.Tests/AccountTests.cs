using Xunit;

namespace BankManagementLibrary
{
    public class AccountTests
    {
        [Fact]
        public void AddCreditCard_CorrectInputData_ShouldCreateCreditCard()
        {
            //Arrange
            var account = new Account("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");
            var expected = true;

            //Act
            var actual = account.AddCreditCard("0000 0000 0000 0000");

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddCreditCard_CorrectInputData_ShouldAddCreditCardToList()
        {
            //Arrange
            var account = new Account("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");
            var expected = new Card("0000 0000 0000 0001");

            //Act
            account.AddCreditCard("0000 0000 0000 0001");

            //Assert
            Assert.Contains(expected, account.Cards);
        }

        [Theory]
        [InlineData("0000 0000 0000 000")]
        [InlineData("0000000000000000")]
        [InlineData("0000 0000 0000 0001a")]
        [InlineData("000a 0000 0000 0001")]
        [InlineData("")]
        [InlineData(null)]
        public void AddCreditCard_WrongInputData_ShouldNotAddCreditCardToList(string card)
        {
            //Arrange
            var account = new Account("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");
            var expected = false;

            //Act
            var actual = account.AddCreditCard(card);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveCreditCard_CorrectInputData_ShouldRemoveCardFromAccount()
        {
            //Arrange
            var account = new Account("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");
            account.AddCreditCard("0000 0000 0000 0001");
            account.AddCreditCard("0000 0001 0000 0001");
            account.AddCreditCard("0000 0001 0001 0001");
            var expected = true;

            //Act
            var actual = account.RemoveCreditCard("0000 0001 0000 0001");

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("0000 0000 0000 0001")]
        [InlineData("0000 0000 0001 000a")]
        [InlineData("0000 0001 0001 0001")]
        [InlineData(null)]
        [InlineData("")]
        public void RemoveCreditCard_WrongInputData_ShouldNotRemoveCardFromAccount(string card)
        {
            //Arrange
            var account = new Account("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");
            account.AddCreditCard("0000 0000 0000 0001");
            account.AddCreditCard("0000 0001 0000 0001");
            account.RemoveCreditCard("0000 0000 0000 0001");
            var expected = false;

            //Act
            var actual = account.RemoveCreditCard(card);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetStatement_ShouldSortCardsInListByAscendingBalance()
        {
            //Arrange
            var account = new Account("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");
            account.AddCreditCard("0000 0000 0000 0001");
            account.AddCreditCard("0000 0000 0000 0002");
            account.AddCreditCard("0000 0000 0000 0003");
            account.Cards.Find(x => x.Number == "0000 0000 0000 0001").UpdateBalance(300);
            account.Cards.Find(x => x.Number == "0000 0000 0000 0002").UpdateBalance(100);
            account.Cards.Find(x => x.Number == "0000 0000 0000 0003").UpdateBalance(200);

            //Act
            account.GetStatement();

            //Assert
            Assert.Equal("0000 0000 0000 0002", account.Cards[0].Number);
            Assert.Equal("0000 0000 0000 0003", account.Cards[1].Number);
            Assert.Equal("0000 0000 0000 0001", account.Cards[2].Number);
        }
    }
}
