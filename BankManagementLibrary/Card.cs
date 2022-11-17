using System;

namespace BankManagementLibrary
{
    public class Card
    {
        private string number;
        public string Number
        {
            get { return number; }
        }
        public Card(string number)
        {
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