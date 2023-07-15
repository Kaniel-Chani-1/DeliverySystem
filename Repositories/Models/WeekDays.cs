using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class WeekDays
    {
        public WeekDays()
        {
            DetailsOfShifts = new HashSet<DetailsOfShifts>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DetailsOfShifts> DetailsOfShifts { get; set; }
    }
}
