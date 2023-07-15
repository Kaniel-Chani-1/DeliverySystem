using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Repositories;
using Common;
using System.Net.Mail;
using System.Net;

namespace AppServices
{
   public class ClientService:IClientService
    {
        IMapper mapper;
        IClientRepository clientRepository;
        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            this.clientRepository = clientRepository;
            this.mapper = mapper;
        }

        //מחזירה רשימת כל הלקוחות
        public List<ClientViewModel> GetList()
        {
            List<ClientViewModel> clientViewModels = new List<ClientViewModel>();
            foreach (var item in clientRepository.GetAll())
            {
                clientViewModels.Add(mapper.Map<ClientViewModel>(item));

            }
            return clientViewModels;
        }
       
    }
   
}
