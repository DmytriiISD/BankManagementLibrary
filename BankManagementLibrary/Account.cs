using System;

namespace BankManagementLibrary
{
    public class Account
    {
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
    }
}