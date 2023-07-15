using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Repositories.Models;
using Common;

namespace AppServices.Profiles
{
   public class ClientProfile:Profile
    {
        public ClientProfile()
        {
            CreateMap<Clients, ClientViewModel>();

        }
    }
}

