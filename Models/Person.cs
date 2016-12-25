using System;
using System.Collections.Generic;

namespace WpfApplication1.Models
{
    public partial class Person
    {
        public Person()
        {
            this.Person_Phone = new List<Person_Phone>();
        }

        public int PId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Nullable<bool> Gender { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public string SSN { get; set; }
        public virtual Boss Boss { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Person_Phone> Person_Phone { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
