using AutoMapper;
using Common.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Profiles
{
  public  class AddressWithCityNameProfile:Profile
    {
        public AddressWithCityNameProfile()
        {
            CreateMap<AdressWithCityName, AddressWithCityNameViewModel>();
        }
    }
}
