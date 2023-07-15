using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Models;
using static AppServices.GeneticServices;
using Repositories;
using Common;
using AutoMapper;
using Repositories.Models;
using Google.Maps;
using Google.Maps.Direction;
using Google.Maps.Geocoding;

namespace AppServices
{
    public class InlayService : IInlayService
    {
        
        IMapper mapper;
        IOrdersRepository ordersRepository;
        IEmployeesRepository employeesRepository;
        IInlaysRepository inlaysRepository;
        IDetailsInlayRepository detailsInlayRepository;
        IGeneticServices geneticServices;
        IAlgorithmService algorithmService;
        IMapService mapService;
        public InlayService(IOrdersRepository ordersRepository,
            IEmployeesRepository employeesRepository,
            IInlaysRepository inlaysRepository,
        IDetailsInlayRepository detailsInlayRepository,
        IGeneticServices geneticServices,
        IAlgorithmService algorithmService,
        IMapService mapService,
        IMapper mapper)
        {
            this.detailsInlayRepository = detailsInlayRepository;
            this.employeesRepository = employeesRepository;
            this.inlaysRepository = inlaysRepository;
            this.ordersRepository = ordersRepository;
            this.geneticServices = geneticServices;
            this.algorithmService = algorithmService;
            this.mapService = mapService;
        }
        //הפונקציה מאתחלת את האוביקטים הסטטים
        public void Init(Orders[] arrayOrders)
        {
            GeneralData.StaticDetailsOrders = new Dictionary<int, DetailsOrder>();
            //הצבת רשימת המשלוחים שהתקבלה ברשימה הסטטית
            GeneralData.StaticArrayOrders = arrayOrders;
            //הצבת נקודה על המפה של כתובת מרכז ההפצה
            GeneralData.StaticPackingCenterPoint = mapService.ConvertAddressToPoint("תלמי יוסף 6, מעלה אדומים");
            //אתחול מילון פרטי המשלוחים
            InitDictionaryDetailsOrders();
            //אתחול נתוני מטריצת המרחקים
            InitDistanceMatrix();
           
        }
        //אתחול מילון פרטי המשלוחים
        public void InitDictionaryDetailsOrders()
        {
            //מספר משלוחים 
            int numOrders = GeneralData.StaticArrayOrders.Length;
            DetailsOrder detailsOrder;
            //בנית המילון
            //סריקת מערך המשלוחים
            for (int i = 0; i < numOrders; i++)
            {
                //אתחול אוביקט מסוג פרטי משלוח
                detailsOrder = new DetailsOrder();
                //מציאת כתובת שליחת המשלוח
                Adress adress= algorithmService.GetAdressOrder(GeneralData.StaticArrayOrders[i].Id);
                //מציאת נקודה על המפה
                LatLng latLng= mapService.ConvertAddressToPoint(mapService.AddressToString(adress));
                //מציאת מרחק ממרכז ההפצה
                double distance= mapService.DrivingDistancebyAddressAndLngLat(GeneralData.StaticPackingCenterPoint, latLng);
                //קביעת כתובת שליחת המשלוח
                detailsOrder.Adress = adress;
                //קביעת נקודה על המפה של כתובת שליחת המשלוח
                detailsOrder.PointOnMap = latLng;
                //קביעת מרחק ממרכז ההפצה
                detailsOrder.DistanceFromPackingCenter = distance;
                //קביעת אינדקס במטריצת המרחקים
                detailsOrder.IndexOfDistanceMatrix = i;
                //הוספה למילון
                GeneralData.StaticDetailsOrders.Add(GeneralData.StaticArrayOrders[i].Id, detailsOrder);
            }

        }
        //אתחול פרטי מטריצת המרחקים
        public void InitDistanceMatrix()
        {
            //מספר משלוחים 
            int numOrders= GeneralData.StaticArrayOrders.Length;
            //משתנה לשמירת מרחק
            double distance = 0;
            //אתחול המטריצה
            GeneralData.StaticDistanceMatrix = new double[numOrders,numOrders];
            for (int i = 0; i <numOrders-1; i++)
            {
                for (int j = i+1; j < numOrders; j++)
                {
                    //מציאת המרחק בין 2 הנקודות
                    distance = mapService.DrivingDistancebyAddressAndLngLat(GeneralData.StaticDetailsOrders[GeneralData.StaticArrayOrders[i].Id].PointOnMap, GeneralData.StaticDetailsOrders[GeneralData.StaticArrayOrders[j].Id].PointOnMap);
                    //קביעת המרחק במטריצת המרחקים
                    GeneralData.StaticDistanceMatrix[i, j] = distance;
                    GeneralData.StaticDistanceMatrix[j, i] = distance;

                }
            }
        }
        //פונקצית השיבוץ
        public void Inlay(DateTime date)
        {
            //מספר משלוחים לתאריך שיבוץ המסוים
            int numOfOrders = ordersRepository.GetArrayOrdersOfSpesificArivialDate(date).Count();
            //שליחה לפונקציה שמחזירה מערך של משלוחים ליום שיבוץ מסוים
            Orders[] arrOrders = new Orders[numOfOrders];
            int i = 0;
            foreach (var item in ordersRepository.GetArrayOrdersOfSpesificArivialDate(date))
            {
                arrOrders[i++] = item;

            }
            //מספר משלוחנים לתאריך שיבוץ המסוים
            int numOfEmployees = employeesRepository.GetArrayEmployeesToSpesificDate(date).Count();
            // שליחה לפונקציה שמחזירה מערך של משלוחנים ליום שיבוץ מסוים
            i = 0;
            Employees[] arrEmployees = new Employees[numOfEmployees];
            foreach (var item in employeesRepository.GetArrayEmployeesToSpesificDate(date))
            {
                arrEmployees[i++] = item;

            }
            //שליחה לפונקציה שמאתחלת את המבנים הסטטיים
            Init(arrOrders);
            //שליחה לפונקציה שמחזירה את השיבוץ האופטימלי 
            //שולחים לפונקציה מערך של משלוחים ומערך של משלוחנים לתאריך המבוקש 
            IndividualServices individual = geneticServices.Genetic(arrOrders, arrEmployees);
            //הוספת השיבוצים לטבלת השיבוצים בDB
            for (i = 0; i < numOfOrders; i++)
            {
                inlaysRepository.SetInlay(date);


            }
            //שליחה לפונקציה שמחזירה מערך של שיבוצים ששובצו ליום מסוים
            //לאחר ששובצו כנ"ל
            Inlays[] arrInlays = inlaysRepository.GetArrayInlaysTospesificArivalDate(date);
            // מיקום במערך השיבוצים
            int counterInlay = 0;
            //הוספת פרטי שיבוץ לטבלה בDB

            for (int j = 0; j < numOfEmployees; j++)
            {
                //אם שובץ לשליח משלוחים לתאריך זה
                if (individual.chromosome[arrEmployees[j].Id].ListOrders.Count>0)
                {
                    foreach (var order in individual.chromosome[arrEmployees[j].Id].ListOrders)
                    {

                        detailsInlayRepository.SetDetailsInlay(arrEmployees[j].Id,
                            arrInlays[counterInlay].Id, order.Id, 1);
                        //הגדלת מונה של מערך השיבוץ 
                        counterInlay++;
                    }
                    //שליחה לפונקציה שקובעת מספר סידורי לשליחה
                   SetSerialNumber(individual.chromosome[arrEmployees[j].Id].ListOrders);
                }
                

            }
        }
         //הפונקציה מטפלת בקביעת המספרים הסידוריים של שליחת המשלוחים 
       public void SetSerialNumber(List<Orders> listOrders)
          //  public void SetSerialNumber()
        {
           // List<Orders> listOrders = ordersRepository.GetArrayOrdersOfSpesificArivialDate(DateTime.Today.AddDays(-1)).ToList();
            //מספר המשלוחים
            int numOrders = listOrders.Count;
            int pointer = 0;
            //מערך לשמירת נתונים מסוג מבנה המכיל מזהה משלוח כתובת משלוח וכתובת המיוצגת בנקודה
            OrderAddress[] arrayOrderAddress = new OrderAddress[numOrders];
            //משתנה יחיד מסוג מבנה זה
            OrderAddress orderAddress = new OrderAddress();
            //רשימה של נקודות המייצגות כתובות של המשלוחים
            List<LatLng> listLatlng = new List<LatLng>();
            //מערך עזר של נקודות
            LatLng[] tempLatLngs = new LatLng[listOrders.Count];
            //מילוי של רשימת הכתובות ע"י סריקת רשימת המשלוחים
            foreach (var order in listOrders)
            {
                orderAddress = new OrderAddress();

                //קריאה לפונקציה שמקבלת מזהה משלוח ומחזירה את הכתובת שלו
                orderAddress.Adress = algorithmService.GetAdressOrder(order.Id);
                orderAddress.IdOrder = order.Id;
                arrayOrderAddress[pointer++] = orderAddress;
            }
            //הוספת הנקודות למערך המבנה 
            for (int i = 0; i < numOrders; i++)
            {
                //הפעלת פונקציה שמקבלת כתובת וממירה אותה לנקודה
                LatLng point = mapService.ConvertAddressToPoint(mapService.AddressToString(arrayOrderAddress[i].Adress));
                arrayOrderAddress[i].Point = point;
                listLatlng.Add(point);
            }


            //שליחה לפונקציה שמחזירה מערך  שבמקום הראשון יש את נקודת המקור
            //של המסלול ובמקום השני יש את היעד של המסלול ובשאר המקומות נקודות אמצע במסלול 
            //tempLatLngs = mapService.OriginDestinationAndWaypoints(listLatlng);להוציא מהערהההההההההה
            tempLatLngs = mapService.newOriginDestinationAndWaypoints(listLatlng);
            //העתקת נקודות אמצע המסלול לרשימה
            List<LatLng> listWayPoints = new List<LatLng>();
            for (int i = 2; i < tempLatLngs.Length; i++)
            {
                listWayPoints.Add(tempLatLngs[i]);
            }
            //שליחה לפונקציה שמקבלת נקודת מקור ונקודת יעד ורשימת נקודות ומחזירה אוביקט מסוג 
            //DirectionResponse  
            DirectionResponse direction = mapService.CreateDirection(listWayPoints, tempLatLngs[0], tempLatLngs[1]);
            var request = new GeocodingRequest();
            request.PlaceId = direction.Waypoints[0].PlaceId;
            var response = new GeocodingService().GetResponse(request);
            //מעבר על כל הנקודות המרכיבות את המסלול
            for (int i = 1; i < direction.Waypoints.Length; i++)
            {
                request.PlaceId = direction.Waypoints[i].PlaceId;
                response = new GeocodingService().GetResponse(request);

                //מעבר על רשימת המבנים ומציאת המשלוח שכתובתו שווה לנקודה זו במסלולו וקביעת מספר סידורי
                for (int j = 0; j < numOrders; j++)
                {
                    if (response.Results[0].FormattedAddress.Equals(
                        mapService.GetAddressByLongLat(arrayOrderAddress[j].Point.Latitude, arrayOrderAddress[j].Point.Longitude)))
                    {
                        detailsInlayRepository.UpdateSerialNumber(arrayOrderAddress[j].IdOrder, i);
                        break;
                    }
                }
            }
            



        }


    }

}
