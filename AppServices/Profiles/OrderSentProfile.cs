using AutoMapper;
using Common;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Repositories;
using Common.Models;

namespace AppServices.Profiles
{
   public class OrderSentProfile:Profile
    {
        public OrderSentProfile()
        {
            CreateMap<OrderSent, OrderSentViewModel>();
        }
       
    }
}
