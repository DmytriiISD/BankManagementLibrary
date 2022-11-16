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
            var actual = bank.RegisterAccount("", "", "", "", "");

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}