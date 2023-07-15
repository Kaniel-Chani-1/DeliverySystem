using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class Orders
    {
        public Orders()
        {
            DetailsInlay = new HashSet<DetailsInlay>();
            DetailsOfTheContentsOfReservation = new HashSet<DetailsOfTheContentsOfReservation>();
        }

        public int Id { get; set; }
        public string IdClient { get; set; }
        public DateTime OrderDate { get; set; }
        public TimeSpan OrderTime { get; set; }
        public double OrderWeight { get; set; }
        public DateTime ArrivalDate { get; set; }
        public int Status { get; set; }

        public virtual Clients IdClientNavigation { get; set; }
        public virtual StatusOrder StatusNavigation { get; set; }
        public virtual DifferentShippingAddress DifferentShippingAddress { get; set; }
        public virtual ICollection<DetailsInlay> DetailsInlay { get; set; }
        public virtual ICollection<DetailsOfTheContentsOfReservation> DetailsOfTheContentsOfReservation { get; set; }
    }
}
