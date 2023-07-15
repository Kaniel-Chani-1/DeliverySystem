using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Repositories;
using Common.Models;

namespace AppServices.Profiles
{
   public class AdressProfile:Profile
    {
        public AdressProfile()
        {
            CreateMap<Adress, AdressViewModel>();
        }
    }
}
