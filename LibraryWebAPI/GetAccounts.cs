using BankManagementLibrary;
using Microsoft.EntityFrameworkCore;
using LibraryWebAPI.Entities;

namespace LibraryWebAPI
{
    internal class DBSource : IGetAccounts
    {
        private DbContextOptions<BankLibraryContext> options;

        public DBSource(DbContextOptions<BankLibraryContext> options)
        {
            this.options = options;
        }

        public void GetList()
        {
            Bank.accounts = new List<BankManagementLibrary.Account>();
            using (BankLibraryContext db = new BankLibraryContext(options))
            {
                var acc = db.Accounts.ToList();
                foreach (Entities.Account account in acc)
                {
                    var temp = new BankManagementLibrary.Account(account.FirstName, account.LastName, account.Email,
                                           account.PhoneNumber, account.PassportId);
                    var cards = db.Cards.Where(x => x.AccountPassportId == temp.PassportId).ToList();
                    cards.ForEach(x => temp.AddCreditCard(x.Number));
                    temp.Cards.ForEach(x => x.UpdateBalance(cards.Where(s => s.Number == x.Number).Select(y => y.Balance).First()));
                    Bank.accounts.Add(temp);
                }
            }
        }
    }
}
