using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class Shifts
    {
        public Shifts()
        {
            DetailsOfShifts = new HashSet<DetailsOfShifts>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public TimeSpan BeginningTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public virtual ICollection<DetailsOfShifts> DetailsOfShifts { get; set; }
    }
}
