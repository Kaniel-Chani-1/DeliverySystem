using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;
using Common.Models;
using Repositories;
using static AppServices.IndividualServices;
using Repositories.Models;
using System.IO;


namespace AppServices
{
   public class GeneticServices:IGeneticServices
    {
        IPackingTypesRepository packingTypesRepository;
        IIndividualServices individualServices;

        public GeneticServices(IPackingTypesRepository packingTypesRepository,
            IIndividualServices individualServices
           )
        {
            this.packingTypesRepository = packingTypesRepository;
            this.individualServices = individualServices;

        }
        static int POPULATION_SIZE;
        static int MAX_GENERETIONS;
        static int MAX_TRIES;
        static Orders[] GENES_ORDERS;
        static Employees[] GENES_EMPLOYEE;
        static int NUM_ORDERS;
        static int NUM_EMPLOYEES;
        //פונקציה המאתחלת נתונים עבור האלגוריתם הגנטי
        public void InitGenetic(Orders[] orders, Employees[] employees)
        {
            // Number of individuals in each generation 
            POPULATION_SIZE = 100;
            GENES_EMPLOYEE = employees;
            GENES_ORDERS = orders;
            MAX_GENERETIONS = 50;
            MAX_TRIES = 50;
            NUM_EMPLOYEES = employees.Length;
            NUM_ORDERS = orders.Length;

        }
        public IndividualServices Genetic(Orders[] orders, Employees[] employees)//להוסיף
        {
            //the main algorithm of the project implement genetic algorithm
            InitGenetic(orders, employees);
            //current generation
            int generation = 0;
            //יצירת הדור הראשון ע"י זימון הפונקציה שמיצרת
            IndividualServices[] population = Create_Random_Population();
            StreamWriter myFile = File.CreateText(@"C:\logFile.txt");
            myFile.WriteLine("Random_Population");
            myFile.WriteLine(population[0].fitness + " :fitness");
            for (int j = 0; j < GENES_EMPLOYEE.Length; j++)
            {
                myFile.WriteLine(GENES_EMPLOYEE[j].LastName + " " + GENES_EMPLOYEE[j].FirstName);
                foreach (var item in population[0].chromosome[GENES_EMPLOYEE[j].Id].ListOrders)
                {
                    myFile.Write("idOrder:" + item.Id + ",");

                }

            }
            myFile.Close();
            for (int i = 1; i < POPULATION_SIZE-1; i++)
            {
                FileStream myFile3 = File.OpenWrite(@"C:\logFile.txt");
                //myFile3.WriteLine("Random_Population");
                myFile3.WriteLine(population[i].fitness + " :fitness");
                for (int j = 0; j < GENES_EMPLOYEE.Length; j++)
                {
                    myFile3.WriteLine(GENES_EMPLOYEE[j].LastName + " " + GENES_EMPLOYEE[j].FirstName);
                    foreach (var item in population[i].chromosome[GENES_EMPLOYEE[j].Id].ListOrders)
                    {
                        myFile3.Write("idOrder:"+item.Id+",");

                    }
                   
                }
                myFile3.Close();
                
                }
            bool found = false;
            //  אתחלתי את האנדודואל המינימלי לאנדודואל הראשון בדור(אפשר לבחור גם כל אחד אחר בדור זה)  
            IndividualServices minIndividual = new IndividualServices(population[0].chromosome);
            //קביעת ציון לאינדוודואל המינימלי האתחלתי
            individualServices.SetIndividualFitness(minIndividual);
            while ((!found) && (generation < MAX_GENERETIONS))
            {
                //משתנה שמציין את המקום שאני נמצאת במערך של האינדודואל
                int location = 0;
                //sort the population in decreacing order of fitness score
                population = MergePopulationByCalFitness(population);
                for (int i = 0; i < POPULATION_SIZE; i++)
                {
                    StreamWriter myFile1 = File.CreateText(@"C:\logFile.txt");
                    myFile1.WriteLine("generation:" + " " + generation);
                    myFile1.WriteLine(population[i].fitness + " :fitness");
                    for (int j = 0; j < GENES_EMPLOYEE.Length; j++)
                    {
                        myFile1.WriteLine(GENES_EMPLOYEE[j].LastName + " " + GENES_EMPLOYEE[j].FirstName);
                        foreach (var item in population[i].chromosome[GENES_EMPLOYEE[j].Id].ListOrders)
                        {
                            myFile1.Write("idOrder:" + item.Id + ",");

                        }

                    }
                    myFile1.Close();

                }
                //if the individual having lowest fitness score ie
                //0 then we know that we have reached to the target
                //and break the loop
                if (population[0].fitness == 0)
                {
                    found = true;
                    break;
                }
                //otherwise generate new offsprings for new generation
                IndividualServices[] new_generation = new IndividualServices[POPULATION_SIZE];
                //Perform Elitism, that 10% of fittest population
                //goes to the next generation
                int s = (10 * POPULATION_SIZE) / 100;
                for (int i = 0; i < s; i++)
                {
                    //עוברים כמו שהם לדור הבא
                    new_generation[i] = population[i];
                    location++;
                    if (population[i].fitness <= minIndividual.fitness)
                    {
                        //שמירת השיבוץ בעל הציון הנמוך ביותר
                        minIndividual = population[i];
                    }


                }
                // From 90% of fittest population, Individuals 
                // will mate to produce offspring 
                s = (90 * POPULATION_SIZE) / 100;
                Random rand = new Random();
                int r;
                for (int i = 0; i < s; i++)
                {
                    int len = (50 * POPULATION_SIZE) / 100;
                    //בוחרים ע"י הגרלה את האנדודואלים שהולכים לייצר את הדור הבא
                    r = rand.Next(0, len - 1);
                    IndividualServices parent1 = population[r];
                    r = rand.Next(0, len - 1);
                    IndividualServices parent2 = population[r];
                    //יצירת הצאצא ע"י זימון הפונקציה MATE
                    IndividualServices offspring = Mate(parent1, parent2);
                    //הוספת הצאצא לדור החדש
                    new_generation[location] = offspring;
                    //הגדלת מונה מיקום במערך הדור החדש
                    location++;
                }
                //הכנסת הדור החדש למשתנה POPULATION
                population = new_generation;
                Console.WriteLine("Generation : " + generation);
                Console.WriteLine("  Fitness : " + population[0].fitness);
                generation++;

            }
            if (generation == MAX_GENERETIONS)
            {
                return minIndividual;
            }
            else
            {
                return population[0];
            }

        }
        //יצירת דור שיבוצים ראשוני - רנדומלי

