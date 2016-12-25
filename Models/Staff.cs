using System;
using System.Collections.Generic;

namespace WpfApplication1.Models
{
    public partial class Staff
    {
        public int SId { get; set; }
        public Nullable<int> AbsenceCount { get; set; }
        public string SystemPassword { get; set; }
        public int BranchId_FK { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Person Person { get; set; }
    }
}
