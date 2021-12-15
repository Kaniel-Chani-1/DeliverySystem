using System;
using System.Collections.Generic;
using System.Text;
using static AppServices.GeneticServices;
using System.Collections;
using System.Linq;
using Common.Models;
using Repositories;
using static AppServices.IndividualServices;
using Google.Maps;
using Google.Maps.Direction;

namespace AppServices
{

    public class IndividualServices:IIndividualServices
    {
        public Dictionary<string, DeliveryPersonChromosome> chromosome { get; set; }
        public double fitness { get; set; }
        IPackingTypesRepository packingTypesRepository;
        IDetailsOfShiftsRepository detailsOfShiftsRepository;
        IEmployeesRepository employeesRepository;
        IClientRepository clientRepository;
        IMapService mapService;
        IAlgorithmService algorithmService;
        public IndividualServices(IPackingTypesRepository packingTypesRepository,
            IEmployeesRepository employeesRepository,
            IMapService mapService,
            IClientRepository clientRepository,
            IAlgorithmService algorithmService,
            IDetailsOfShiftsRepository detailsOfShiftsRepository)
        {
            this.employeesRepository = employeesRepository;
            this.packingTypesRepository = packingTypesRepository;
            this.mapService = mapService;
            this.clientRepository = clientRepository;
            this.algorithmService = algorithmService;
            this.detailsOfShiftsRepository = detailsOfShiftsRepository;
        }
        public IndividualServices()
        {

        }
        public IndividualServices(Dictionary<string, DeliveryPersonChromosome> chromosome)
        {
            this.chromosome = chromosome;
            

        }

        //פונקציה שמקבלת אינדודואל ומחזירה את הציון של השיבוץ שלו-הכרומוזום
        public double cal_fitness(IndividualServices individual)
        {
            double fitness = 0;
            foreach (KeyValuePair<string, DeliveryPersonChromosome> chro in individual.chromosome)
            {
                
                if (chro.Value.ListOrders.Count>0)
                {
                    fitness += SetFitnessDeliveryPersonChromosome(chro.Value, chro.Key);
                }
               
            }

            return fitness;

        }

