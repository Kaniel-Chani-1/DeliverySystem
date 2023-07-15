using System;
using System.Collections.Generic;
using System.Text;
using Repositories.Models;

namespace Repositories
{
    public interface IDetailsInlayRepository : IRepository<DetailsInlay>
    {
        public void SetDetailsInlay(string idEmployee, int idInlay, int idOrder, int serialNumber);
        public List<DetailsInlay> GetListOrdersOfSpecificDeliveryManAndToSpecificDate(string idEmployee, DateTime date);
        public void UpdateSerialNumber(int idOrder, int serialNumber);
        public List<DetailsInlay> GetListOrdersToSpecificDate(DateTime date);
    }
}
