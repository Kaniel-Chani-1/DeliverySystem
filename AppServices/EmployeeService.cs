using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Repositories;
using Common.Models;

namespace AppServices
{
   public class EmployeeService:IEmployeeService
    {
        IMapper mapper;
        IEmployeesRepository employeesRepository;
        public EmployeeService(IMapper mapper,IEmployeesRepository employeesRepository)
        {
            this.employeesRepository = employeesRepository;
            this.mapper = mapper;
        }

        public List<EmployeeViewModel> GetList()
        {
            List<EmployeeViewModel>  employeeViewModels = new List<EmployeeViewModel>();
            foreach (var item in employeesRepository.GetAll())
            {
                employeeViewModels.Add(mapper.Map<EmployeeViewModel>(item));

            }
            return employeeViewModels;
        }
        //הפונקציה בודקת האם השליח קים והאם הסיסמא היא שלו
        public bool IsDeliveryManExist(string idEmployee,string password)
        {
            return employeesRepository.IsDeliveryManExist(idEmployee, password);
        }
        //הפונקציה מחזירה שם מלא של שליח לפי מ"ז
        public string GetFullNameById(string id)
        {
            return employeesRepository.GetFullNameById(id);
        }
    }
}
