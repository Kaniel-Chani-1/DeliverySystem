using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AppServices
{
  public  class IndividualComparer : IComparer
    {

        int IComparer.Compare(object x, object y)
        {
            IndividualServices c1 = (IndividualServices)x;
            IndividualServices c2 = (IndividualServices)y;
            if (c1.fitness > c2.fitness)
                return 1;
            if (c1.fitness < c2.fitness)
                return -1;
            else return 0;

        }

    }
    
    
}
