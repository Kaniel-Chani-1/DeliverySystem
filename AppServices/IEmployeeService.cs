using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices
{
   public interface IEmployeeService
    {
        public List<EmployeeViewModel> GetList();
        public bool IsDeliveryManExist(string idEmployee, string password);
        public string GetFullNameById(string id);
    }
}