        public IndividualServices[] Create_Random_Population()
        {
            IndividualServices[] individuals = new IndividualServices[POPULATION_SIZE];
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                
                individuals[i] = new IndividualServices(Create_Random_Chromosom(GENES_ORDERS, GENES_EMPLOYEE));
                //קביעת ציון לאינדוודואל
                individualServices.SetIndividualFitness(individuals[i]);


            }
            return individuals;
        }
        //הפונקציה יוצרת את הדור הראשוני הרנדומלי
        // הפונקציה מקבלת מערך של משלוחים לשיבוץ ומערך של משלחונים
        // ומחזירה מילון של שיבוצים רנדומלי
        public Dictionary<string, DeliveryPersonChromosome> Create_Random_Chromosom(Orders[] order, Employees[] employees)
        {

            Dictionary<string, DeliveryPersonChromosome> chromosome = new Dictionary<string, DeliveryPersonChromosome>();
            DeliveryPersonChromosome deliveryPersonChromosome;

            //מספר משלוחים 
            int numOrders = order.Length;
            //מספר משלחונים
            int numEmployees = employees.Length;
            //מספר ממוצע של משלוחים למשלוחן
            int numOrdersOfEmployee = numOrders / numEmployees;
            //שארית של חלוקת המשלוחים למשלחונים
            int remainderOrders = numOrders % numEmployees;
            Random rand = new Random();
            //משתני עזר להגרלה
            int x, y, z, d;
            Orders temp;
            //ערבוב מערך המשלוחים
            for (int i = 0; i < 50; i++)
            {
                x = rand.Next(0, numOrders );
                y = rand.Next(0, numOrders );
                temp = order[x];
                order[x] = order[y];
                order[y] = temp;



            }
            //חלוקה המשלוחים למשלחונים
            //משתנה מצביע לרשימת המשלוחים
            int t = 0;
            for (int i = 0; i < numEmployees; i++)
            {
                deliveryPersonChromosome = new DeliveryPersonChromosome();
                deliveryPersonChromosome.ListOrders = new List<Orders>();
                for (int j = 0; j < numOrdersOfEmployee; j++)
                {

                    deliveryPersonChromosome.ListOrders.Add(order[t++]);
                }
                chromosome.Add(employees[i].Id, deliveryPersonChromosome);
            }
            //אם יש שארית לחלוקה של המשלוחים למשלחונים אז נוסיף את השארית באופן אקראי לכמה משלחונים
            if (remainderOrders > 0)
            {
                for (int i = numOrdersOfEmployee * numEmployees + 1; i < remainderOrders; i++)
                {
                    x = rand.Next(numEmployees );
                    chromosome[employees[x].Id].ListOrders.Add(order[i]);

                }
            }
            //נגריל מספר z- 
            //נבצע לולאה מ1 עד z 
            //בכל איטרציה נגריל 2 משלוחנים, ונעביר משלוח ממשלוחן אחד לשני
            z = rand.Next(100000);
            for (int i = 0; i < z; i++)
            {
                //הגרלת אינדקס משלוחן 1
                x = rand.Next(0, numEmployees );
                //הגרלת אינדקס משלוחן 2
                y = rand.Next(0, numEmployees );
                //הגרלת אינדקס משלוח להעברה
                //מספר משלוחים ששובצו למשלוחן X
                int xOrders = (chromosome[employees[x].Id].ListOrders).Count;
                //אם מספר המשלוחים שיש למשלוחן X היא אפס תמשיך לאיטרציה הבאה
                if (xOrders == 0)
                {
                    continue;
                }
                d = rand.Next(0, xOrders - 1);
                //מציאת המשלוח שנמצא באינדקס שהוגרל
                Orders order1 = chromosome[employees[x].Id].ListOrders[d];
                //מחיקת המשלוח ממשלוחן X
                chromosome[employees[x].Id].ListOrders.Remove(order1);
                //הוספת המשלוח למשלוחןY
                chromosome[employees[y].Id].ListOrders.Add(order1);
            }
            

            return chromosome;
        }

