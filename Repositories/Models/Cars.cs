using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class Cars
    {
        public string IdEmployee { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public int YearOfManufacture { get; set; }
        public double WeightCapacity { get; set; }
        public double Capacity { get; set; }

        public virtual Employees IdEmployeeNavigation { get; set; }
    }
}
