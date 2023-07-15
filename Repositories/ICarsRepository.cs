using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
   public  interface ICarsRepository : IRepository<Cars>
    {
        public double GetSumCarsCapicitySpesificDate(DateTime date);
        public double GetSumCarsWeightCapicitySpesificDate(DateTime date);
    }
}
