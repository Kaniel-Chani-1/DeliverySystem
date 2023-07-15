using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices
{
   public interface IGeneticServices
    {
        public IndividualServices Genetic(Orders[] orders, Employees[] employees);
    }
}
