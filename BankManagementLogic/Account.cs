using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BankManagementLibrary
{
    internal class Account
    {
        private List<Card> cards;
        public List<Card> Cards
        { 
            get { return cards; }
        }

        private string firstName;

        private string lastName;

        private string email;
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
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email)
                || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(passportId))
                throw new ArgumentNullException();
            else if (firstName.Length < 2 || firstName.Length > 100 || !Regex.IsMatch(firstName.Substring(1), @"^[a-z]+$") ||
                !Regex.IsMatch(firstName[0].ToString(), @"^[A-Z]+$"))
                throw new ArgumentException();
            else if (lastName.Length < 2 || lastName.Length > 100 || !Regex.IsMatch(lastName.Substring(1), @"^[a-z]+$") ||
                !Regex.IsMatch(lastName[0].ToString(), @"^[A-Z]+$"))
                throw new ArgumentException();
            else if (!Regex.Match(email, "[.\\-_A-Za-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}").Success)
                throw new ArgumentException();
            else if (!Regex.IsMatch(phoneNumber, @"^\+[3][8][0]\d{9}$"))
                throw new ArgumentException();
            else if (!Regex.IsMatch(passportId, @"^\d{9}$"))
                throw new ArgumentException();
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
            Card card;
            try
            {
                card = new Card(number);
            }
            catch(ArgumentNullException)
            {
                return false;
            }
            catch(ArgumentException)
            {
                return false;
            }
            try
            {
                if (Bank.accounts.Exists(x => x.cards.Exists(x => x.Number == number)))
                    return false;
                else cards.Add(card);
            }
            catch (NullReferenceException)
            {
                cards.Add(card);
            }
            if (!cards.Contains(card))
                return false;
            else return true;
        }

        public bool RemoveCreditCard(string number)
        {
            if (string.IsNullOrEmpty(number))
                return false;
            else if (!Regex.IsMatch(number, @"^\d{4}\s\d{4}\s\d{4}\s\d{4}$"))
                return false;
            else if (cards.Exists(x => x.Number == number))
                cards.Remove(cards.Find(x => x.Number == number));
            else return false;
            if (cards.Exists(x => x.Number == number))
                return false;
            else return true;
        }

        public void GetStatement()
        {
            cards = cards.OrderBy(x => x.Balance).ToList();
        }
    }
}
