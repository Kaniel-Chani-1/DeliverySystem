using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class City
    {
        public City()
        {
            Clients = new HashSet<Clients>();
            DifferentShippingAddress = new HashSet<DifferentShippingAddress>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Clients> Clients { get; set; }
        public virtual ICollection<DifferentShippingAddress> DifferentShippingAddress { get; set; }
    }
}
