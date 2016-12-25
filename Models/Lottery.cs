using System;
using System.Collections.Generic;

namespace WpfApplication1.Models
{
    public partial class Lottery
    {
        public Lottery()
        {
            this.SavingAccounts = new List<SavingAccount>();
            this.SavingAccounts1 = new List<SavingAccount>();
        }

        public int LotteryId { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Description { get; set; }
        public int BranchId_FK { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual ICollection<SavingAccount> SavingAccounts { get; set; }
        public virtual ICollection<SavingAccount> SavingAccounts1 { get; set; }
    }
}
