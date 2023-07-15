using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        DeliverySystemContext context;
        public EmployeesRepository(DeliverySystemContext context)
        {
            this.context = context;
           
        }
        public void Create(Employees objectCreate)
        {
            context.Employees.Add(objectCreate);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Employees> GetAll()
        {
            return context.Employees.ToList();
        }

        public Employees GetById(int i)
        {
            throw new NotImplementedException();
        }
        public Employees GetById(string id)
        {
            var employee = context.Employees.Where(o => o.Id == id).FirstOrDefault();
            return employee;
        }
        public void Update(Employees objectCreate)
        {
            throw new NotImplementedException();
        }
        //  שליפת רשימת משלוחנים העובדים ביום מסוים המתקבל כקלט
        public  List<Employees> GetEmployeesToSpesificDate(DateTime date)
        {
            var employeesId = (context.DetailsOfShifts.Where(d => d.IdWeekDay == Convert.ToInt32(date.DayOfWeek)).Select(d => d.IdEmployee)).ToList();
            List<Employees> employeesList = new List<Employees>();
            Employees employees1 = new Employees();

            foreach (var employeeId in employeesId)
            {
                employees1 = context.Employees.Where(e => e.Id == employeeId).FirstOrDefault();
                employeesList.Add(employees1);
            }
            //// //int dateConvertToInt = Convert.ToInt32(date.DayOfWeek);
            //var employee = (context.Employees.Include(d => d.DetailsOfShifts).ToList());
            //var emp1 = employee.Where(e=>e.DetailsOfShifts.Where(d=>d.IdWeekDay == Convert.ToInt32(date.DayOfWeek))))



            ///return employee;
            return employeesList;
        }
        //  שליפת מערך משלוחנים העובדים ביום מסוים המתקבל כקלט
        public  Employees[] GetArrayEmployeesToSpesificDate(DateTime date)
        {
            ////int dateConvertToInt = Convert.ToInt32(date.DayOfWeek);
            //var employee = (context.Employees.Include(d => d.DetailsOfShifts.Where(w => w.IdWeekDay == Convert.ToInt32(date.DayOfWeek)))).ToArray();
           return GetEmployeesToSpesificDate(date).ToArray();

            
          
        }
        //שליפת מספר המשלוחנים שעובדים בתאריך מסוים
        public  int GetNumEmployeeToSpecificDate(DateTime date)
        {
            return GetEmployeesToSpesificDate(date).Count;
        }
        //שליפת אוביקט רכב של עובד מסוים המתקבל כקלט 
        public  Cars GetCarOfSpecificEmployee(string id)
        {
           
                var car = context.Cars.Where(e => e.IdEmployee == id).FirstOrDefault();
                return car;
            
        }




        // שליפת רשימת עובדים העובדים ביום ובמשמרת מסוימת המתקבלים כקלט
        public  List<Employees> GetWhoWorkInSpecificDayAndShift(WeekDays weekDay, Shifts shift)
        {
            var employee = (context.Employees.Include(d => d.DetailsOfShifts.Where(w => w.IdWeekDay == weekDay.Id && w.IdShift == shift.Id))).ToList();
            return employee;

        }
        //שליפת נפח של רכב של עובד מסוים
        public  double GetCapicityCarEmployee(string idEmployee)
        {
            Cars car = GetCarOfSpecificEmployee(idEmployee);
            return car.Capacity;
        }

        //שליפת משקל הכלה של רכב של עובד מסוים
        public  double GetWeightCarEmployee(string idEmployee)
        {
            Cars car = GetCarOfSpecificEmployee(idEmployee);
            return car.WeightCapacity;
        }
        //הפונקציה מקבלת מז שליח וסיסמא ומחזיר TRUE אם קים בדטה בייס 
        public bool IsDeliveryManExist(string idEmployee,string password)
        {
            Employees employees = GetById(idEmployee);
            if (employees!=null&& employees.Password.Equals(password))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        //מקבל מז שליח ומחזיר שם מלא
        public string GetFullNameById(string id)
        {
            Employees employees= context.Employees.Where(e => e.Id.Equals(id)).FirstOrDefault();
            return employees.FirstName + " " + employees.LastName;
        }
    }
}
