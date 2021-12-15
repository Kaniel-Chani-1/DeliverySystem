using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories
{
    class CarsRepository : ICarsRepository
    {
        DeliverySystemContext context;
        IEmployeesRepository employeesRepository;
        public CarsRepository(DeliverySystemContext context,
            IEmployeesRepository employeesRepository)
        {
            this.context = context;
            this.employeesRepository = employeesRepository;
        }
        public void Create(Cars objectCreate)
        {
            context.Cars.Add(objectCreate);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Cars> GetAll()
        {
            throw new NotImplementedException();
        }

        public Cars GetById(int i)
        {
            throw new NotImplementedException();
        }

        public void Update(Cars objectCreate)
        {
            throw new NotImplementedException();
        }
        //Calculates the average of יכולת קיבול של נפח של רכבים ליום מסוים
        public double GetSumCarsCapicitySpesificDate(DateTime date)
        {
            var sumCarsCapicity = (from employee in employeesRepository.GetEmployeesToSpesificDate(date)
                                   select employeesRepository.GetCarOfSpecificEmployee(employee.Id).Capacity).Sum();
            return sumCarsCapicity;

        }
        //Calculates the average of יכולת קיבול של משקל של רכבים ליום מסוים
        public double GetSumCarsWeightCapicitySpesificDate(DateTime date)
        {
           

            var sumCarsWeightCapicity = (from employee in employeesRepository.GetEmployeesToSpesificDate(date)
                                         select employeesRepository.GetCarOfSpecificEmployee(employee.Id).WeightCapacity).Sum();
            return sumCarsWeightCapicity;

        }
    }
}
