using System;
using System.Collections.Generic;

namespace WpfApplication1.Models
{
    public partial class Branch_Phone
    {
        public int BranchId_FK { get; set; }
        public string Phone { get; set; }
        public virtual Branch Branch { get; set; }
    }
}
