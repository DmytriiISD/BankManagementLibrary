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
            cards.Add(card);
            if (!cards.Contains(card))
                return false;
            else return true;
        }
    }
}