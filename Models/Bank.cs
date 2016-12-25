using System;
using System.Collections.Generic;

namespace WpfApplication1.Models
{
    public partial class Bank
    {
        public Bank()
        {
            this.Branches = new List<Branch>();
            this.Messages = new List<Message>();
        }

        public int BankId { get; set; }
        public string Name { get; set; }
        public Nullable<int> Interest { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
