using System;
using System.Collections.Generic;
using System.Text;
using Repositories.Models;

namespace Repositories
{
    public interface IPackingTypesRepository : IRepository<PackingTypes>
    {
        public double GetCapicityOfOrder(Orders order);
    }
}
