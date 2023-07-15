using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories
{
    public class StatusOrderRepository : IStatusOrderRepository
    {
        DeliverySystemContext context;
        public StatusOrderRepository(DeliverySystemContext context)
        {
            this.context = context;
        }
        public void Create(StatusOrder objectCreate)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<StatusOrder> GetAll()
        {
            return context.StatusOrder.ToList();
        }

        public StatusOrder GetById(int i)
        {
          return  context.StatusOrder.Where(s => s.Id == i).FirstOrDefault();
        }
        public StatusOrder GetByName(string name)
        {
            return context.StatusOrder.Where(s => s.Name==name).FirstOrDefault();
        }

        public void Update(StatusOrder objectCreate)
        {
            throw new NotImplementedException();
        }
    }
}
