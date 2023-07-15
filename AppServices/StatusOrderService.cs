using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Repositories;
using Common.Models;
using Repositories.Models;
using Repositories;


namespace AppServices
{
    public class StatusOrderService:IStatusOrderService
    {
        IMapper mapper;
        IStatusOrderRepository statusOrderRepository;
        IOrdersRepository ordersRepository;
        public StatusOrderService(IMapper mapper,
            IStatusOrderRepository statusOrderRepository,
            IOrdersRepository ordersRepository)
        {
            this.statusOrderRepository = statusOrderRepository;
            this.mapper = mapper;
            this.ordersRepository = ordersRepository;
        }
        //מחזירה רשימת כל הסטטוסים
        public List<StatusOrderViewModel> GetList()
        {
            List<StatusOrderViewModel> listStatusOrderViewModel = new List<StatusOrderViewModel>();

            foreach (var item in statusOrderRepository.GetAll())
            {
                listStatusOrderViewModel.Add(mapper.Map<StatusOrderViewModel>(item));

            }


            return listStatusOrderViewModel;
        }
        //מקבל מזהה סטטוס הזמנה חדש ומזהה משלוח 
        //ומעדכן את הסטטוס את המשלוח
        public void UpdateStatusOrder(int idOrder, int idStatus)
        {
            ordersRepository.UpdateStatusOrder(idOrder, idStatus);
        }
    }
}
