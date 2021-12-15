using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class DetailsOfShifts
    {
        public int Id { get; set; }
        public int IdShift { get; set; }
        public int IdWeekDay { get; set; }
        public string IdEmployee { get; set; }

        public virtual Employees IdEmployeeNavigation { get; set; }
        public virtual Shifts IdShiftNavigation { get; set; }
        public virtual WeekDays IdWeekDayNavigation { get; set; }
    }
}
