using System;
using System.Collections.Generic;
using System.Text;
using Repositories.Models;

namespace Repositories
{
    public interface IDetailsOfTheContentsOfReservationRepository : IRepository<DetailsOfTheContentsOfReservation>
    {
        public void Insert(List<PackingAmount> packingAmounts, int idOrder);
    }
}
