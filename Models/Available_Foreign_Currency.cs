using System;
using System.Collections.Generic;

namespace WpfApplication1.Models
{
    public partial class Available_Foreign_Currency
    {
        public int AFCID { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> CurrencyAmount { get; set; }
        public int BranchId_FK { get; set; }
        public virtual Branch Branch { get; set; }
    }
}
