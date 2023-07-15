using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories
{
    class InlaysRepository : IInlaysRepository
    {
        DeliverySystemContext context;
        public InlaysRepository(DeliverySystemContext context)
        {
            this.context = context;
        }
        public void Create(Inlays objectCreate)
        {
            context.Inlays.Add(objectCreate);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Inlays> GetAll()
        {
            throw new NotImplementedException();
        }

        public Inlays GetById(int i)
        {
            throw new NotImplementedException();
        }

        public void Update(Inlays objectCreate)
        {
            throw new NotImplementedException();
        }
        //הוספת רשומה חדשה של שיבוץ- מקבלים תאריך שליחה של משלוח 
        public  void SetInlay(DateTime date)
        {
            
            Inlays inlay = new Inlays();
            inlay.DateInlay = DateTime.Today;
            inlay.ArrivalDateOrder = date;
            Create(inlay);


            
        }
        //שליפת מערך של משלוחים ששובצו ליום מסוים 
        public  Inlays[] GetArrayInlaysTospesificArivalDate(DateTime date)
        {
            var inlays = context.Inlays.Where(i => i.ArrivalDateOrder == date).ToArray();
            return inlays;
            

        }
       
    }
}
