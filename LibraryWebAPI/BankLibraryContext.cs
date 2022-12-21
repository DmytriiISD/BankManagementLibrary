using Microsoft.EntityFrameworkCore;
using LibraryWebAPI.Entities;
using Microsoft.EntityFrameworkCore.Design;

namespace LibraryWebAPI
{
    public class BankLibraryContext : DbContext
    {
        public BankLibraryContext (DbContextOptions<BankLibraryContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(x => new { x.PassportId, x.PhoneNumber, x.Email});
                entity.HasData(new Account { FirstName = "Ivan", LastName = "Dubovyk", Email = "van.tancheg@gmail.com",
                                             PhoneNumber = "+380965572448", PassportId = "123456789"});
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasData(new Card { Number = "0000 0000 0000 0001", AccountPassportId = "123456789"},
                               new Card { Number = "0000 0000 0000 0002", AccountPassportId = "123456789"});
                entity.HasOne(s => s.Account).WithMany(s => s.Cards);
            });
        }
    }

    public class SampleContextFactory : IDesignTimeDbContextFactory<BankLibraryContext>
    {
        public BankLibraryContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BankLibraryContext>();

            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            string connectionString = config.GetConnectionString("DefaultConnection")!;
            optionsBuilder.UseSqlServer(connectionString);
            return new BankLibraryContext(optionsBuilder.Options);
        }
    }
}
