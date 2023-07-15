using System;
using System.Collections.Generic;
using System.Text;
using Repositories.Models;

namespace Repositories
{
    public interface IDetailsOfShiftsRepository:IRepository<DetailsOfShifts>
    {
        public double GetShiftTimeToSpecificEmploAndDay(int idWeekDays, string idEmployee);
        public List<string> GetListIdEmployeeToSpecificDate(DateTime date);
    }
}
