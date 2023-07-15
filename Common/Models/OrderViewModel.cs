using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
   public class OrderViewModel
    {
        public int Id { get; set; }
        public string IdClient { get; set; }
        public DateTime OrderDate { get; set; }
        public TimeSpan OrderTime { get; set; }
        public double OrderWeight { get; set; }
        public DateTime ArrivalDate { get; set; }
        public int Status { get; set; }
    }
}
