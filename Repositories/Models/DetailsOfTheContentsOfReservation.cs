using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class DetailsOfTheContentsOfReservation
    {
        public int IdOrder { get; set; }
        public int IdPackingTypes { get; set; }
        public int Amount { get; set; }

        public virtual Orders IdOrderNavigation { get; set; }
        public virtual PackingTypes IdPackingTypesNavigation { get; set; }
    }
}
