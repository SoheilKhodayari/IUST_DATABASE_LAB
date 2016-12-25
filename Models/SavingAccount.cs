using System;
using System.Collections.Generic;

namespace WpfApplication1.Models
{
    public partial class SavingAccount
    {
        public SavingAccount()
        {
            this.CreditCards = new List<CreditCard>();
            this.Lotteries = new List<Lottery>();
            this.Lotteries1 = new List<Lottery>();
        }

        public int AId { get; set; }
        public decimal Remainder { get; set; }
        public Nullable<System.DateTime> OpeningDate { get; set; }
        public int CId_FK { get; set; }
        public int BranchId_FK { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual ICollection<CreditCard> CreditCards { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Lottery> Lotteries { get; set; }
        public virtual ICollection<Lottery> Lotteries1 { get; set; }
    }
}
