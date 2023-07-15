using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppServices;
using Common.Models;

namespace DeliverySystem.Controllers
{
    public class StatusOrderController : ApiBaseController
    {
        IStatusOrderService statusOrderService;
        public StatusOrderController(IStatusOrderService statusOrderService)
        {
            this.statusOrderService = statusOrderService;
        }
        //מחזיר רשימת סטטוס הזמנה 
        [HttpGet("getStatusOrderList")]
        public List<StatusOrderViewModel> GetAll()
        {
            return statusOrderService.GetList();
        }
        //מקבל שם סטטוס הזמנה חדש ומזהה הזמנה ומעדכן
        [HttpPost("updateStatusOrder")]
        public void UpdateStatusOrder(UpdateStatuseService updateStatuseService)
        {
            statusOrderService.UpdateStatusOrder(Convert.ToInt32(updateStatuseService.IdOrder), Convert.ToInt32(updateStatuseService.IdStatus));
        }

    }

}
