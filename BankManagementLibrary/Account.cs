using System;
using System.Xml.Linq;

namespace BankManagementLibrary
{
    public class Account
    {
        private string firstName;
        private string lastName;
        private string email;
        private string phoneNumber;
        private string passportId;

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