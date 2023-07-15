using System;
using System.Collections.Generic;
using System.Text;



namespace Common.Models
{
   public class OrderSentViewModel
    {
        public int IdOrder { get; set; }
        public int SerialNumberToSent { get; set; }
        public AddressWithCityNameViewModel Adress { get; set; }
       
    }
}
