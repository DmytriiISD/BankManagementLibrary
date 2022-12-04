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
        }

        public void UpdateBalance(decimal money)
        {
            if ((balance + money) < 0)
                throw new ArithmeticException();
            else
                balance = balance + money;
        }

        public override bool Equals(object obj)
        {
            return obj is Card model &&
                   number == model.number &&
                   balance == model.balance;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(balance, number);
        }
    }
}
