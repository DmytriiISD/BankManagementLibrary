using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BankManagementLibrary
{
    public class Account
    {
        public List<Card> cards;

        private string firstName;

        private string lastName;

        private string email;

        public string FirstName
        {
            get { return firstName; }
        }

        public string LastName
        {
            get { return lastName; }
        }
        public string Email
        {
            get { return email; }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
        }

        private string passportId;
        public string PassportId
        {
            get { return passportId; }
        }

        public Account(string firstName, string lastName, string email, string phoneNumber, string passportId)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.passportId = passportId;
            cards = new List<Card>();
        }

        public override bool Equals(object obj)
        {
            return obj is Account model &&
                   firstName == model.firstName &&
                   lastName == model.lastName &&
                   email == model.email &&
                   phoneNumber == model.phoneNumber &&
                   passportId == model.passportId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(firstName, lastName, email, phoneNumber, passportId);
        }

        public bool AddCreditCard(string number)
        {
            if (!Regex.IsMatch(number, @"^\d{4}\s\d{4}\s\d{4}\s\d{4}$"))
                return false;
            Card card = new Card(number);
            foreach (Account acc in Bank.accounts)
                foreach (Card temp in acc.cards)
                    if (temp.Number == number)
                        return false;
            cards.Add(card);
            if (!cards.Contains(card))
                return false;
            else return true;
        }

        public bool RemoveCreditCard(string number)
        {
            if (!Regex.IsMatch(number, @"^\d{4}\s\d{4}\s\d{4}\s\d{4}$"))
                return false;
            else if (cards.Exists(x => x.Number == number))
                cards.Remove(cards.Find(x => x.Number == number));
            else return false;
            if (cards.Exists(x => x.Number == number))
                return false;
            else return true;
        }

        public Card ReturnCreditCard(string number)
        {
            if (!Regex.IsMatch(number, @"^\d{4}\s\d{4}\s\d{4}\s\d{4}$"))
                throw new ArgumentException();
            else if (cards.Exists(x => x.Number == number))
                return cards.Find(x => x.Number == number);
            else throw new ArgumentException();
        }

        public bool CardsInfoByAccount()
        {


            if (cards == null)
            {
                Console.WriteLine("Карток не знайдено");
                return false;
            }


            Console.WriteLine($"Акаунт:{FirstName} {LastName}");
            Console.WriteLine("Кредитні картки:");
            cards.Sort((a, b) => b.Balance - a.Balance);
            foreach (Card card in cards)
                Console.WriteLine($"Номер: {card.Number} Баланс:{card.Balance}");

            return true;
        }

    }
}