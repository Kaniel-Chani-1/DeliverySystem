using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class DifferentShippingAddress
    {
        public int IdOrder { get; set; }
        public int IdCityAdress { get; set; }
        public string StreetAdress { get; set; }
        public int BuldingNumber { get; set; }
        public int EntranceNumber { get; set; }
        public int ApartmentNumber { get; set; }

        public virtual City IdCityAdressNavigation { get; set; }
        public virtual Orders IdOrderNavigation { get; set; }
    }
}
