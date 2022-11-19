using Newtonsoft.Json.Linq;
using System;
using Xunit;

namespace BankManagementLibrary
{
    public class BankTests
    {
        [Fact]
        public void RegisterAccount_CorrectInputData_ShouldCreateAccount()
        {
            //Arrange
            var bank = new Bank();
            var expected = true;

            //Act
            var actual = bank.RegisterAccount("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", "", "", "", "")]
        [InlineData("john", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789")]
        [InlineData("JOhn", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789")]
        [InlineData("J", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789")]
        [InlineData("John", "wick", "JohnWick@gmail.com", "+380000000000", "123456789")]
        [InlineData("John", "WIck", "JohnWick@gmail.com", "+380000000000", "123456789")]
        [InlineData("John", "W", "JohnWick@gmail.com", "+380000000000", "123456789")]
        [InlineData("John", "Wick", "JohnWickgmail.com", "+380000000000", "123456789")]
        [InlineData("John", "Wick", "JohnWick@gmailcom", "+380000000000", "123456789")]
        [InlineData("John", "Wick", "@gmail.com", "+380000000000", "123456789")]
        [InlineData("John", "Wick", "JohnWick@gmail.com", "+381000000000", "123456789")]
        [InlineData("John", "Wick", "JohnWick@gmail.com", "-380000000000", "123456789")]
        [InlineData("John", "Wick", "JohnWick@gmail.com", "+38000000000", "123456789")]
        [InlineData("John", "Wick", "JohnWick@gmail.com", "+3800000000000", "123456789")]
        [InlineData("John", "Wick", "JohnWick@gmail.com", "+38000abc0000", "123456789")]
        [InlineData("John", "Wick", "JohnWick@gmail.com", "+380000000000", "0123456789")]
        [InlineData("John", "Wick", "JohnWick@gmail.com", "+380000000000", "12345678")]
        [InlineData("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123abc789")]
        public void RegisterAccount_WrongInputData_ShouldNotCreateAccount(string firstName,
            string lastName, string email, string phoneNumber, string passportId)
        {
            //Arrange
            var bank = new Bank();
            var expected = false;

            //Act
            var actual = bank.RegisterAccount(firstName, lastName, email, phoneNumber, passportId);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RegisterAccount_AddToList_ShouldAddAccountToList()
        {
            //Arrange
            var bank = new Bank();
            var expected = new Account("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");

            //Act
            bank.RegisterAccount("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");

            //Assert
            Assert.Contains(expected, bank.Accounts);
        }

        [Theory]
        [InlineData("John", "Wick", "SnoopDogg@gmail.com", "+380000000001", "123456789")]
        [InlineData("John", "Wick", "DrDre@gmail.com", "+380000000000", "123456799")]
        [InlineData("John", "Wick", "JohnWick@gmail.com", "+380000000002", "023456789")]
        public void RegisterAccount_UniqueCheck_ShouldNotAddAccountToList(string firstName,
            string lastName, string email, string phoneNumber, string passportId)
        {
            //Arrange
            var bank = new Bank();
            var expected = false;
            bank.RegisterAccount("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");

            //Act
            var actual = bank.RegisterAccount(firstName, lastName, email, phoneNumber, passportId);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReturnAccount_CorrectInputData_ShouldReturnAccount()
        {
            //Arrange
            var bank = new Bank();
            var expected = "+380000000000";
            bank.RegisterAccount("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");

            //Act
            var actual = bank.ReturnAccount(expected).PhoneNumber;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("+3800000000001")]
        [InlineData("380000000000")]
        [InlineData("+38000000000")]
        [InlineData("+111000000000")]
        [InlineData("+380123456789")]
        public void ReturnAccount_WrongInputData_ShouldTrowException(string value)
        {
            //Arrange
            var bank = new Bank();
            bank.RegisterAccount("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");

            //Assert
            Assert.Throws<ArgumentException>(() => bank.ReturnAccount(value));
        }

        [Fact]
        public void AddCreditCard_CorrectInputData_ShouldCreateCreditCard()
        {
            //Arrange
            var bank = new Bank();
            bank.RegisterAccount("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");
            var expected = true;

            //Act
            var actual = bank.ReturnAccount("+380000000000").AddCreditCard("0000 0000 0000 0000");

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddCreditCard_CorrectInputData_ShouldAddCreditCardToList()
        {
            //Arrange
            var bank = new Bank();
            bank.RegisterAccount("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");
            var expected = new Card("0000 0000 0000 0001");

            //Act
            bank.ReturnAccount("+380000000000").AddCreditCard("0000 0000 0000 0001");

            //Assert
            Assert.Contains(expected, bank.ReturnAccount("+380000000000").cards);
        }

        [Theory]
        [InlineData("+380000000000", "0000 0000 0000 0001")]
        [InlineData("+380111111111", "0000 0000 0000 0001")]
        public void AddCreditCard_WrongInputData_ShouldNotAddCreditCardToList(string ph, string card)
        {
            //Arrange
            var bank = new Bank();
            bank.RegisterAccount("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");
            bank.RegisterAccount("Snoop", "Dogg", "SnoopDogg@gmail.com", "+380111111111", "223456789");
            bank.ReturnAccount("+380000000000").AddCreditCard("0000 0000 0000 0001");
            var expected = false;

            //Act
            var actual = bank.ReturnAccount(ph).AddCreditCard(card);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveCreditCard_CorrectInputData_ShouldRemoveCardFromAccount()
        {
            //Arrange
            var bank = new Bank();
            bank.RegisterAccount("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");
            bank.ReturnAccount("+380000000000").AddCreditCard("0000 0000 0000 0001");
            bank.ReturnAccount("+380000000000").AddCreditCard("0000 0001 0000 0001");
            bank.ReturnAccount("+380000000000").AddCreditCard("0000 0001 0001 0001");
            var expected = true;

            //Act
            var actual = bank.ReturnAccount("+380000000000").RemoveCreditCard("0000 0001 0000 0001");

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("0000 0000 0000 0001")]
        [InlineData("0000 0000 0001 000a")]
        [InlineData("0000 0001 0001 0001")]
        public void RemoveCreditCard_WrongInputData_ShouldNotRemoveCardFromAccount(string card)
        {
            //Arrange
            var bank = new Bank();
            bank.RegisterAccount("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");
            bank.ReturnAccount("+380000000000").AddCreditCard("0000 0000 0000 0001");
            bank.ReturnAccount("+380000000000").AddCreditCard("0000 0001 0000 0001");
            bank.ReturnAccount("+380000000000").RemoveCreditCard("0000 0000 0000 0001");
            var expected = false;

            //Act
            var actual = bank.ReturnAccount("+380000000000").RemoveCreditCard(card);

            //Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void ReturnCreditCard_CorrectInputData_ShouldReturnCreditCard()
        {
            //Arrange
            var bank = new Bank();
            var expected = "0000 0000 0000 0001";
            bank.RegisterAccount("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");
            bank.ReturnAccount("+380000000000").AddCreditCard("0000 0000 0000 0001");

            //Act
            var actual = bank.ReturnAccount("+380000000000").ReturnCreditCard(expected).Number;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("+380000000000", "0000 0000 0000 0000")]
        [InlineData("+380111111111", "0000 0000 0000 0001")]
        [InlineData("+380000000000", "0000 0000 0001 0000")]
        [InlineData("+380111111111", "0000 0000 0000 0000")]

        public void ReturnCreditCard_WrongInputData_ShouldTrowException(string ph, string card)
        {
            //Arrange
            var bank = new Bank();
            var expected = "0000 0000 0000 0001";
            bank.RegisterAccount("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");
            bank.RegisterAccount("Snoop", "Dogg", "SnoopDogg@gmail.com", "+380111111111", "223456789");
            bank.ReturnAccount("+380000000000").AddCreditCard("0000 0000 0000 0001");
            bank.ReturnAccount("+380111111111").AddCreditCard("0000 0000 0001 0000");

            //Assert
            Assert.Throws<ArgumentException>(() => bank.ReturnAccount(ph).ReturnCreditCard(card));
        }

        [Fact]
        public void AddMoneyInCard_CorrectInputData_ShouldAddMoneyInCard()
        {
            //Arrange
            var bank = new Bank();
            bank.RegisterAccount("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");
            bank.ReturnAccount("+380000000000").AddCreditCard("0000 0000 0000 0001");
            var expected = true;

            //Act
            var actual = bank.ReturnAccount("+380000000000").ReturnCreditCard("0000 0000 0000 0001").AddMoneyInCard(50);
           

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-1000)]
        [InlineData(-100)]
        [InlineData(0)]

        public void AddMoneyInCard_WrongInputData_ShouldNotAddMoneyInCard(int money)
        {
            //Arrange
            var bank = new Bank();
            bank.RegisterAccount("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");
            bank.ReturnAccount("+380000000000").AddCreditCard("0000 0000 0000 0001");
            var expected = false;

            //Act
            var actual = bank.ReturnAccount("+380000000000").ReturnCreditCard("0000 0000 0000 0001").AddMoneyInCard(money);


            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TakeMoneyFromCard_CorrectInputData_ShouldTakeMoneyFromCard()
        {
            //Arrange
            var bank = new Bank();
            bank.RegisterAccount("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");
            bank.ReturnAccount("+380000000000").AddCreditCard("0000 0000 0000 0001");
            bank.ReturnAccount("+380000000000").ReturnCreditCard("0000 0000 0000 0001").AddMoneyInCard(50);
            var expected = true;

            //Act
            var actual = bank.ReturnAccount("+380000000000").ReturnCreditCard("0000 0000 0000 0001").TakeMoneyFromCard(50);


            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(75)]
        [InlineData(-100)]
        [InlineData(0)]

        public void TakeMoneyFromCard_WrongInputData_ShouldNotTakeMoneyFromCard(int money)
        {
            //Arrange
            var bank = new Bank();
            bank.RegisterAccount("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");
            bank.ReturnAccount("+380000000000").AddCreditCard("0000 0000 0000 0001");
            bank.ReturnAccount("+380000000000").ReturnCreditCard("0000 0000 0000 0001").AddMoneyInCard(50);
            var expected = false;

            //Act
            var actual = bank.ReturnAccount("+380000000000").ReturnCreditCard("0000 0000 0000 0001").TakeMoneyFromCard(money);


            //Assert
            Assert.Equal(expected, actual);
        }

    }
}