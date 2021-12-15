using System;
using System.Collections.Generic;
using System.Text;
using Repositories.Models;

namespace Repositories
{
    public interface IClientRepository : IRepository<Clients>
    {
       

        public  bool IsClientASdressEqualAdress(Clients client, Adress adress);
        public bool IsNewClient(string id);
        public void Create(string id, string firstName, string lastName, string phoneNumber1,
           string phoneNumber2, int idCityAdress, string streetName, int buldingNumber
           , int enteranceNumber, int apartmentNumber, string email, string creditCardNumber);
        public void Update(string id, string firstName, string lastName, string phoneNumber1,
            string phoneNumber2, int idCityAdress, string streetName, int buldingNumber
            , int enteranceNumber, int apartmentNumber, string email, string creditCardNumber);
        public Adress GetAdressByIdClient(string idClient);
        public Clients GetById(string id);

    }
}
