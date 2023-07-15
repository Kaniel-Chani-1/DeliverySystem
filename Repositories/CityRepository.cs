using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories
{
    class CityRepository : ICityRepository
    {
        DeliverySystemContext context;
        public CityRepository(DeliverySystemContext context)
        {
            this.context = context;
        }
        public void Create(City objectCreate)
        {
            context.City.Add(objectCreate);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<City> GetAll()
        {
            return context.City.ToList();
        }

        public City GetById(int i)
        {
            throw new NotImplementedException();
        }

        public void Update(City objectCreate)
        {
            throw new NotImplementedException();
        }
        //בדיקה האם עיר מסומת קימת במערכת לפי שם
        public bool IsNewClient(string name)
        {

            if (context.City.Where(c => c.Name.Equals(name)).FirstOrDefault() == null)
            {
                return true;
            }
            return false;


        }
        //פונקציה שמקבלת שם עיר ומחזירה את הקוד שלו במאגר הערים בין אם קימת ובין אם לא
        //אם לא קימת מוסיפה אותה למאגר הערים
        public int GetCityCodeFromCityName(string cityName)
        {
            City city = context.City.Where(c => c.Name.Equals(cityName)).FirstOrDefault();
            if (city!=null)
            {
                return city.Id;
            }
            else
            {
                city = new City();
                city.Name = cityName;
                Create(city);
                return city.Id;

            }
        }
        //פונקציה שמקבלת קוד עיר ומחזירה את השם של העיר במחרוזת
        public string ConvertIdToName(int idCity)
        {
            City city = context.City.Where(c => c.Id.Equals(idCity)).FirstOrDefault();
            return city.Name;
        }
    }
}
