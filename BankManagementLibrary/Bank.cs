using System;
using System.Text.RegularExpressions;

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
            if (firstName.Length < 2 || firstName.Length > 20 || !Regex.IsMatch(firstName.Substring(1), @"^[a-z]+$") ||
                !Regex.IsMatch(firstName[0].ToString(), @"^[A-Z]+$"))
                return false;
            else if (lastName.Length < 2 || lastName.Length > 20 || !Regex.IsMatch(lastName.Substring(1), @"^[a-z]+$") ||
                !Regex.IsMatch(lastName[0].ToString(), @"^[A-Z]+$"))
                return false;
            else if (!Regex.Match(email, "[.\\-_A-Za-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}").Success)
                return false;
            else if (!Regex.IsMatch(phoneNumber, @"^\+[3][8][0]\d{9}$"))
                return false;
            else if (!Regex.IsMatch(passportId, @"^\d{9}$"))
                return false;
            else return true;
        }
    }
}