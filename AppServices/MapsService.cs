using Google.Maps;
using Google.Maps.Geocoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repositories;
using AutoMapper;
using Google.Maps.DistanceMatrix;
using Google.Maps.Direction;
using System.Configuration;

namespace AppServices
{
    public class MapsService : IMapService
    {
        IMapper mapper;
        ICityRepository cityRepository;
        public MapsService(IMapper mapper,
            ICityRepository cityRepository)
        {
            this.mapper = mapper;
            this.cityRepository = cityRepository;
        }
        //פונקציה שממירה כתובת לנקודה
        public LatLng ConvertAddressToPoint(string address)
        {
            GoogleSigned.AssignAllServices(new GoogleSigned("AIzaSyAu0DP7VSPA7NV0-RP0o74zt_bgKjxHQeQ"));
            var request = new GeocodingRequest();
            request.Address = address;
            Result result=new Result();
            LatLng latLng = new LatLng();

            var response = new GeocodingService().GetResponse(request);
            if (response.Status == ServiceResponseStatus.Ok && response.Results.Count() > 0)
            {
                result = response.Results.First();
                latLng = new LatLng(Convert.ToDecimal(result.Geometry.Location.Latitude),
               Convert.ToDecimal(result.Geometry.Location.Longitude));

            }
            else
            {
                Console.WriteLine("Unable to geocode. Status={0} and ErrorMessage={1}",
                    response.Status, response.ErrorMessage);
            }
            return latLng;
        }
        //פונקציה שמקבלת רשימת כתובות וממירה אותם לרשימת נקודות
        public List<LatLng> ConvertAdressListToLatlng(List<Adress> listAdresses)
        {
            List<LatLng> newListAddress = new List<LatLng>();
            string addressToString;
            foreach (var address in listAdresses)
            {
                addressToString = AddressToString(address);
                newListAddress.Add(ConvertAddressToPoint(addressToString));
                   
            }
            return newListAddress;
        }
        //פונקציה שממירה מבנה כתובת למחרוזת כתובת
        public string AddressToString(Adress adress)
        {
            string stringAddress = adress.StreetAdress + " " + adress.BuldingNumber + " " 
                + cityRepository.ConvertIdToName(adress.IdCityAdress);
            return stringAddress;

        }
        //פונקצית עזר צרייייייייייייי למחוק
        public LatLng[] newOriginDestinationAndWaypoints(List<LatLng> listPointsAddress)
        {

            //מספר נקודות ברשימה הנתונה
            int numOfPoints = listPointsAddress.Count;
            //הגדרת המערך
            LatLng[] result = new LatLng[numOfPoints + 1];
            //המרת כתובת מרכז האריזה לנקודה
            LatLng latLngPackingCenter = ConvertAddressToPoint("תלמי יוסף 6, מעלה אדומים");
            //משתנה לשמירת נקודת היעד
            LatLng destination = new LatLng();
            //משתנה לשמירת מקסימום מרחק בין מרכז האריזה ליעד
            double maxDistance = 0;
            double tempDistance = 0;
            //מעבר על כל הנקודות למציאת הנקודה הרחוקה ביותר מנקודת בית האריזה 
            //ע"מ למצוא ולקבוע אותה כנקודת יעד
            foreach (var address in listPointsAddress)
            {
                //אם המרחק של נקודה זו יותר גדול אז תקבע את נקודה זו לנקודת היעד
                tempDistance = DrivingDistancebyAddressAndLngLat(latLngPackingCenter, address);
                if (tempDistance > maxDistance)
                {
                    maxDistance = tempDistance;
                    destination = address;
                }
            }
            //הכנסת נקודות מקור ויעד למערך
            result[0] = latLngPackingCenter;
            result[1] = destination;
            //הסרת נקודת היעד והמקור מרשימת הנקודות
            listPointsAddress.Remove(destination);
            listPointsAddress.Remove(latLngPackingCenter);
            //סריקת רשימת הנקודות והעתקת הנקודות האמצע 
            //למערך
            int i = 2;
            foreach (var address in listPointsAddress)
            {
                result[i] = address;
                i++;
            }
            return result;

        }

