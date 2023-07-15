using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public interface  ICityRepository:IRepository<City>
    {

        public int GetCityCodeFromCityName(string cityName);
        public string ConvertIdToName(int idCity);
    }
}
