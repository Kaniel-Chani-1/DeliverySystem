using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class Employees
    {
        public Employees()
        {
            DetailsInlay = new HashSet<DetailsInlay>();
            DetailsOfShifts = new HashSet<DetailsOfShifts>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }

        public virtual Cars Cars { get; set; }
        public virtual ICollection<DetailsInlay> DetailsInlay { get; set; }
        public virtual ICollection<DetailsOfShifts> DetailsOfShifts { get; set; }
    }
}
