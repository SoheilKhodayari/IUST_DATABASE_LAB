using System;
using System.Collections.Generic;

namespace WpfApplication1.Models
{
    public partial class Message
    {
        public int MsgId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int BId_FK { get; set; }
        public int BankId_FK { get; set; }
        public virtual Bank Bank { get; set; }
        public virtual Boss Boss { get; set; }
    }
}
