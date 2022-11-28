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
        [InlineData(null, null, null, null, null)]
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
        [InlineData("")]
        [InlineData(null)]
        public void ReturnAccount_WrongInputData_ShouldTrowException(string value)
        {
            //Arrange
            var bank = new Bank();
            bank.RegisterAccount("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");

            //Assert
            Assert.Throws<ArgumentException>(() => bank.ReturnAccount(value));
        }

        [Theory]
        [InlineData("+380000000000", "0000 0000 0000 0001")]
        [InlineData("+380111111111", "0000 0000 0000 0001")]
        public void AddCreditCard_UniqueCheck_ShouldNotAddCreditCardToList(string ph, string card)
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
    }
}