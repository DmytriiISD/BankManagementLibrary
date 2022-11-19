using System;

namespace BankManagementLibrary
{
    public class Card
    {
        private string number;
        private int balance;
        public string Number
        {
            get { return number; }
        }

        public int Balance
        {
            get { return balance; }
        }
        public Card(string number)
        {
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

        public bool AddMoneyInCard(int money)
        {

            if (money <= 0)
                return false;

            balance = balance + money;
            if (balance <= 0)
                return false;
            return true;
        }

        public bool TakeMoneyFromCard(int money)
        {

            if (money <= 0)
                return false;

            balance = balance - money;
            if (balance < 0)
                return false;
            return true;
        }
    }
}