        //הפונקציה מחזירה את המרחק בין 2 כתובות המיוצגות כנקודה
        public  double DrivingDistancebyAddressAndLngLat(LatLng originAddress, LatLng DestinationAddress)

        {
            GoogleSigned.AssignAllServices(new GoogleSigned("AIzaSyAu0DP7VSPA7NV0-RP0o74zt_bgKjxHQeQ"));
            //משתנה המכיל את המרחק בין 2 הנקודות
            double travelDistance = -1;
            //יצירת אובייקט מסוג בקשת מרחק של דרכים
            DistanceMatrixRequest request = new DistanceMatrixRequest();
            //קביעת היעד של הבקשה
            request.AddDestination(DestinationAddress);
            //הוספת מוצא
            request.AddOrigin(originAddress);
            //קביעת אמצעי הנסיעה לרכב פרטי
            request.Mode = TravelMode.driving;
            //יצירת אוביקט מסוג תגובת מרחק דרכים
            DistanceMatrixResponse response = new DistanceMatrixService().GetResponse(request);
            //בודק האם הבקשה טופלה כראוי
            if (response.Status == ServiceResponseStatus.Ok)
            {
                travelDistance = Convert.ToDouble(response.Rows[0].Elements[0].distance.Value);
            }
            else
            {
                Console.WriteLine("Unable to get direction. Status={0} and ErrorMessage={1}",
                    response.Status, response.ErrorMessage);
            }
            return travelDistance;
        }
        
