using System;
using System.Collections.Generic;

namespace WpfApplication1.Models
{
    public partial class Customer
    {
        public Customer()
        {
            this.CurrentAccounts = new List<CurrentAccount>();
            this.DepositAccounts = new List<DepositAccount>();
            this.ForeignCurrencyAccounts = new List<ForeignCurrencyAccount>();
            this.Loans = new List<Loan>();
            this.SavingAccounts = new List<SavingAccount>();
        }

        public int CId { get; set; }
        public virtual ICollection<CurrentAccount> CurrentAccounts { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<DepositAccount> DepositAccounts { get; set; }
        public virtual ICollection<ForeignCurrencyAccount> ForeignCurrencyAccounts { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
        public virtual ICollection<SavingAccount> SavingAccounts { get; set; }
    }
}
