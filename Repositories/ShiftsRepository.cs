using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories
{
    public class ShiftsRepository : IShiftsRepository
    {
        DeliverySystemContext context;
        public ShiftsRepository(DeliverySystemContext context)
        {
            this.context = context;
        }
        public void Create(Shifts objectCreate)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Shifts> GetAll()
        {
            throw new NotImplementedException();
        }

        public Shifts GetById(int i)
        {
            throw new NotImplementedException();
        }

        public void Update(Shifts objectCreate)
        {
            throw new NotImplementedException();
        }
        //הפונקציה מקבלת קוד משמרת ומחזירה את הזמן שלה בשעות
        public double GetShiftTime(int id)
        {
            var shift = context.Shifts.Where(s => s.Id == id).FirstOrDefault();
            return Math.Abs( shift.EndTime.Subtract(shift.BeginningTime).TotalHours);
        }
    }
}
