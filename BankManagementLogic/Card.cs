using System;
using System.Text.RegularExpressions;

namespace BankManagementLibrary
{
    internal class Card
    {
        private string number;
        private decimal balance;
        public string Number
        {
            get { return number; }
        }

        public decimal Balance
        {
            get { return balance; }
        }
        public Card(string number)
        {
            if (string.IsNullOrEmpty(number))
                throw new ArgumentNullException();
            else if (!Regex.IsMatch(number, @"^\d{4}\s\d{4}\s\d{4}\s\d{4}$"))
                throw new ArgumentException();
            this.number = number;
            balance = 0;
        }
        public Card(string number,int balance)
        {
            this.number = number;
            this.balance = balance;
        }

        public override bool Equals(object obj)
        {
            return obj is Card model &&
                   number == model.number &&
                   balance== model.balance;
        }

        public override int GetHashCode()
        {
            // return number.GetHashCode();
            return HashCode.Combine(number, balance);
        }


        public bool UpdateBalanceInCard(decimal money)
        {
            balance = balance + money;
           if(balance < 0)
                return false;
            return true;
        }

      

    }
}