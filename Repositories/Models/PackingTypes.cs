using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class PackingTypes
    {
        public PackingTypes()
        {
            DetailsOfTheContentsOfReservation = new HashSet<DetailsOfTheContentsOfReservation>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public double EstimatedCapicity { get; set; }

        public virtual ICollection<DetailsOfTheContentsOfReservation> DetailsOfTheContentsOfReservation { get; set; }
    }
}
