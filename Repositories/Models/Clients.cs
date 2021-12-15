using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class Clients
    {
        public Clients()
        {
            Orders = new HashSet<Orders>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public int IdCityAdress { get; set; }
        public string StreetAdress { get; set; }
        public int BuldingNumber { get; set; }
        public int EntranceNumber { get; set; }
        public int ApartmentNumber { get; set; }
        public string EMail { get; set; }
        public string CreditCardNumber { get; set; }
        public string Password { get; set; }

        public virtual City IdCityAdressNavigation { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
