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
            Assert.Contains(expected, bank.accounts);
        }

        [Fact]
        public void RegisterAccount_UniqueCheck_ShouldNotAddAccountToList()
        {
            //Arrange
            var bank = new Bank();
            var expected = false;

            //Act
            var actual = bank.RegisterAccount("John", "Wick", "JohnWick@gmail.com", "+380000000000", "123456789");
            actual = bank.RegisterAccount("Snoop", "Dogg", "JohnWick@gmail.com", "+380000000000", "123456789");

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}