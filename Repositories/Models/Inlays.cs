using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class Inlays
    {
        public Inlays()
        {
            DetailsInlay = new HashSet<DetailsInlay>();
        }

        public int Id { get; set; }
        public DateTime DateInlay { get; set; }
        public DateTime ArrivalDateOrder { get; set; }

        public virtual ICollection<DetailsInlay> DetailsInlay { get; set; }
    }
}
