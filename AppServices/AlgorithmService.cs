using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Models;
using Repositories;
using Repositories.Models;
using AutoMapper;
using Google.Maps;

namespace AppServices
{
   public class AlgorithmService:IAlgorithmService
    {
        const string addressCentral = "תלמי יוסף 6, מעלה אדומים";
        IMapper mapper;
        ICarsRepository carsRepository;
        IOrdersRepository ordersRepository;
        IDetailsOfTheContentsOfReservationRepository detailsOfTheContentsOfReservationRepository;
        IDetailsInlayRepository detailsInlayRepository;
        IDifferentShipingAdressRepository differentShipingAdressRepository;
        IClientRepository clientRepository;
        IEmployeesRepository employeesRepository;
        ICityRepository cityRepository;
        IMapService mapService;
        public AlgorithmService(ICarsRepository carsRepository,
            IOrdersRepository ordersRepository,
            IDetailsOfTheContentsOfReservationRepository detailsOfTheContentsOfReservationRepository,
            IDetailsInlayRepository detailsInlayRepository,
            IDifferentShipingAdressRepository differentShipingAdressRepository,
            IClientRepository clientRepository,
            IMapper mapper,
            IMapService mapService,
            ICityRepository cityRepository,
            IEmployeesRepository employeesRepository)
        {
            this.mapper = mapper;
            this.detailsOfTheContentsOfReservationRepository = detailsOfTheContentsOfReservationRepository;
            this.ordersRepository = ordersRepository;
            this.carsRepository = carsRepository;
            this.detailsInlayRepository = detailsInlayRepository;
            this.differentShipingAdressRepository = differentShipingAdressRepository;
            this.clientRepository = clientRepository;
            this.mapService = mapService;
            this.cityRepository = cityRepository;
            this.employeesRepository = employeesRepository;
        }

       
        //פונקציות מרכזיות
        //Brings a quantity of deliveries to a specific date
        public int GetquantityOfDeliveries(DateTime date)
        {
            var numDeliveriesByCapicity = carsRepository.GetSumCarsCapicitySpesificDate(date) - (0.05 * 4) / ordersRepository.GetAveregeDeliveryCapicity();//לשאול???
            var numDeliveriesByWeight = carsRepository.GetSumCarsWeightCapicitySpesificDate(date) / ordersRepository.GetAveregeDeliveryWeight();
            var numDeliveriesAverage = (numDeliveriesByCapicity + numDeliveriesByWeight) / 2;//לשאול????
            return (int)numDeliveriesAverage;
        }
        //פונקציה שתקבל הזמנה ותחשב תאריך צפי לביצוע

        public DateTime GetArivialDateToSpesificOrder(Orders order)
        {  
            Orders orderLast = ordersRepository.SortOrdersNotSend().Last();//השיבוץ האחרון
            // מספר משלוחים שתאריך קבלתם הוא התאריך הכי מאוחר שקים במערכת
            int numOfOrdersToSpecificArivialDate = ordersRepository.GetNumOfOrdersOfSpesificArivialDate(orderLast.ArrivalDate);
            //מספר המשלוחים הממוצע שניתן לשלוח בתאריך של ההזמנה האחרונה
            int numOfQuantityOfDeliveriesToSpecificDate = GetquantityOfDeliveries(orderLast.ArrivalDate);
            //אם התאריך האחרון קטן שווה להיום אז נקבע למחר
            if (orderLast.ArrivalDate<=DateTime.Today)
            {
                DateTime date = DateTime.Today;
                if (Convert.ToInt32(date.DayOfWeek)!=4)
                {
                    return date.AddDays(1);
                }
                else
                {
                    return date.AddDays(3);
                }
            }
            else
            {
                //אם יש מקום בתאריך הזה של ההזמנה האחרונה
                if (numOfQuantityOfDeliveriesToSpecificDate - numOfOrdersToSpecificArivialDate > 0)
                {
                    return orderLast.ArrivalDate;
                }
                else
                {
                    if (Convert.ToInt32(orderLast.ArrivalDate.DayOfWeek) != 4)
                    {
                        return orderLast.ArrivalDate.AddDays(1);
                    }
                    else
                    {
                        return orderLast.ArrivalDate.AddDays(3);
                    }
                }
            }
            
        }

        
        //// יעוררו את הפונקציה הזו מתי שירצו לקבוע להזמנה תאריך קבלה 
        //public void GetNewOrderToSetArivialDate(Orders order)
        //{
        //    DateTime arivialDate = GetArivialDateToSpesificOrder(order);
        //    ordersRepository.SetArivialDate(order.Id, arivialDate);
        //}
        //-סיום קבלת  הזמנה חדשה למערכת הוספת הזמנה חדשה כולל קביעת תאריך קבלה + שליחת מייל ללקוח
        //הפונקציה מקבלת הזמנה ורשימת מבנים של סוגי אריזות של ההזמנה
        //מחזיר את  ההזמנה שנוספה
        public Orders EndOfReceivingOfNewOrder(OrderViewModel order,
           List<PackingAmount> packingAmount)
        {

             Orders order1= ordersRepository.InsertNewOrder(order.IdClient, order.OrderDate, order.OrderTime, order.OrderWeight);
            DateTime date = GetArivialDateToSpesificOrder(order1);
             ordersRepository.SetArivialDate(order1.Id, GetArivialDateToSpesificOrder(order1));
            return order1;
        }
        //הפונקציה מקבלת רשימת של מבנה ORDERSENT 
        //וממינת אותה לפי השדה SerialNumberToSendOrder
        public List<OrderSent> GetSortedOrderSentList(List<OrderSent> orderSents)
        {

            var sort = (from order in orderSents
                        orderby order.SerialNumberToSent
                        select order).ToList();
            return sort;
        }
    