        // חשוב הציון של שיבוץ של שליח ספציפי
        public double DeliveryPersonChromosome_fitness(DeliveryPersonChromosome deliveryPersonChromosome, string idDelivery)
        {
           
            //מספר המשלוחים ששובצו לכרומוזום
            int numOrders = deliveryPersonChromosome.ListOrders.Count();
            double fitness = 0;
            //סך משקל כל המשלוחים
            double weightSum = 0;
            //סך נפח כל המשלוחים
            double capicitySum = 0;
            //זמן מסלול הנסיעה
            int timeDirection = 0;
            //סך השעות של שלוקח משוער השליחה של המשלוחים
            double WorkTime = 0;
            //רשימה של כתובות בייצוג של נקודה
            List<LatLng> listLatLngs = new List<LatLng>();
            //מערך של כתובות בייצוג של נקודה
            LatLng[] arrLatLngs = new LatLng[numOrders];
            //משתנה קבוע שמייצג מספר דקות בשעה - 60
            const int minutesPerHour = 60;
            //משתנה קבוע המייצג זמן משוער בדקות שלוקח לשליח
            //מאז שהוא מגיע לכתובת עד שהוא ממשיך לכתובת הבאה
            const int deliveryTimePerDelivery = 15;
            //המשתנה מכיל את התאריך שלו משבצים
            DateTime dateSend = deliveryPersonChromosome.ListOrders.First().ArrivalDate;
            foreach (var order in deliveryPersonChromosome.ListOrders)
            {
                weightSum += order.OrderWeight;
                capicitySum += packingTypesRepository.GetCapicityOfOrder(order);
            }
            //אם המשקל של השיבוץ גדול מיכולת כיבול הרכב 
            if (employeesRepository.GetWeightCarEmployee(idDelivery) < weightSum)
            {
                double hefresh = weightSum - employeesRepository.GetWeightCarEmployee(idDelivery);
                fitness += (Convert.ToDouble(Constraint.WeightCarryCar) * hefresh);
            }
            
            //אם הנפח של השיבוץ גדול מיכולת כיבול הרכב 
            if (capicitySum > employeesRepository.GetCapicityCarEmployee(idDelivery))
            {
                double hefresh = capicitySum - employeesRepository.GetCapicityCarEmployee(idDelivery);
                fitness += (Convert.ToDouble(Constraint.CapicityCarryCar) * hefresh);
            }

            //שליפת רשימת נקודות של כתובות המשלוחים מהמילון הסטטי
            foreach (var order in deliveryPersonChromosome.ListOrders)
            {
                listLatLngs.Add(GeneralData.StaticDetailsOrders[order.Id].PointOnMap);
            }
            //שליחה לפונקציה שקובעת  יעד לרשימת נקודות
            //מחזירה  מערך שבמקום הראשון יש את נקודת המקור
            //שהיא נקודת כתובת מרכז האריזה של החברה
            //של המסלול ובמקום השני יש את היעד של המסלול ובשאר המקומות נקודות אמצע במסלול
            arrLatLngs = mapService.OriginDestinationAndWaypoints(listLatLngs);
            //העתקת נקודות אמצע המסלול לרשימה
            List<LatLng> listWayPoints = new List<LatLng>();
            for (int i = 2; i < arrLatLngs.Length; i++)
            {
                listWayPoints.Add(arrLatLngs[i]);
            }
            //שליחה לפונקציה שמקבלת נקודת מקור ונקודת יעד ורשימת נקודות ומחזירה אוביקט מסוג 
            //DirectionResponse  
            DirectionResponse direction = mapService.CreateDirection(listWayPoints, arrLatLngs[0], arrLatLngs[1]);
            //שליחה לפונקציה שמחזירה את הזמן המסלול בדקות
            timeDirection = mapService.GetTimeDirection(direction);
            //זמן משוער של עבודה של שליחת כל המשלוחים
            WorkTime = timeDirection + (numOrders * deliveryTimePerDelivery);
            // מספר שעות שעובד אותו משלוחן באותו יום
            double workTimeEmployee = (detailsOfShiftsRepository.GetShiftTimeToSpecificEmploAndDay(Convert.ToInt32(dateSend.DayOfWeek), idDelivery)) * minutesPerHour;
            //אם מספר השעות שעובד השליח קטן ממספר השעות שלוקח המסלול נגדיל את הציון
            if (WorkTime > workTimeEmployee)
            {
                double hefresh = WorkTime - workTimeEmployee;
                fitness += (Convert.ToDouble(Constraint.WorkTime) * hefresh);
            }
            //בדיקת פיזור איזורים
            //שליחה לפונקציה שמחזירה את גודל התיחום של נקודות הכתובות
            double sizeArea = mapService.AreaSizePoints(listLatLngs);
            //חלוקת שטח התיחום לפי מספר משלוחים והוספה לציון
            fitness += ((sizeArea / numOrders) * Convert.ToDouble(Constraint.ScatteringAreas));
            return fitness;
        }
        //פונקציה שקובעת לאנדיודואל מסוים את הציון שלו
        public void SetIndividualFitness(IndividualServices individual)
        {
            individual.fitness = cal_fitness(individual);
        }
        // פונקציה שקובעת לכרומוזום של שליח את הציון שלו
       //ומחזיר את הציון שנקבע
        public double SetFitnessDeliveryPersonChromosome(DeliveryPersonChromosome deliveryPersonChromosome, string idDeliveryPerson)
        {
            //אם יש לשליח משלוחים - אם שובצו לו
            if (deliveryPersonChromosome.ListOrders.Count>0)
            {
                deliveryPersonChromosome.Fitness = DeliveryPersonChromosome_fitness(deliveryPersonChromosome, idDeliveryPerson);

            }
            else
            {
                deliveryPersonChromosome.Fitness = 0;
            }
            return deliveryPersonChromosome.Fitness;
            
        }
       
      
    }
}
