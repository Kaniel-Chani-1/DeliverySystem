using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
  public  class AddressWithCityNameViewModel
    {
        public string NameCityAdress { get; set; }
        public string StreetAdress { get; set; }
        public int BuldingNumber { get; set; }
        public int EntranceNumber { get; set; }
        public int ApartmentNumber { get; set; }
    }
}
