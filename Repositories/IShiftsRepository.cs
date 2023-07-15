using System;
using System.Collections.Generic;
using System.Text;
using Repositories.Models;

namespace Repositories
{
   public interface IShiftsRepository:IRepository<Shifts>
    {
        public double GetShiftTime(int id);
    }
}
