using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories
{
    class ClientsRepository : IClientRepository
    {
        DeliverySystemContext context;
        public ClientsRepository(DeliverySystemContext context)
        {
            this.context = context;
        }
        public void Create(Clients objectCreate)
        {
            context.Clients.Add(objectCreate);
            context.SaveChanges(); 
        }
        public void Create(string id, string firstName, string lastName, string phoneNumber1,
            string phoneNumber2, int idCityAdress, string streetName, int buldingNumber
            , int enteranceNumber,int apartmentNumber,string email,string creditCardNumber )
        {
            Clients clients = new Clients();
            clients.Id = id;
            clients.FirstName = firstName;
            clients.LastName = lastName;
            clients.PhoneNumber1 = phoneNumber1;
            clients.PhoneNumber2 = phoneNumber2;
            clients.IdCityAdress = idCityAdress;
            clients.StreetAdress = streetName;
            clients.BuldingNumber = buldingNumber;
            clients.EntranceNumber = enteranceNumber;
            clients.ApartmentNumber = apartmentNumber;
            clients.EMail = email;
            clients.CreditCardNumber = creditCardNumber;
            clients.Password = phoneNumber1;
            Create(clients);
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Clients> GetAll()
        {
            return context.Clients.ToList();
        }
        public Clients GetById(int id)
        {
            return context.Clients.Where(c => c.Id.Equals(id)).FirstOrDefault();
        }

        public Clients GetById(string id)
        {
            return context.Clients.Where(c => c.Id.Equals(id)).FirstOrDefault();
        }

        public void Update(Clients objectCreate)
        {
            throw new NotImplementedException();
        }

        public void Update(string id,string firstName, string lastName, string phoneNumber1,
            string phoneNumber2, int idCityAdress, string streetName, int buldingNumber
            , int enteranceNumber, int apartmentNumber, string email, string creditCardNumber)
        {
            //var clients = GetById(id);
            var clients =
                      (from cli in context.Clients
                       where cli.Id == id
                       select cli).FirstOrDefault();
            clients.FirstName = firstName;
            clients.LastName = lastName;
            clients.PhoneNumber1 = phoneNumber1;
            clients.PhoneNumber2 = phoneNumber2;
            clients.IdCityAdress = idCityAdress;
            clients.StreetAdress = streetName;
            clients.BuldingNumber = buldingNumber;
            clients.EntranceNumber = enteranceNumber;
            clients.ApartmentNumber = apartmentNumber;
            clients.EMail = email;
            clients.CreditCardNumber = creditCardNumber;
            context.SaveChanges();
        }
        //שליפת מספר הלקוחות שקיים במערכת
        public  int GetNumClients()
        {
           
                var numClients = context.Clients.Count();
                return numClients;
            
        }
        //שליפת רשימת פרטי הכתובות של הלקוחות
        public List<Adress> GetDetailsAdress()
        {
            
                List<Adress> adressClientList = new List<Adress>();
                foreach (var item in context.Clients)
                {
                    Adress a = new Adress();
                    a.IdCityAdress = item.IdCityAdress;
                    a.StreetAdress = item.StreetAdress;
                    a.BuldingNumber = item.BuldingNumber;
                    a.EntranceNumber = (int)item.EntranceNumber;
                    a.ApartmentNumber = (int)item.ApartmentNumber;
                    adressClientList.Add(a);
                }
                return adressClientList;
            


        }
        //  פונקציה שבודקת אם שתי כתובות הן בדיוק זהות
        public  bool IsEqualAdress(Adress adress1, Adress adress2)
        {
            if (adress1.IdCityAdress == adress2.IdCityAdress &&
                adress1.StreetAdress == adress2.StreetAdress &&
                adress1.BuldingNumber == adress2.BuldingNumber &&
                adress1.ApartmentNumber == adress2.ApartmentNumber &&
                adress1.EntranceNumber == adress2.EntranceNumber)
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        //פונקציה שמקבלת לקוח ומבנה של כתובת ומחזירה אם שווים או לא
        public  bool IsClientASdressEqualAdress(Clients client, Adress adress)
        {
            Adress adress1 = new Adress();
            adress1.IdCityAdress = client.IdCityAdress;
            adress1.StreetAdress = client.StreetAdress;
            adress1.BuldingNumber = client.BuldingNumber;
            adress1.ApartmentNumber = client.ApartmentNumber;
            adress1.EntranceNumber = client.EntranceNumber;
            return IsEqualAdress(adress, adress1);


        }
        //בדיקה האם לקוח מסוים קיים במערכת לפי מ"ז
        public  bool IsNewClient(string id)
        {
            
                if (context.Clients.Where(c => c.Id.Equals(id)).FirstOrDefault() == null)
                {
                    return true;
                }
                return false;
           

        }
        //חפוש לקוח לפי מ"ז
        public Clients GetClientById(int id)
        {

            return context.Clients.Where(c => c.Id.Equals(id)).FirstOrDefault();



        }
        //הפונקציה מקבלת מזהה לקוח 
        //ומחזירה את הכתובת שלו במבנה של כתובת
        public Adress GetAdressByIdClient(string idClient)
        {
            Clients client = GetById(idClient);
            Adress adress = new Adress();
            adress.IdCityAdress = client.IdCityAdress;
            adress.ApartmentNumber = client.ApartmentNumber;
            adress.BuldingNumber = client.BuldingNumber;
            adress.EntranceNumber = client.EntranceNumber;
            adress.StreetAdress = client.StreetAdress;
            return adress;

        }
    }

}
