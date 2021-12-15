using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices
{
    //רמת עדיפויות של אילוצי השיבוץ
    //הערכים הם מציינים את רמת ההכרחיות ככל שהערך יותר גדול ההכרחיות גדולה יותר
    public enum Constraint
    {
        //פיזור אזורים
        ScatteringAreas = 18,
        //זמן עבודה משוער
        WorkTime = 9,
        //משקל שהרכב יכול להכיל
        WeightCarryCar = 12,
        //נפח שהרכב יכול להכיל
        CapicityCarryCar = 13
    }
}
