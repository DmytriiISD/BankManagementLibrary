using System.Linq;

namespace BankManagementLibrary
{
    internal class SortByPassportID : IAccount
    {
        public void Sort()
        {
            Bank.accounts = Bank.accounts.OrderBy(x => x.PassportId).ToList();
        }
    }

    internal class SortByCardNumber : IAccount
    {
        public void Sort()
        {
            Bank.accounts = Bank.accounts.OrderBy(x => x.Cards.Min(x => x.Number)).ToList();
        }
    }
}
