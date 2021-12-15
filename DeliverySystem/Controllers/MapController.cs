using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Google.Maps.Direction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppServices;
using Google.Maps;

namespace DeliverySystem.Controllers
{
    
    public class MapController : ApiBaseController
    {
        IMapService mapService;
        IAlgorithmService algorithmService;
        public MapController(IMapService mapService,
            IAlgorithmService algorithmService)
        {
            this.mapService = mapService;
            this.algorithmService = algorithmService;
        }
        //מקבל תאריך ומזהה שליח ומחזיר  מסלול של נקודות
        [HttpGet("getListOrderSent/{idEmployee}")]

        public List<LatLng> GetListOrderSent(string idEmployee)
        {
            DateTime date = DateTime.Today;
            List<LatLng> listLatLng = algorithmService.GetOrderLatlagListOfSpecificDeliveryManToSpecificDate(idEmployee, date);
            if (listLatLng.Count>0)
            {
                return listLatLng;

            }
            return null;


        }
        ///לנסיון
        //[HttpGet("getListOrderSent")]
        //public List<LatLng> GetListOrder()
        //{
        //    List<LatLng> latLngs = new List<LatLng>();
        //    LatLng latLng1 = new LatLng(31.7933274, 35.2130509);
        //    latLngs.Add(latLng1);
        //    LatLng latLng2 = new LatLng( 31.8394739,  35.2473706);
        //    latLngs.Add(latLng2);
        //    LatLng latLng3 = new LatLng(31.844789, 35.243237);
        //    latLngs.Add(latLng3);
        //    LatLng latLng4 = new LatLng(31.7932074, 35.2132448);
        //    latLngs.Add(latLng4);
        //    LatLng latLng5 = new LatLng(31.69133549999999, 35.1099605);
        //    latLngs.Add(latLng5);
        //    LatLng latLng6 = new LatLng(31.842024, 35.24404);
        //    latLngs.Add(latLng6);
        //    LatLng latLng7 = new LatLng(31.7905365, 35.2186943);
        //    latLngs.Add(latLng7);
        //    LatLng latLng8 = new LatLng(31.6902352, 35.1077525);
        //    latLngs.Add(latLng8);
        //    LatLng latLng9 = new LatLng(31.6901948, 35.1085017);
        //    latLngs.Add(latLng9);
        //    LatLng latLng10 = new LatLng(31.6902645, 35.1075245);
        //    latLngs.Add(latLng10);
        //    LatLng latLng11 = new LatLng(31.690678, 35.1097638);
        //    latLngs.Add(latLng11);
           

        //    return latLngs;
        //}
    }
}
