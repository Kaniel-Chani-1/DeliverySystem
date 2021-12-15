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
    
    public class OrderSentController : ApiBaseController
    {
        IAlgorithmService algorithmService;
        IInlayService inlayService;
        public OrderSentController(IAlgorithmService algorithmService,
            IInlayService inlayService)
        {
            this.algorithmService = algorithmService;
            this.inlayService = inlayService;
        }
        //הפונקציה מקבלת מזהה שליח ותאריךומחזירה רשימה של משלוחים שישלחו בתאריך זה ע"י שליח זה  
        [HttpGet("getListOrderSent/{idEmployee}")]
        //ordersent/getListOrderSent/217991777/25/04/2021 00:00:00
        public List<OrderSentViewModel> GetListOrderSent(string idEmployee)
        {
            DateTime date = DateTime.Today;
             return algorithmService.GetOrderViewModelListOfSpecificDeliveryManToSpecificDate(idEmployee, date);
          
           
        }
        ///לנסיון
        //[HttpGet("get")]
        //public void Get()
        //{
        //    inlayService.SetSerialNumber();
        //}

    }
}
