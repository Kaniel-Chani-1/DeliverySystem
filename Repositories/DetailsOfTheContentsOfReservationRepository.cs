using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
   public class DetailsOfTheContentsOfReservationRepository : IDetailsOfTheContentsOfReservationRepository
    {
        DeliverySystemContext context;
        public DetailsOfTheContentsOfReservationRepository(DeliverySystemContext context)
        {
            this.context = context;
        }
        public void Create(DetailsOfTheContentsOfReservation objectCreate)
        {
            context.DetailsOfTheContentsOfReservation.Add(objectCreate);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<DetailsOfTheContentsOfReservation> GetAll()
        {
            throw new NotImplementedException();
        }

        public DetailsOfTheContentsOfReservation GetById(int i)
        {
            throw new NotImplementedException();
        }

        public void Update(DetailsOfTheContentsOfReservation objectCreate)
        {
            throw new NotImplementedException();
        }

        //הוספת רשומות סוגי אריזות של הזמנה לטבלה
        public void Insert(List<PackingAmount> packingAmounts, int idOrder)
        {
            DetailsOfTheContentsOfReservation detailsOfTheContentsOf;
            foreach (var item in packingAmounts)
            {
                detailsOfTheContentsOf = new DetailsOfTheContentsOfReservation
                {
                    IdOrder = idOrder,
                    IdPackingTypes = item.IdPackingType,
                    Amount = item.Amount
                };
                Create(detailsOfTheContentsOf);

                

            }

        }       
               
    }
}
