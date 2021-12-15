using System;
using System.Collections.Generic;
using System.Text;
using Repositories.Models;

namespace Repositories
{
    public interface IStatusOrderRepository:IRepository<StatusOrder>
    {
        public StatusOrder GetByName(string name);
    }
}
