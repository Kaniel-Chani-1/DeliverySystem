using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Repositories.Models;
using Common.Models;

namespace AppServices.Profiles
{
   public class StatusOrderProfile:Profile
    {
        public StatusOrderProfile()
        {
            CreateMap<StatusOrder, StatusOrderViewModel>();
        }
    }
}
