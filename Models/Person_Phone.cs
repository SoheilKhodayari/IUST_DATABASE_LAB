using System;
using System.Collections.Generic;

namespace WpfApplication1.Models
{
    public partial class Person_Phone
    {
        public int PId_FK { get; set; }
        public string Phone { get; set; }
        public virtual Person Person { get; set; }
    }
}
