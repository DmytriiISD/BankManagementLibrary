using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BankManagementLibrary
{
    internal class Bank
    {
        static public List<Account> accounts;
        public List<Account> Accounts
        {
            get { return accounts; }
        }
        public Bank()
        {
            accounts = new List<Account>();
        }

        public bool RegisterAccount(string firstName, string lastName,
            string email, string phoneNumber, string passportId)
        {
            if (firstName.Length < 2 || firstName.Length > 100 || !Regex.IsMatch(firstName.Substring(1), @"^[a-z]+$") ||
                !Regex.IsMatch(firstName[0].ToString(), @"^[A-Z]+$"))
                return false;
            else if (lastName.Length < 2 || lastName.Length > 100 || !Regex.IsMatch(lastName.Substring(1), @"^[a-z]+$") ||
                !Regex.IsMatch(lastName[0].ToString(), @"^[A-Z]+$"))
                return false;
            else if (!Regex.Match(email, "[.\\-_A-Za-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}").Success)
                return false;
            else if (!Regex.IsMatch(phoneNumber, @"^\+[3][8][0]\d{9}$"))
                return false;
            else if (!Regex.IsMatch(passportId, @"^\d{9}$"))
                return false;
            Account account = new Account(firstName, lastName, email, phoneNumber, passportId);
            foreach (Account acc in accounts)
                if (acc.PassportId == passportId || acc.PhoneNumber == phoneNumber || acc.Email == email)
                    return false;
            accounts.Add(account);
            if (!accounts.Contains(account))
                return false;
            else return true;
        }

        public Account ReturnAccount(string phNumber)
        {
            if (!Regex.IsMatch(phNumber, @"^\+[3][8][0]\d{9}$"))
                throw new ArgumentException();
            else if (accounts.Exists(x => x.PhoneNumber == phNumber))
                return accounts.Find(x => x.PhoneNumber == phNumber);
            else throw new ArgumentException();
        }
    }
}