        //הפונקציה מקבלת רשימת נקודות ומחזירה את מערך שבמקום הראשון יש את נקודת המקור
        //של המסלול ובמקום השני יש את היעד של המסלול ובשאר המקומות נקודות אמצע במסלול
        public LatLng[] OriginDestinationAndWaypoints(List<LatLng> listPointsAddress)
        {
            //העתקת הרשימה הנתונה
            List<LatLng> tempListPointsAddress = new List<LatLng>();
            foreach (var address in listPointsAddress)
            {
                tempListPointsAddress.Add(address);
            }
            //מספר נקודות ברשימה הנתונה
            int numOfPoints = listPointsAddress.Count;
            //הגדרת המערך
            LatLng[] result = new LatLng[numOfPoints+1];
            // כתובת מרכז האריזה-נקודה
            LatLng latLngPackingCenter = GeneralData.StaticPackingCenterPoint;
            //משתנה לשמירת נקודת היעד
            LatLng destination = new LatLng();
            //משתנה לשמירת מקסימום מרחק בין מרכז האריזה ליעד
            double maxDistance = 0;
            double tempDistance = 0;
            //מעבר על כל הנקודות למציאת הנקודה הרחוקה ביותר מנקודת בית האריזה 
            //ע"מ למצוא ולקבוע אותה כנקודת יעד
            //מעבר על המילון הסטטי
            foreach (KeyValuePair<int, DetailsOrder> detailsOrder in  GeneralData.StaticDetailsOrders)
            {
                //אם המשלוח הזה קים ברשימת נקודות על המפה האלו
                if (listPointsAddress.Contains(detailsOrder.Value.PointOnMap))
                {
                    //אם כן
                    //אם המרחק של כתובת משלוח זה יותר גדולה מהמרחק של הנקודות הקודמות
                    if (detailsOrder.Value.DistanceFromPackingCenter > maxDistance)
                    {
                        maxDistance = detailsOrder.Value.DistanceFromPackingCenter;
                        destination = detailsOrder.Value.PointOnMap;
                    }
                }
            }
            //הכנסת נקודות מקור ויעד למערך
            result[0] = latLngPackingCenter;
            result[1] = destination;
            //הסרת נקודת היעד  מרשימת הנקודות
            tempListPointsAddress.Remove(destination);
            //סריקת רשימת הנקודות והעתקת הנקודות האמצע 
            //למערך
            int i = 2;
            foreach (var address in tempListPointsAddress)
            {
                result[i] = address;
                i++;
            }
            return result;

        }
        //הפונקציה מקבלת רשימת כתובות ומחזירה את הגודל 
        //של תיחום נקודות אלו
        public double AreaSizePoints(List<LatLng> listPointsAddress)
        {
            
            double minLatitude = listPointsAddress.Last().Latitude;
            double maxLatitude = 0;
            double minLongitude = listPointsAddress.Last().Longitude;
            double maxLongitude = 0;
            //חיפוש אחר הLatitude 
            //הכי קטן והכי גדול
            //Longitude חיפוש אחר ה
            //הכי גדול והכי קטן

            foreach (var address in listPointsAddress)
            {
                if (address.Latitude >= maxLatitude)
                {
                    maxLatitude = address.Latitude;
                }
                if (address.Latitude<= minLatitude)
                {
                    minLatitude = address.Latitude;
                }
                if (address.Longitude >= maxLongitude)
                {
                    maxLongitude = address.Longitude;
                }
                if (address.Longitude <= minLongitude)
                {
                    minLongitude = address.Longitude;
                }
            }
            //חישוב מרחק בנקודות בין הLOG הכי גדול לבין הכי קטן
            double LongitudeDistance = maxLongitude-minLongitude;
            //חישוב המרחק בנקודות בין הLAT הכי גדול להכי קטן
            double LatitudeDistance = maxLatitude-minLatitude;
            //החזרת כפל המרחק למציאת גודל התיחום
            return LongitudeDistance * LatitudeDistance;

        }
        //הפונקציה מקבלת נקודת יעד ומקור ורשימת נקודות באמצע 
        //ומחזירה אוביקט מסוג מסלול שנוצר מנקודות אלו
        public  DirectionResponse CreateDirection(List<LatLng> latLngs, LatLng origin, LatLng destination)
        {
            GoogleSigned.AssignAllServices(new GoogleSigned("AIzaSyAu0DP7VSPA7NV0-RP0o74zt_bgKjxHQeQ"));
            DirectionRequest request = new DirectionRequest();
            request.Origin = origin;
            request.Destination = destination;
            request.Optimize = true;
            request.Mode = TravelMode.driving;
            foreach (var point in latLngs)
            {
                request.AddWaypoint(point);
            }

            DirectionResponse response = new DirectionService().GetResponse(request);
            if (response.Status == ServiceResponseStatus.Ok)
            {
                return response;
            }
            else
            {
                Console.WriteLine("Unable to get direction. Status={0} and ErrorMessage={1}",
                    response.Status, response.ErrorMessage);
            }

            return null;
        }
        // DirectionResponse פונקציה שמקבלת אוביקט מסוג 
        //ומחזירה את הזמן של המסלול בדקות
        public int GetTimeDirection(DirectionResponse direction)
        {
            int time = 0;
            for (int i = 0; i < direction.Routes[0].Legs.Length; i++)
            {
                time += Convert.ToInt32(direction.Routes[0].Legs[i].Duration.Value);

            }
            return Convert.ToInt32(time/60);
        }
        // DirectionResponse פונקציה שמקבלת אוביקט מסוג 
        //ומחזירה את האורך של המסלול במטרים
        public int GetDistanceDirection(DirectionResponse direction)
        {
            int distance = 0;
            for (int i = 0; i < direction.Routes[0].Legs.Length; i++)
            {
                distance+= Convert.ToInt32(direction.Routes[0].Legs[i].Distance.Value);

            }
            return distance;
        }
        //פונקציה שמקבלת נקודה על המפה ומחזירה אותה בפורמט
        //של מחרוזת
        public string GetAddressByLongLat(double lat, double lng)
        {
            string formatAddress=" ";
            GoogleSigned.AssignAllServices(new GoogleSigned("AIzaSyAu0DP7VSPA7NV0-RP0o74zt_bgKjxHQeQ"));
            var request = new GeocodingRequest()
            {
                Address
            = new LatLng(lat, lng) { }
            };
            Result result = new Result();
            LatLng latLng = new LatLng();

            var response = new GeocodingService().GetResponse(request);
            if (response.Status == ServiceResponseStatus.Ok && response.Results.Count() > 0)
            {
                result = response.Results.First();
                formatAddress = result.FormattedAddress.ToString();


            }
            else
            {
                Console.WriteLine("Unable to geocode. Status={0} and ErrorMessage={1}",
                    response.Status, response.ErrorMessage);
            }
            return formatAddress;

        }

    }
}
