using System;
using System.Collections.Generic;
using System.Text;
using Repositories.Models;

namespace Repositories
{
   public interface IEmployeesRepository : IRepository<Employees>
    {
        public Employees[] GetArrayEmployeesToSpesificDate(DateTime date);
        public List<Employees> GetEmployeesToSpesificDate(DateTime date);
        public double GetCapicityCarEmployee(string idEmployee);
        public double GetWeightCarEmployee(string idEmployee);
        public Cars GetCarOfSpecificEmployee(string id);
        public bool IsDeliveryManExist(string idEmployee, string password);
        public string GetFullNameById(string id);
    }
}
