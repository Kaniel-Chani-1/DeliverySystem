using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Repositories
{
    class OrderRepository:IOrdersRepository
    {
        DeliverySystemContext context;
        public OrderRepository(DeliverySystemContext context)
        {
            this.context = context;
        }

        
        public void Create(Orders objectCreate)
        {
            context.Orders.Add(objectCreate);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Orders> GetAll()
        {
            var orders = context.Orders.ToList();
            return orders;
        }

        public Orders GetById(int id)
        {
            var order = context.Orders.Where(o => o.Id == id).FirstOrDefault();
            return order;
        }

        public void Update(Orders objectCreate)
        {
            throw new NotImplementedException();
        }
        //קביעת שדה תאריך קבלה  להזמנה מסוימת- לפי קוד הזמנה ותאריך 
        public void SetArivialDate(int id, DateTime date)
        {
            var order = (from order1 in context.Orders
                         where order1.Id == id
                         select order1).FirstOrDefault();
            order.ArrivalDate = date;
            context.SaveChanges();

        }
        
        // הוספת הזמנת משלוח חדשה
        //מחזיר את  ההזמנה שנוספה
        public Orders InsertNewOrder(string idClient, DateTime orderDate,
                                        TimeSpan orderTime, double orderWeight)                               
        {
            Orders order = (new Orders()
            {
                IdClient = idClient,
                OrderDate = orderDate,
                OrderTime = orderTime,
                OrderWeight = orderWeight,
                Status = 1,
            });
            Create(order);
            return order;
        }
        //The function pulls out all the orders for a specific date as list
        public List<Orders> GetOrdersOfSpesificArivialDate(DateTime date)
        {
                // List<Order> orders = null;
                var orders = context.Orders.
                            Where(o => o.ArrivalDate == date).ToList();
                var orders2 = (from o in context.Orders
                               where o.ArrivalDate == date
                               select o).ToList();
                return orders;
        }
        //The function pulls out all the orders for a specific date as array
        public  Orders[] GetArrayOrdersOfSpesificArivialDate(DateTime date)
        {
            // List<Order> orders = null;
            var orders = context.Orders.
                        Where(o => o.ArrivalDate == date).ToArray();
            var orders2 = (from o in context.Orders
                           where o.ArrivalDate == date
                           select o).ToArray();

            return orders;
           
           

        }
        //A function that pulls out all orders
        public  List<Orders> GetAllOrders()
        {
            var orders = context.Orders.ToList();
            return orders;
           

        }
        //A function that pulls out all orders  details
        public  List<Orders> GetAllOrdersDetails()
        {
            var orders = context.Orders.Include(a => a.DetailsOfTheContentsOfReservation).ToList();
            return orders;
        }
        //Function that pull information about the content of the order
        public  List<DetailsOfTheContentsOfReservation> GetContentsOfReservations()
        {
            
                var detailsOrders = context.DetailsOfTheContentsOfReservation.Include(p => p.IdPackingTypes).ToList();
                return detailsOrders;

            
        }
        //Function that returns a list of capicity of all orders
        public  List<double> GetListOfCapicityOrders()
        {
            var capicityList = (from capicity in context.DetailsOfTheContentsOfReservation
                                select (capicity.IdPackingTypesNavigation.EstimatedCapicity) * (capicity.Amount)).ToList();
            return capicityList;
           
        }
        //Function that returns a number of  orders שקימות במערכת
        public  int GetNumOfAllOrders()
        {
            using (DeliverySystemContext context = new DeliverySystemContext())
            {
                var numOrders = context.Orders.Count();
                return numOrders;
            }

        }
        //פונקציה ששולפת הזמנה לפי קוד הזמנה
        public Orders GetOrderById(int id)
        {
            var order = context.Orders.Where(o => o.Id == id).FirstOrDefault();
            return order;

        }
        //פונקציה שמחזירה את מספר ההזמנות שתאריך קבלתם ליום מסוים 
        public  int GetNumOfOrdersOfSpesificArivialDate(DateTime arivialDate)
        {
            var numOrders = GetOrdersOfSpesificArivialDate(arivialDate).Count();
            return numOrders;

        }
        //פונקציה שמחזירה מספר הזמנות שלא נשלחו
        public  int GetNumOfOrdersNotSend()
        {
            var numOrders = (context.Orders.Where(o => o.ArrivalDate > DateTime.Today)).Count();
            return numOrders;
            

        }
        //פונקציה שמחזירה רשימת הזמנות שלא נשלחו
        public  List<Orders> GetListOfOrdersNotSend()
        {
            var orders = (context.Orders.Where(o => o.Status == 1)).ToList();//הסטטוס - ממתין לשליחה
            return orders;

        }
        //פונקציה שממינת את כל ההזמנות שלא נשלחו מחזירה רשימה ממוין 
        public  List<Orders> SortOrdersNotSend()
        {
            var sort = (from order in GetListOfOrdersNotSend()
                        orderby order.ArrivalDate, order.OrderTime
                        select order).ToList();
            return sort;



        }
        //Calculates the average of delivery capicity
        public double GetAveregeDeliveryCapicity()
        {

            var averageOfCapicity = (GetListOfCapicityOrders()).Average();
            return averageOfCapicity;

        }
        //Calculates the average of delivery weight
        public  double GetAveregeDeliveryWeight()
        {

            var averageOfWeight = (from order in GetAllOrders()
                                   select order.OrderWeight).Average();
            return averageOfWeight;

        }
        //הפונקציה מקבלת מזהה משלוח
        //ומחזירה את מזהה הלקוח של משלוח זה
        public string GetIdClientByIdEmployee(int idOrder)
        {
            return GetById(idOrder).IdClient;
        }
       //הפונקציה מקבלת מזהה משלוח ושם סטטוס חדש ומעדכנת את הסטטוס החדש
       public void UpdateStatusOrder(int idOrder,int idStatus)
        {
            Orders order = context.Orders.Where(o => o.Id == idOrder).FirstOrDefault();
            order.Status = idStatus;
            context.SaveChanges();
        }
       

    }
}