        // פונקציה שמקבלת שיבוץ, משלוח ומשלוחן מקור ומשלוחן יעד ומעבירה את השיבוץ של המשלוח
        public void ChangeEmployeeOfOrder(Dictionary<string, DeliveryPersonChromosome> chromosome,
            Orders order, Employees sourceemployee, Employees targetemployee)
        {
            
            chromosome[sourceemployee.Id].ListOrders.Remove(order);
            chromosome[targetemployee.Id].ListOrders.Add(order);
        }
        //פונקציה שממינת את השיבוצים שבדור מהקטן לגדול לפי הציון של השיבוץ
        public IndividualServices[] MergePopulationByCalFitness(IndividualServices[] individuals)
        {
            IndividualComparer individualComparer = new IndividualComparer();
            Array.Sort(individuals, new IndividualComparer());
            return individuals;


        }

        //Perform mating and produce new offspring
        public IndividualServices Mate(IndividualServices parent1, IndividualServices parent2)
        {
            Random rand = new Random();
            // chromosome for offspring 
            IndividualServices individual = new IndividualServices();
            individual.chromosome = new Dictionary<string, DeliveryPersonChromosome>();
            //אתחול המילון
            for (int i = 0; i < GENES_EMPLOYEE.Length; i++)
            {
                DeliveryPersonChromosome chromosome = new DeliveryPersonChromosome();
                chromosome.ListOrders = new List<Orders>();
                individual.chromosome.Add(GENES_EMPLOYEE[i].Id, chromosome);
            }

            int len = GENES_ORDERS.Count();
            for (int i = 0; i < len; i++)
            {
                // random probability  
                double p = rand.Next(101);

                // if prob is less than 45, insert gene 
                // from parent 1  
                if (p < 45)
                    individual.chromosome[GetDeliveryManOfOrder(parent1, GENES_ORDERS[i])].ListOrders.Add(GENES_ORDERS[i]);
                // if prob is between 45 and 90, insert 
                // gene from parent 2 
                else if (p < 90)
                    individual.chromosome[GetDeliveryManOfOrder(parent2, GENES_ORDERS[i])].ListOrders.Add(GENES_ORDERS[i]);
                // otherwise insert random gene(mutate),  
                // for maintaining diversity 
                else
                {
                    int r = rand.Next(0, 2);
                    if (r == 0)
                    {
                        individual.chromosome[GetDeliveryManOfOrder(parent1, GENES_ORDERS[i])].ListOrders.Add(GENES_ORDERS[i]);
                    }
                    else
                    {
                        individual.chromosome[GetDeliveryManOfOrder(parent2, GENES_ORDERS[i])].ListOrders.Add(GENES_ORDERS[i]);
                    }
                }
            }
            //קביעת הציון של כל האנדוודואל
            //קביעת ציון לצאצא החדש שנוצר
            individualServices.SetIndividualFitness(individual);
           
            return individual;
        }
        //פונקציה שמקבלת משלוח ואינדוודואל ומחזירה את המזהה של השליח שאליו שייך המשלוח במערך המשלוחים
        public string GetDeliveryManOfOrder(IndividualServices individual,Orders orders)
        {
            string idDeliveryMan = "";
            foreach (KeyValuePair<string, DeliveryPersonChromosome> chro in individual.chromosome)
            {
                Orders orders1 = chro.Value.ListOrders.Where(o => o.Id == orders.Id).FirstOrDefault();
                if (orders1 != null)
                {
                   idDeliveryMan= chro.Key;
                    break;
                }

            }
            return idDeliveryMan;
        }
        // פונקציה שמקבלת משלוח מקור ומשלוח יעד להחלפה ןאנדודואל
        public static void SwapOrders(Orders source, Orders target, IndividualServices individual)
        {

            for (int i = 0; i < individual.chromosome.Count - 1; i++)
            {
                if (individual.chromosome[GENES_EMPLOYEE[i].Id].ListOrders.Contains(source))
                {
                    individual.chromosome[GENES_EMPLOYEE[i].Id].ListOrders.Remove(source);
                    individual.chromosome[GENES_EMPLOYEE[i].Id].ListOrders.Add(target);
                }

            }

        }
        //בדיקה האם שתי משלוחים הם זהים
        public static bool EqualsOrders(Orders order1, Orders order2)
        {
            //Check for null and compare run-time types.
            if ((order1 == null) || (order2 == null) || !order1.GetType().Equals(order2.GetType()))
            {
                return false;
            }
            else
            {

                return (order1.Id == order2.Id);

            }
        }
        //הפונקציה מקבלת אנדוודואל ומשלוח ומוחקת את היצוג הראשון שלו שהיא מוצאת
        public void DeleteOrder(IndividualServices individual,Orders orders)
        {
            
            foreach (KeyValuePair<string, DeliveryPersonChromosome> chro in individual.chromosome)
            {
                Orders orders1 = chro.Value.ListOrders.Where(o => o.Id == orders.Id).FirstOrDefault();
                if (orders1!=null)
                {
                    chro.Value.ListOrders.Remove(orders1);
                    return;
                }

            }
        }
        //הפונקציה מקבלת משלוח ואינדוודואל ומשבצת אותו לשליח באופן רנדומלי
        public void DeliveryManRandomInlay(IndividualServices individual, Orders order)
        {

            //הגרלת לאיזה שליח לשבץ את המשלוח
            Random rand = new Random();
            int p = rand.Next(GENES_EMPLOYEE.Length);
            //הוספה לשליח שהוגרל
            individual.chromosome[GENES_EMPLOYEE[p].Id].ListOrders.Add(order);

        }




    }
}
