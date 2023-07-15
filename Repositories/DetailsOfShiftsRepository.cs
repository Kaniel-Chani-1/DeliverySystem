using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Repositories.Models;
using System.Linq;

namespace Repositories
{
   public class DetailsOfShiftsRepository:IDetailsOfShiftsRepository
    {
        DeliverySystemContext context;
        IShiftsRepository shiftsRepository;
        public DetailsOfShiftsRepository(DeliverySystemContext context,
            IShiftsRepository shiftsRepository)
        {
        this.context = context;
            this.shiftsRepository = shiftsRepository;
        }

        public void Create(DetailsOfShifts objectCreate)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<DetailsOfShifts> GetAll()
        {
            throw new NotImplementedException();
        }

        public DetailsOfShifts GetById(int i)
        {
            throw new NotImplementedException();
        }

        public void Update(DetailsOfShifts objectCreate)
        {
            throw new NotImplementedException();
        }
        //הפונקציה מקבלת מזהה לקוח ומזהה יום ומחזירה את מספר השעות שעובד באותו יום
        public double GetShiftTimeToSpecificEmploAndDay(int idWeekDays,string idEmployee)
        {
            var employee = context.DetailsOfShifts.Where(e => (e.IdEmployee == idEmployee) && (e.IdWeekDay == idWeekDays)).FirstOrDefault();
            return shiftsRepository.GetShiftTime(employee.IdShift);
        }
        //פונקציה מקבלת תאריך ומחזיר רשימה של קודים העובדים שעובדים יום זה
        public List<string> GetListIdEmployeeToSpecificDate(DateTime date)
        {
            var employeesId = (context.DetailsOfShifts.Where(d => d.IdWeekDay == Convert.ToInt32(date.DayOfWeek)).Select(d=>d.IdEmployee)).ToList();
            return employeesId;
        }
    }
}
