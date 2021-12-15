using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices
{
   public interface IStatusOrderService
    {
        public List<StatusOrderViewModel> GetList();
        public void UpdateStatusOrder(int idOrder, int idStatus);
    }
}
