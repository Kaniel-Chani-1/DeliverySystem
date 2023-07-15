using Common.Models;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices
{
  public  class DeliveryPersonChromosome
    {
        public List<Orders> ListOrders { get; set; }
        public double Fitness { get; set; }
    }
}
 