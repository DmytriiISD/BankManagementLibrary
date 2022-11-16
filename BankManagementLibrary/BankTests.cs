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

        [Fact]
        public void RegisterAccount_WrongInputData_ShouldCreateAccount()
        {
            //Arrange
            var bank = new Bank();
            var expected = false;

            //Act
            var actual = bank.RegisterAccount("", "", "", "", "");

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}