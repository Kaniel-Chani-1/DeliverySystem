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
   
    public class InlayController : ApiBaseController
    {
        IInlayService inlayService;
        IAlgorithmService algorithmService;
        public InlayController(IInlayService inlayService,
            IAlgorithmService algorithmService)
        {
            this.inlayService = inlayService;
            this.algorithmService = algorithmService;
        }
        //הפונקציה מקבלת תאריך ומפעילה שיבוץ על תאריך זה
       // [HttpGet("inlay/{date}")]
        [HttpGet("inlay")]
        public void SetInlay(DateTime date)
        {
            DateTime date1 = DateTime.Today.AddDays(1);
            
            inlayService.Inlay(date1);
            
        }
        //הפונקציה מקבלת תאריך ומחזירה את השיבוץ של תאריך זה 
        [HttpGet("inlayView/{date}")]
        public List<DetailsInlayOrderViewModel> GetInlay(DateTime date)
        {
            List<DetailsInlayOrderViewModel> listdDetailsInlayOrderViewModel = algorithmService.GetDetailsOrderViewModelListToSpecificDate(date);
            return listdDetailsInlayOrderViewModel;
            
        }
    }
}
