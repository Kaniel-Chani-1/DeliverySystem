using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repositories.Models;

namespace Repositories
{
    class DetailsInlaysRepository : IDetailsInlayRepository
    {
        DeliverySystemContext context;
        public DetailsInlaysRepository(DeliverySystemContext context)
        {
            this.context = context;   
        }
        public void Create(DetailsInlay objectCreate)
        {
            context.DetailsInlay.Add(objectCreate);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<DetailsInlay> GetAll()
        {
            throw new NotImplementedException();
        }

        public DetailsInlay GetById(int i)
        {
            throw new NotImplementedException();
        }

        public void Update(DetailsInlay objectCreate)
        {
            throw new NotImplementedException();
        }
        //הוספת רשומה חדשה של פירוט  שיבוץ- מקבלים קוד משלוחן קוד שיבוץ וקוד משלוח ומספר סידורי 
        public  void SetDetailsInlay(string idEmployee, int idInlay, int idOrder, int serialNumber)
        {
            DetailsInlay detailsInlay = new DetailsInlay();
            detailsInlay.IdOrder = idOrder;
            detailsInlay.IdEmployee = idEmployee;
            detailsInlay.IdInlay = idInlay;
            detailsInlay.SerialNumberToSendOrder = serialNumber;
            Create(detailsInlay);

        }
        //פונקציה שמחזירה רשימה של פרטי שיבוץ ששובצו למשלוחן מסוים לתאריך מסוים
        public List<DetailsInlay> GetListOrdersOfSpecificDeliveryManAndToSpecificDate(string idEmployee, DateTime date)
        {

            return (context.DetailsInlay.Where(d => d.IdEmployee.Equals(idEmployee) && d.IdOrderNavigation.ArrivalDate.Equals(  date))).ToList();
           
            
        }
        //פונקציה שמחזירה רשימה של פרטי שיבוץ ששובצו  לתאריך מסוים
        public List<DetailsInlay> GetListOrdersToSpecificDate( DateTime date)
        {

            return (context.DetailsInlay.Where(d =>  d.IdOrderNavigation.ArrivalDate.Equals(date))).ToList();


        }
        //הפונקציה מקבלת מזהה משלוח ומספר סידורי לשליחה ומעדכנת
        public void UpdateSerialNumber(int idOrder, int serialNumber)
        {
            DetailsInlay detailsInlay = context.DetailsInlay.Where(d => d.IdOrder == idOrder).FirstOrDefault();
            detailsInlay.SerialNumberToSendOrder = serialNumber;
            context.SaveChanges();

        }


    }
}
