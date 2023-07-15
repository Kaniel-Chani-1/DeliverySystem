using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class DetailsInlay
    {
        public int IdInlay { get; set; }
        public string IdEmployee { get; set; }
        public int IdOrder { get; set; }
        public int SerialNumberToSendOrder { get; set; }

        public virtual Employees IdEmployeeNavigation { get; set; }
        public virtual Inlays IdInlayNavigation { get; set; }
        public virtual Orders IdOrderNavigation { get; set; }
    }
}
