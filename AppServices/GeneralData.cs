using System;
using System.Collections.Generic;
using System.Text;
using Google.Maps;
using Repositories.Models;

namespace AppServices
{
 public  static class GeneralData
    {
        //מערך של המשלוחים לשיבוץ נוכחי
        public static Orders[] StaticArrayOrders;
        //מטריצה המיצגת מרחקים בין כתובות
        public static double[,] StaticDistanceMatrix ;
        //מילון השומר למפתחות - מזהי משלוחים נתונים שונים
        public static Dictionary<int,DetailsOrder > StaticDetailsOrders;
        //יצוג כתובת מרכז ההפצה בנקודה על המפה
        public static LatLng StaticPackingCenterPoint;
     

    }
}
