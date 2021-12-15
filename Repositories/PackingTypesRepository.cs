using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Repositories
{
    class PackingTypesRepository : IPackingTypesRepository
    {
        DeliverySystemContext context;
        public PackingTypesRepository(DeliverySystemContext context)
        {
            this.context = context;
        }
        public void Create(PackingTypes objectCreate)
        {
            context.PackingTypes.Add(objectCreate);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<PackingTypes> GetAll()
        {
            throw new NotImplementedException();
        }

        public PackingTypes GetById(int i)
        {
            throw new NotImplementedException();
        }

        public void Update(PackingTypes objectCreate)
        {
            throw new NotImplementedException();
        }
        //חשוב  סך כללי של נפח משלוח
        public  double GetCapicityOfOrder(Orders order)
        {
            double capicity = 0;
            List<int> packingTypesCodeList = GetPackingTypeOfSpasificOrder(order);
            foreach (var item in packingTypesCodeList)
            {
                capicity += GetCapicityByIdPacking(item);

            }
            return capicity;

        }
        // הפונקציה מקבלת משלוח ומחזירה רשימה של מספר מזוהה סוגי האריזות של אותו משלוח
        public List<int> GetPackingTypeOfSpasificOrder(Orders order)
        {

            List<int> packingTypes = new List<int>();
            packingTypes = (from packing in context.DetailsOfTheContentsOfReservation
                         where packing.IdOrder == order.Id
                            select (packing.IdPackingTypes)).ToList();
            return packingTypes;
            

        }
        //פונקציה שמקבלת מספר מזוהה של אריזה ומחזירה את הנפח שלו
        public  double GetCapicityByIdPacking(int id)
        {
            using (DeliverySystemContext contex = new DeliverySystemContext())
            {
                var capicity = (contex.PackingTypes.Where(p => p.Id == id).FirstOrDefault()).EstimatedCapicity;
                return capicity;

            }

        }
    }
}
