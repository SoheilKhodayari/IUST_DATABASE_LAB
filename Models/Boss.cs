using System;
using System.Collections.Generic;

namespace WpfApplication1.Models
{
    public partial class Boss
    {
        public Boss()
        {
            this.Messages = new List<Message>();
        }

        public int BId { get; set; }
        public Nullable<int> AbsenceCount { get; set; }
        public string SystemPassword { get; set; }
        public int BranchId_FK { get; set; }
        public virtual Person Person { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
