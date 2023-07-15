using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices
{
    public interface IIndividualServices
    {
        public void SetIndividualFitness(IndividualServices individual);
        public double SetFitnessDeliveryPersonChromosome(DeliveryPersonChromosome deliveryPersonChromosome, string idDeliveryPerson);
        
    }
}
