using System;
using System.Collections.Generic;

namespace WpfApplication1.Models
{
    public partial class Check_Paper
    {
        public int AId { get; set; }
        public int CheckId { get; set; }
        public int CheckPaperId { get; set; }
        public decimal Amount { get; set; }
        public string Receiver { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        public virtual Check Check { get; set; }
    }
}
