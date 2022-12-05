using System;
using System.Collections.Generic;
using System.Linq;
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

        public Bank(IGetAccounts source)
        {
            source.GetList();
        }

        public bool RegisterAccount(string firstName, string lastName,
            string email, string phoneNumber, string passportId)
        {
            Account account;
            try
            {
                account = new Account(firstName, lastName, email, phoneNumber, passportId);
            }
            catch (ArgumentNullException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            if (accounts.Exists(x => x.PassportId == passportId || x.PhoneNumber == phoneNumber ||
              x.Email == email))
                return false;
            accounts.Add(account);
            if (!accounts.Contains(account))
                return false;
            else return true;
        }

        public Account ReturnAccount(string phNumber)
        {
            if (string.IsNullOrEmpty(phNumber))
                throw new ArgumentException();
            else if (!Regex.IsMatch(phNumber, @"^\+[3][8][0]\d{9}$"))
                throw new ArgumentException();
            else if (accounts.Exists(x => x.PhoneNumber == phNumber))
                return accounts.Find(x => x.PhoneNumber == phNumber);
            else throw new ArgumentException();
        }

        public void GetSummary()
        {
            accounts = accounts.OrderBy(x => x.Cards.Sum(x => x.Balance)).ToList();
            accounts.ForEach(x => x.Cards = x.Cards.OrderBy(x => x.Balance).ToList());
        }

        public void GetSummary(IAccount filter)
        {
            filter.Sort();
        }
    }

    public interface IAccount
    {
        public void Sort();
    }

    internal class SortByPassportID : IAccount
    {
        public void Sort()
        {
            Bank.accounts = Bank.accounts.OrderBy(x => x.PassportId).ToList();
        }
    }

    internal class SortByCardNumber : IAccount
    {
        public void Sort()
        {
            Bank.accounts = Bank.accounts.OrderBy(x => x.Cards.Max(x => x.Number)).ToList();
        }
    }

    public interface IGetAccounts
    {
        public void GetList();
    }

    internal class DBSource : IGetAccounts
    {
        public void GetList()
        {

        }
    }
}