        //הפונקציה מקבלת מ"ז של משלוחן ותאריך שליחה 
        //ומחזירה רשימה של כל הקודים של המשלוחים שצריכים להשלח בתאריך 
        //זה ע"י משלוחן זה כולל כתובת שליחה של כל משלוח 
        //ממוינת לפי המספר הסידורי
        public List<OrderSent> GetOrderListOfSpecificDeliveryManToSpecificDate(string idEmployee, DateTime dateSent)
        {
            List<DetailsInlay> detailsInlays =detailsInlayRepository.
                GetListOrdersOfSpecificDeliveryManAndToSpecificDate(idEmployee, dateSent);
            List<OrderSent> orderSents = new List<OrderSent>();
            OrderSent orderSent;
            foreach (var detailsInlay in detailsInlays)
            {
                orderSent.IdOrder = detailsInlay.IdOrder;
                //שליחה לפונקציה שמקבלת מזהה משלוח ומחזירה את כתובת לשליחה שלו
                orderSent.Adress = GetAdressWithCityName(GetAdressOrder(detailsInlay.IdOrder));
                orderSent.SerialNumberToSent = detailsInlay.SerialNumberToSendOrder;
                orderSents.Add(orderSent);
            }
            //שליחה לפונקציה שממינת את הרשימה לפי המספר הסידורי
            orderSents = GetSortedOrderSentList(orderSents);
            return orderSents;
        }
        //הפונקציה מקבלת תאריך שליחה 
        //ומחזירה רשימה של כל הקודים של המשלוחים שצריכים להשלח בתאריך 
        //זה  כולל כתובת שליחה של כל משלוח 
        //ממוינת לפי המספר הסידורי
        public List<OrderSent> GetOrderListToSpecificDate(DateTime dateSent)
        {
            List<DetailsInlay> detailsInlays = detailsInlayRepository.
                GetListOrdersToSpecificDate( dateSent);
            List<OrderSent> orderSents = new List<OrderSent>();
            OrderSent orderSent;
            foreach (var detailsInlay in detailsInlays)
            {
                orderSent.IdOrder = detailsInlay.IdOrder;
                //שליחה לפונקציה שמקבלת מזהה משלוח ומחזירה את כתובת לשליחה שלו
                orderSent.Adress = GetAdressWithCityName(GetAdressOrder(detailsInlay.IdOrder));
                orderSent.SerialNumberToSent = detailsInlay.SerialNumberToSendOrder;
                orderSents.Add(orderSent);
            }
            //שליחה לפונקציה שממינת את הרשימה לפי המספר הסידורי
            orderSents = GetSortedOrderSentList(orderSents);
            return orderSents;
        }
        //הפונקציה מחזירה פרטי שיבוץ של תאריך מסוים
        //הפונקציה מקבלת תאריך שליחה 
        //ומחזירה רשימה של כל הקודים של המשלוחים שצריכים להשלח בתאריך 
        //זה  כולל כתובת שליחה של כל משלוח 
       //כולל שם שליח שלוח
        
