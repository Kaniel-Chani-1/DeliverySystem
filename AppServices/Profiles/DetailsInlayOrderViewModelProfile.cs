using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Repositories;
using Common.Models;

namespace AppServices.Profiles
{
   public class DetailsInlayOrderViewModelProfile:Profile
    {
        public DetailsInlayOrderViewModelProfile()
        {
            CreateMap<DetailsInlayOrder, DetailsInlayOrderViewModel>();
        }
    }
}
