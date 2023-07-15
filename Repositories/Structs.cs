using Google.Maps;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    //מבנה של פרטי כתובת של לקוח
    public struct Adress
    {
        public int IdCityAdress;
        public string StreetAdress;
        public int BuldingNumber;
        public int EntranceNumber;
        public int ApartmentNumber;

    }
    public struct AdressWithCityName
    {
        public string NameCityAdress;
        public string StreetAdress;
        public int BuldingNumber;
        public int EntranceNumber;
        public int ApartmentNumber;

    }
    //מבנה של סוג אריזה וכמות
    public struct PackingAmount
    {
        public int IdPackingType;
        public int Amount;
    }
    //מבנה של קוד משלוח עם כתובת לשליחה ומספר סידורי לשליחה
    public struct OrderSent
    {
        public int SerialNumberToSent;
        public int IdOrder;
        public AdressWithCityName Adress;

    }
    //מבנה של קוד הזמנה כתובת לשליחה והכתובת בנקודות על המפה
    public struct OrderAddress
    {
        public int IdOrder;
        public Adress Adress;
        public LatLng Point;
        public int SerialNumber;
    }
    //מבנה של תיאור שיבוץ
    public struct DetailsInlayOrder
    {
        public int IdOrder;
        public string IdDeliveryMan;
        public string NameDeliveryMan;
        public DateTime AriviallDate;
        public AdressWithCityName Adress;
    }
}