        public List<DetailsInlayOrder> GetDetailsOrderListToSpecificDate(DateTime dateSent)
        {
            List<DetailsInlay> detailsInlays = detailsInlayRepository.
                GetListOrdersToSpecificDate(dateSent);
            List<DetailsInlayOrder> listDetailsInlayOrders = new List<DetailsInlayOrder>();
            DetailsInlayOrder detailsInlayOrder = new DetailsInlayOrder();  
          
            foreach (var detailsInlay in detailsInlays)
            {
                detailsInlayOrder.IdOrder = detailsInlay.IdOrder;
                //שליחה לפונקציה שמקבלת מזהה משלוח ומחזירה את כתובת לשליחה שלו
                detailsInlayOrder.Adress =GetAdressWithCityName( GetAdressOrder(detailsInlay.IdOrder));
                detailsInlayOrder.AriviallDate = detailsInlay.IdOrderNavigation.ArrivalDate;
                detailsInlayOrder.IdDeliveryMan = detailsInlay.IdEmployee;
                detailsInlayOrder.NameDeliveryMan = employeesRepository.GetFullNameById(detailsInlay.IdEmployee);
                listDetailsInlayOrders.Add(detailsInlayOrder);
            }

            return listDetailsInlayOrders; 
        }
        public List<DetailsInlayOrderViewModel> GetDetailsOrderViewModelListToSpecificDate(DateTime dateSent)
        {
            //קריאה לפונקציה שמביאה את הרשימה של OrderSent
            List<DetailsInlayOrder> listDetailsInlayOrder = GetDetailsOrderListToSpecificDate(dateSent);
            List<DetailsInlayOrderViewModel> listDetailsInlayOrderViewModel = new List<DetailsInlayOrderViewModel>();
            foreach (var item in listDetailsInlayOrder)
            {

                listDetailsInlayOrderViewModel.Add(mapper.Map<DetailsInlayOrderViewModel>(item));


            }
            return listDetailsInlayOrderViewModel;

        }
        public List<OrderSentViewModel> GetOrderViewModelListToSpecificDate( DateTime dateSent)
        {
            //קריאה לפונקציה שמביאה את הרשימה של OrderSent
            List<OrderSent> orderSents = GetOrderListToSpecificDate(dateSent);
            List<OrderSentViewModel> orderSentViewModels = new List<OrderSentViewModel>();
            foreach (var item in orderSents)
            {
                
                orderSentViewModels.Add(mapper.Map<OrderSentViewModel>(item));


            }
            return orderSentViewModels;
        }
        public List<OrderSentViewModel> GetOrderViewModelListOfSpecificDeliveryManToSpecificDate(string idEmployee, DateTime dateSent)
        {
            //קריאה לפונקציה שמביאה את הרשימה של OrderSent
            List<OrderSent> orderSents = GetOrderListOfSpecificDeliveryManToSpecificDate(idEmployee, dateSent);
            List<OrderSentViewModel> orderSentViewModels = new List<OrderSentViewModel>();
            foreach (var item in orderSents)
            {
                orderSentViewModels.Add(mapper.Map<OrderSentViewModel>(item));


            }
            return orderSentViewModels;
        }
        //הפונקציה מקבלת מ"ז של משלוחן ותאריך שליחה 
        //ומחזירה רשימה של נקודות של כתובות שליחת המשלוחים שצריכים להשלח בתאריך 
        //זה ע"י משלוחן זה  
        
