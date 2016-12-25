using System;
using System.Collections.Generic;

namespace WpfApplication1.Models
{
    public partial class Loan
    {
        public int LoadId { get; set; }
        public decimal Amount { get; set; }
        public int Interest { get; set; }
        public int ReturningDuration { get; set; }
        public int BranchId_FK { get; set; }
        public int CId_FK { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
