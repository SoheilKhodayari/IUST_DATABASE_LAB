using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WpfApplication1.Models.Mapping;

namespace WpfApplication1.Models
{
    public partial class BankDBContext : DbContext
    {
        static BankDBContext()
        {
            Database.SetInitializer<BankDBContext>(null);
        }

        public BankDBContext()
            : base("Name=BankDBContext")
        {
        }

        public DbSet<Available_Foreign_Currency> Available_Foreign_Currency { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Boss> Bosses { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Branch_Phone> Branch_Phone { get; set; }
        public DbSet<Check> Checks { get; set; }
        public DbSet<Check_Paper> Check_Paper { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<CurrentAccount> CurrentAccounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DepositAccount> DepositAccounts { get; set; }
        public DbSet<ForeignCurrencyAccount> ForeignCurrencyAccounts { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Lottery> Lotteries { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Person_Phone> Person_Phone { get; set; }
        public DbSet<SavingAccount> SavingAccounts { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Available_Foreign_CurrencyMap());
            modelBuilder.Configurations.Add(new BankMap());
            modelBuilder.Configurations.Add(new BossMap());
            modelBuilder.Configurations.Add(new BranchMap());
            modelBuilder.Configurations.Add(new Branch_PhoneMap());
            modelBuilder.Configurations.Add(new CheckMap());
            modelBuilder.Configurations.Add(new Check_PaperMap());
            modelBuilder.Configurations.Add(new CreditCardMap());
            modelBuilder.Configurations.Add(new CurrentAccountMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new DepositAccountMap());
            modelBuilder.Configurations.Add(new ForeignCurrencyAccountMap());
            modelBuilder.Configurations.Add(new LoanMap());
            modelBuilder.Configurations.Add(new LotteryMap());
            modelBuilder.Configurations.Add(new MessageMap());
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new Person_PhoneMap());
            modelBuilder.Configurations.Add(new SavingAccountMap());
            modelBuilder.Configurations.Add(new StaffMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
        }
    }
}
