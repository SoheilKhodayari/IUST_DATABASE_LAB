using System;
using System.Collections.Generic;

namespace WpfApplication1.Models
{
    public partial class Branch
    {
        public Branch()
        {
            this.Available_Foreign_Currency = new List<Available_Foreign_Currency>();
            this.Bosses = new List<Boss>();
            this.Branch_Phone = new List<Branch_Phone>();
            this.CurrentAccounts = new List<CurrentAccount>();
            this.DepositAccounts = new List<DepositAccount>();
            this.ForeignCurrencyAccounts = new List<ForeignCurrencyAccount>();
            this.Loans = new List<Loan>();
            this.Lotteries = new List<Lottery>();
            this.SavingAccounts = new List<SavingAccount>();
            this.Staffs = new List<Staff>();
        }

        public int BranchId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public bool IsCentral { get; set; }
        public int BankId_FK { get; set; }
        public virtual ICollection<Available_Foreign_Currency> Available_Foreign_Currency { get; set; }
        public virtual Bank Bank { get; set; }
        public virtual ICollection<Boss> Bosses { get; set; }
        public virtual ICollection<Branch_Phone> Branch_Phone { get; set; }
        public virtual ICollection<CurrentAccount> CurrentAccounts { get; set; }
        public virtual ICollection<DepositAccount> DepositAccounts { get; set; }
        public virtual ICollection<ForeignCurrencyAccount> ForeignCurrencyAccounts { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
        public virtual ICollection<Lottery> Lotteries { get; set; }
        public virtual ICollection<SavingAccount> SavingAccounts { get; set; }
        public virtual ICollection<Staff> Staffs { get; set; }
    }
}
