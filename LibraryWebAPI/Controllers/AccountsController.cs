using BankManagementLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebAPI.Controllers
{
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private DbContextOptions<BankLibraryContext> options;

        public AccountsController(DbContextOptions<BankLibraryContext> options)
        {
            this.options = options;
        }

        [HttpGet]
        [Route("accounts")]
        public ObjectResult ReadAccounts()
        {
            var bank = new Bank(new DBSource(options));
            return Ok(bank);
        }

        [HttpPost]
        [Route("register_account")]
        public async Task<StatusCodeResult> RegisterAcc(string firstName, string lastName,
            string email, string phoneNumber, string passportId)
        {
            var bank = new Bank(new DBSource(options));
            phoneNumber = phoneNumber.Replace(" ", "+");
            if (bank.RegisterAccount(firstName, lastName, email, phoneNumber, passportId))
            {
                await using (BankLibraryContext db = new BankLibraryContext(options))
                {
                    db.Accounts.Add(new Entities.Account
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        PhoneNumber = phoneNumber,
                        PassportId = passportId
                    });
                    db.SaveChanges();
                }
                return StatusCode(201);
            }
            else return StatusCode(409);
        }

        [HttpGet]
        [Route("get_statement")]
        public ObjectResult GetStatement()
        {
            var bank = new Bank(new DBSource(options));
            bank.Accounts.ForEach(x => x.GetStatement());
            return new ObjectResult(bank.Accounts);
        }

        [HttpGet]
        [Route("get_summary")]
        public ObjectResult GetSummary(string temp)
        {
            if (temp == "cardnumber")
            {
                var bank = new Bank(new DBSource(options));
                bank.GetSummary(new SortByCardNumber());
                return new ObjectResult(bank);
            }
            if (temp == "passportid")
            {
                var bank = new Bank(new DBSource(options));
                bank.GetSummary(new SortByPassportID());
                return new ObjectResult(bank);
            }
            else
            {
                var bank = new Bank(new DBSource(options));
                bank.GetSummary();
                return new ObjectResult(bank);
            }
        }
    }
}
