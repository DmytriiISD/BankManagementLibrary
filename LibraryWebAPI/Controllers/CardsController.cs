using BankManagementLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebAPI.Controllers
{
    [ApiController]
    public class CardsController : ControllerBase
    {
        private DbContextOptions<BankLibraryContext> options;

        public CardsController(DbContextOptions<BankLibraryContext> options)
        {
            this.options = options;
        }

        [HttpPost]
        [Route("add_card")]
        public async Task<StatusCodeResult> AddCard(string number, string phNumber)
        {
            var bank = new Bank(new DBSource(options));
            bool check;
            phNumber = phNumber.Replace(" ", "+");

            try
            {
                check = bank.ReturnAccount(phNumber).AddCreditCard(number);
            }
            catch (ArgumentException)
            {
                return StatusCode(400);
            }

            if (check)
            {
                await using (BankLibraryContext db = new BankLibraryContext(options))
                {
                    db.Cards.Add(new Entities.Card
                    {
                        Number = number,
                        AccountPassportId = bank.ReturnAccount(phNumber).PassportId
                    });
                    db.SaveChanges();
                }
                return StatusCode(201);
            }
            else return StatusCode(403);
        }

        [HttpDelete]
        [Route("delete_card")]
        public async Task<StatusCodeResult> DeleteCard(string number)
        {
            var bank = new Bank(new DBSource(options));
            bool check = false;
            var acc = bank.Accounts.Find(x => x.Cards.Exists(x => x.Number == number));

            if (acc != null)
                check = acc.RemoveCreditCard(number);

            if (check)
            {
                await using (BankLibraryContext db = new BankLibraryContext(options))
                {
                    var card = db.Cards.Find(number);
                    if (card != null)
                        db.Cards.Remove(card);
                    db.SaveChanges();
                }
                return StatusCode(200);
            }
            else return StatusCode(404);
        }

        [HttpPatch]
        [Route("update_balance")]
        public async Task<StatusCodeResult> UpdateBalance(string number, decimal money)
        {
            var bank = new Bank(new DBSource(options));
            var acc = bank.Accounts.Find(x => x.Cards.Exists(x => x.Number == number));
            List<Card> cards;
            Card card;

            if (acc != null)
            {
                cards = acc.Cards;
                card = cards.Where(x => x.Number == number).First();

                try
                {
                    card.UpdateBalance(money);
                }
                catch (ArithmeticException)
                {
                    return StatusCode(403);
                }

                await using (BankLibraryContext db = new BankLibraryContext(options))
                {
                    var temp = db.Cards.Where(x => x.Number == number).FirstOrDefault();
                    if (temp != null)
                        temp.Balance = card.Balance;
                    db.SaveChanges();
                }
                return StatusCode(200);
            }
            else return StatusCode(403);
        }
    }
}
