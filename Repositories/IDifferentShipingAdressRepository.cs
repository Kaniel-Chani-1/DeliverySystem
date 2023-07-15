using System;
using System.Collections.Generic;
using System.Text;
using Repositories.Models;
using Repositories;

namespace Repositories
{
    public interface IDifferentShipingAdressRepository : IRepository<DifferentShippingAddress>
    {
        public void SetDiffrentShipingAdresss(Adress adress, int idOrder);
        public bool IsDifferentAddress(int idOrder);
        public Adress GetAdressByIdOrder(int idOrder);
    }
}