        public List<LatLng> GetOrderLatlagListOfSpecificDeliveryManToSpecificDate(string idEmployee, DateTime dateSent)
        {
            List<OrderSent> listOrderSent = GetOrderListOfSpecificDeliveryManToSpecificDate(idEmployee, dateSent);
            //יצירת רשימת נקודות של כתובות שליחת המשלוחים
            List<Adress> listAdresses = new List<Adress>();
            
            foreach (var orderSent in listOrderSent)
            {
                listAdresses.Add(GetAdressFromAddresWithCityName(orderSent.Adress));
            }
            //שליחה לפונקציה שממירה רשימה של כתובות לרשימה של נקודות
            List<LatLng> listlLatLngs= mapService.ConvertAdressListToLatlng(listAdresses);
            //LatLng[] latLngs = mapService.newOriginDestinationAndWaypoints(listlLatLngs);
            listlLatLngs.Insert(0, mapService.ConvertAddressToPoint(addressCentral));
            return listlLatLngs;
        }
        //הפונקציה מקבלת מזהה משלוח ומחזירה את הכתובת לשליחה שלו
        public Adress GetAdressOrder(int idOrder)
        {
            //בדיקה האם המשלוח הזה יש לשלוח אותו לכתובת שונה 
            //או לכתובת של הלקוח שהזמין אותו
            //זימון פונקציה שמקבלת מזהה משלוח ומחזירה האם יש לו כתובת שונה
            if (differentShipingAdressRepository.IsDifferentAddress(idOrder))
            {
                //זימון פונקציה שמחזירה את הכתובת השונה במבנה כתובת
                return differentShipingAdressRepository.GetAdressByIdOrder(idOrder);
            }
            //אם כתובת השליחה זהה לכתובת הלקוח
            else
            {
                //נחזיר את הכתובת של הלקוח של המשלוח
                Orders order = ordersRepository.GetById(idOrder);
                return clientRepository.GetAdressByIdClient(order.IdClient);
            }
           
        }
        //פונקציה שממירה מבנה מסוג Adress
        //למבנה מסוג AdressWithCityName
        public AdressWithCityName GetAdressWithCityName(Adress adress)
        {
            AdressWithCityName adressWithCityName = new AdressWithCityName();
            adressWithCityName.NameCityAdress = cityRepository.ConvertIdToName(adress.IdCityAdress);
            adressWithCityName.ApartmentNumber = adress.ApartmentNumber;
            adressWithCityName.BuldingNumber = adress.BuldingNumber;
            adressWithCityName.EntranceNumber = adress.EntranceNumber;
            adressWithCityName.StreetAdress = adress.StreetAdress;
            return adressWithCityName;
        }
        //פונקציה שממירה מבנה מסוג Adress
        //למבנה מסוג AdressWithCityName
        public Adress GetAdressFromAddresWithCityName(AdressWithCityName adressWithCityName)
        {
            Adress adress = new Adress();
            adress.ApartmentNumber = adressWithCityName.ApartmentNumber;
            adress.BuldingNumber = adressWithCityName.BuldingNumber;
            adress.EntranceNumber = adressWithCityName.EntranceNumber;
            adress.IdCityAdress = cityRepository.GetCityCodeFromCityName(adressWithCityName.NameCityAdress);
            adress.StreetAdress = adressWithCityName.StreetAdress;
            return adress;
        }











        ///////////////////פונקציות עזר הקשורות לפונקציה שמחשבת הזמנות לתאריך מסוים
        ///צריך למחוק
        ////Calculates the average of delivery capicity
        ////public static double GetAveregeDeliveryCapicity()
        ////{

        ////    var averageOfCapicity = (ReadOrderManagerDataBase.GetListOfCapicityOrders()).Average();
        ////    return averageOfCapicity;

        ////}
        ///למחוק
        //////Calculates the average of delivery weight
        ////public static double GetAveregeDeliveryWeight()
        ////{

        ////    var averageOfWeight = (from order in ReadOrderManagerDataBase.GetAllOrders()
        ////                           select order.OrderWeight).Average();
        ////    return averageOfWeight;

        ////}
        ///למחוק
        //////Calculates the average of יכולת קיבול של נפח של רכבים ליום מסוים
        ////public static double GetSumCarsCapicitySpesificDate(DateTime date)
        ////{
        ////    var sumCarsCapicity = (from employee in ReadEmployeeManagerDataBase.GetEmployeesToSpesificDate(date)
        ////                           select employee.Car.Capacity).Sum();
        ////    return sumCarsCapicity;

        ////}
        //////Calculates the average of יכולת קיבול של משקל של רכבים ליום מסוים
        ////public static double GetSumCarsWeightCapicitySpesificDate(DateTime date)
        ////{
        ////    var sumCarsWeightCapicity = (from employee in ReadEmployeeManagerDataBase.GetEmployeesToSpesificDate(date)
        ////                                 select employee.Car.WeightCapacity).Sum();
        ////    return sumCarsWeightCapicity;

        ////}
        ///צריך לבטל
        //////פונקציה שממינת את כל ההזמנות שלא נשלחו מחזירה רשימה ממוין
        //////public static List<OrderViewModel> SortOrdersNotSend()
        //////{
        //////    using (DeliverySystemContext context = new DeliverySystemContext())
        //////    {
        //////        var sort = (from order in ReadOrderManagerDataBase.GetListOfOrdersNotSend()
        //////                    orderby order.ArrivalDate, order.OrderTime
        //////                    select order).ToList();
        //////        return sort;
        //////    }
        //////}

    }
}
