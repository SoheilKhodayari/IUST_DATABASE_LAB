using System;
using System.Collections.Generic;

namespace WpfApplication1.Models
{
    public partial class CreditCard
    {
        public string CardNumber { get; set; }
        public decimal Remainder { get; set; }
        public System.DateTime ExpirationDate { get; set; }
        public string CVV2 { get; set; }
        public string Password { get; set; }
        public string SecondaryPassword { get; set; }
        public bool CardType { get; set; }
        public Nullable<int> AId_FK { get; set; }
        public virtual SavingAccount SavingAccount { get; set; }
    }
}
