using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories
{
    class DifferentShipingAdressRepository : IDifferentShipingAdressRepository
    {
        DeliverySystemContext context;
        public DifferentShipingAdressRepository(DeliverySystemContext context)
        {
            this.context = context;
        }
        public void Create(DifferentShippingAddress objectCreate)
        {
            context.DifferentShippingAddress.Add(objectCreate);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<DifferentShippingAddress> GetAll()
        {
            throw new NotImplementedException();
        }

        public DifferentShippingAddress GetById(int idOrder)
        {
            return context.DifferentShippingAddress.Where(d => d.IdOrder.Equals(idOrder)).FirstOrDefault();
        }

        public void Update(DifferentShippingAddress objectCreate)
        {
            throw new NotImplementedException();
        }
        //הוספת רשומה חדשה של כתובת שונה למשלוח - מקבלים מבנה של כתובת  ומספר הזמנה
        public void SetDiffrentShipingAdresss(Adress adress, int idOrder)
        {
           
            DifferentShippingAddress differentShippingAddress = new DifferentShippingAddress();
            differentShippingAddress.IdCityAdress = adress.IdCityAdress;
            differentShippingAddress.StreetAdress = adress.StreetAdress;
            differentShippingAddress.BuldingNumber = adress.BuldingNumber;
            differentShippingAddress.ApartmentNumber = adress.ApartmentNumber;
            differentShippingAddress.EntranceNumber = adress.EntranceNumber;
            differentShippingAddress.IdOrder = idOrder;
            Create(differentShippingAddress);


            
        }
        //שליפת מספר הכתובות שקימות בטבלה-מספר הרשומות
        public  int GetNumAdresses()
        {
            
                var numAdresses = context.DifferentShippingAddress.Count();
                return numAdresses;
            
        }
        //שליפת רשימת כתובות במבנה כתובת
        public  List<Adress> GetDetailsAdress()
        {
           
                List<Adress> adressClientList = new List<Adress>();
                foreach (var item in context.DifferentShippingAddress)
                {
                    Adress a = new Adress();
                    a.IdCityAdress = item.IdCityAdress;
                    a.StreetAdress = item.StreetAdress;
                    a.BuldingNumber = item.BuldingNumber;
                    a.EntranceNumber = item.EntranceNumber;
                    a.ApartmentNumber = item.ApartmentNumber;
                    adressClientList.Add(a);
                }
                return adressClientList;
            
        }
        //הפונקציה מקבלת מזהה משלוח
        //ומחזירה האם למשלוח הזה יש כתובת שונה
        public bool IsDifferentAddress(int idOrder)
        {
            DifferentShippingAddress differentShippingAddress = GetById(idOrder);
            if (differentShippingAddress!=null)
            {
                return true;
            }
            return false;

        }
        //הפונקציה מקבלת מזהה משלוח 
        //ומחזירה את הכתובת שלו במבנה של כתובת
        public Adress GetAdressByIdOrder(int idOrder)
        {
            DifferentShippingAddress differentShippingAddress = GetById(idOrder);
            Adress adress = new Adress();
            adress.IdCityAdress = differentShippingAddress.IdCityAdress;
            adress.ApartmentNumber = differentShippingAddress.ApartmentNumber;
            adress.BuldingNumber = differentShippingAddress.BuldingNumber;
            adress.EntranceNumber = differentShippingAddress.EntranceNumber;
            adress.StreetAdress = differentShippingAddress.StreetAdress;
            return adress;

        }


    }
}
