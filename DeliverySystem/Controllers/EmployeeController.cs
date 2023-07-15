using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Models;
using AppServices;

namespace DeliverySystem.Controllers
{
   
    public class EmployeeController : ApiBaseController
    {
        IEmployeeService employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        //מחזיר רשימת כל המשלוחנים
        [HttpGet("getEmployeeList")]
        public List<EmployeeViewModel> GetAll()
        {
            return employeeService.GetList();
        }
        //מחזיר אם המשלוחן קים או לא
        [HttpGet("IsDeliveryManExist/{idEmployee}/{password}")]
        public bool IsDeliveryManExist(string idEmployee, string password)
        {
            return employeeService.IsDeliveryManExist(idEmployee, password);

        }
        ////מחזיר אם המשלוחן קים או לא
        //[HttpGet("IsDeliveryManExist/{idEmployee}/{password}")]
        //public string IsDeliveryManExist(string idEmployee, string password)
        //{
        //    if (employeeService.IsDeliveryManExist(idEmployee, password))
        //    {
        //        return employeeService.GetFullNameById(idEmployee);
        //    }
        //    return "";

        //}
        //מקבל מזהה שליח ומחזיר את השם המלא שלו 
        [HttpGet("GetNameDeliveryMan/{idEmployee}")]
        public string GetNameDeliveryMan(string idEmployee)
        {
            return employeeService.GetFullNameById(idEmployee);
        }
    }
}
