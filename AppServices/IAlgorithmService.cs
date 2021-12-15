using Common.Models;
using Google.Maps;
using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices
{
  public  interface IAlgorithmService
    {
        public Orders EndOfReceivingOfNewOrder(OrderViewModel order, List<PackingAmount> packingAmount);
        public Adress GetAdressOrder(int idOrder);
        public List<LatLng> GetOrderLatlagListOfSpecificDeliveryManToSpecificDate(string idEmployee, DateTime dateSent);
        public List<OrderSentViewModel> GetOrderViewModelListOfSpecificDeliveryManToSpecificDate(string idEmployee, DateTime dateSent);
        public List<OrderSentViewModel> GetOrderViewModelListToSpecificDate(DateTime dateSent);
        public List<DetailsInlayOrderViewModel> GetDetailsOrderViewModelListToSpecificDate(DateTime dateSent);
    }
}
