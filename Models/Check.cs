using System;
using System.Collections.Generic;

namespace WpfApplication1.Models
{
    public partial class Check
    {
        public Check()
        {
            this.Check_Paper = new List<Check_Paper>();
        }

        public int AId { get; set; }
        public int CheckId { get; set; }
        public Nullable<System.DateTime> ExpirationDate { get; set; }
        public Nullable<int> PaperNumber { get; set; }
        public virtual CurrentAccount CurrentAccount { get; set; }
        public virtual ICollection<Check_Paper> Check_Paper { get; set; }
    }
}
