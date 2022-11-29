using System;
using System.Text.RegularExpressions;

namespace BankManagementLibrary
{
    internal class Card
    {
        private string number;
        public string Number
        {
            get { return number; }
        }
        public Card(string number)
        {
            if (string.IsNullOrEmpty(number))
                throw new ArgumentNullException();
            else if (!Regex.IsMatch(number, @"^\d{4}\s\d{4}\s\d{4}\s\d{4}$"))
                throw new ArgumentException();
            this.number = number;
        }

        public override bool Equals(object obj)
        {
            return obj is Card model &&
                   number == model.number;
        }

        public override int GetHashCode()
        {
            return number.GetHashCode();
        }
    }
}