using System;
using System.Collections.Generic;
using System.Text;
using Google.Maps;
using Repositories;
using Repositories.Models;

namespace AppServices
{
   public class DetailsOrder
    {
        //מרחק ממרכז ההפצה
        public double DistanceFromPackingCenter { get; set; }
        //יצוג כתובת שליחת המשלוח
        public Adress Adress { get; set; }
        //יצוג נקודה על המפה של כתובת שליחת המשלוח
        public LatLng PointOnMap { get; set; }
        //אינדקס במטריצת המרחקים
        public int IndexOfDistanceMatrix { get; set; }


    }
}
