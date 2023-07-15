using System;
using System.Collections.Generic;
using System.Text;
using Repositories.Models;

namespace Repositories
{
    public interface IOrdersRepository : IRepository<Orders>
    {
        public List<Orders> GetOrdersOfSpesificArivialDate(DateTime date);
        public Orders[] GetArrayOrdersOfSpesificArivialDate(DateTime date);
        public List<Orders> SortOrdersNotSend();
        public int GetNumOfOrdersOfSpesificArivialDate(DateTime arivialDate);
        public void SetArivialDate(int id, DateTime date);
        public Orders InsertNewOrder(string idClient, DateTime orderDate,
                                      TimeSpan orderTime, double orderWeight);
        public double GetAveregeDeliveryCapicity();
        public double GetAveregeDeliveryWeight();
        public string GetIdClientByIdEmployee(int idOrder);
        public void UpdateStatusOrder(int idOrder, int idStatus);


    }
}
