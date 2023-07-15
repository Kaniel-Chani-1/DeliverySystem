using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
   public class DetailsInlayOrderViewModel
    {
        public int IdOrder { get; set; }
        public string IdDeliveryMan { get; set; }
        public string NameDeliveryMan { get; set; }
        public DateTime AriviallDate { get; set; }
        public AddressWithCityNameViewModel Adress { get; set; }
       
    }
}
