using System;

namespace BankManagementLibrary
{
    internal class Bank
    {
        public Bank()
        {

        }

        public bool RegisterAccount(string firstName, string lastName,
            string email, string phoneNumber, string passportId)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email)
                || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(passportId))
                return false;
            else return true;
        }
    }
}