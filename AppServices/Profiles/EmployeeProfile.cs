using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Repositories.Models;
using Common;
using Common.Models;

namespace AppServices.Profiles
{
  public  class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employees, EmployeeViewModel>();
        }
    }
}
