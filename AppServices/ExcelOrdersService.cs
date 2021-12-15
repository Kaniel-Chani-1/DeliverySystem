using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
//using EPPlusTest;
using OfficeOpenXml;
using System.IO;
//using LicenseContext = OfficeOpenXml.LicenseContext;
using Common.Models;
using Repositories.Models;
using Repositories;

namespace AppServices
{
   public class ExcelOrdersService:IExcelOrdersService
    {
        
        const int cclientId = 1;
        const int cfirstName = 2;
        const int clastName = 3;
        const int cphoneNumber1 = 4;
        const int cphoneNumber2 = 5;
        const int cmyCityId = 6;
        const int cmyStreet = 7;
        const int cmyBuldingNumber = 8;
        const int cmyEntranceNumber = 9;
        const int cmyApartmentNumber = 10;
        const int cCityId = 11;
        const int cStreet = 12;
        const int cBuldingNumber = 13;
        const int cEntranceNumber = 14;
        const int cApartmentNumber = 15;
        const int cEmail = 16;
        const int cCreditCardNumber = 17;
        const int cOrderDate = 18;
        const int cOrderTime = 19;
        const int cOrderWeight = 20;
        const int cPacking1 = 21;
        const int numPackings = 9;

        IDetailsOfTheContentsOfReservationRepository detailsOfTheContentsOfReservationRepository;
        IClientRepository clientRepository;
        IDifferentShipingAdressRepository differentShipingAdressRepository;
        IAlgorithmService algorithmServiceRepository;
        ICityRepository cityRepository;
        
        public ExcelOrdersService(IClientRepository clientRepository,
            IDetailsOfTheContentsOfReservationRepository detailsOfTheContentsOfReservationRepository,
            IDifferentShipingAdressRepository differentShipingAdressRepository,
             IAlgorithmService algorithmServiceRepository,
             ICityRepository cityRepository)
        {
            this.clientRepository = clientRepository;
            this.detailsOfTheContentsOfReservationRepository = detailsOfTheContentsOfReservationRepository;
            this.differentShipingAdressRepository = differentShipingAdressRepository;
            this.algorithmServiceRepository = algorithmServiceRepository;
            this.cityRepository = cityRepository;
        }
        public  void readExcel(string filePath, string nameWorkSheet)
        {
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var firstSheet = package.Workbook.Worksheets[nameWorkSheet];
                int rowCnt = firstSheet.Dimension.End.Row;
                int colCnt = firstSheet.Dimension.End.Column;

                //מבנה של כתובת בשביל בדיקה האם הכתובות זהות או לא
                Adress adress = new Adress();
                //מבנה של סוג אריזה
                PackingAmount packingAmount = new PackingAmount();
                //מבנה של רשימה של סוגי אריזות של הזמנה
                List<PackingAmount> listPackingAmount;
               
                for (int i = 2; i <= rowCnt; i++)
                {
                    //אתחול רשימת סוגי האריזות
                    listPackingAmount = new List<PackingAmount>();
                    //שליחת שם עיר לפונקציה שמחזירה את הקוד של שם העיר הזאת במאגר הערים
                    //אם העיר לא קימת במאגר ערים שלי אז אני מוסיפה אותה
                    int myCityId = cityRepository.GetCityCodeFromCityName(firstSheet.Cells[i, cmyCityId].Text);
                    //בדיקה אם הלקוח קים או שהוא לקוח חדש
                    //אם הלקוח הוא חדש אז נוסיף אותו למאגר הלקוחות
                    if (clientRepository.IsNewClient(firstSheet.Cells[i, cclientId].Text))
                    {
                        //הוספת לקוח חדש
                        clientRepository.Create(firstSheet.Cells[i, cclientId].Text,
                           firstSheet.Cells[i, cfirstName].Text,
                           firstSheet.Cells[i, clastName].Text,
                           firstSheet.Cells[i, cphoneNumber1].Text,
                           firstSheet.Cells[i, cphoneNumber2].Text,
                           myCityId,
                           firstSheet.Cells[i, cmyStreet].Text,
                           Convert.ToInt32(firstSheet.Cells[i, cmyBuldingNumber].Text),
                           Convert.ToInt32(firstSheet.Cells[i, cmyEntranceNumber].Text),
                           Convert.ToInt32(firstSheet.Cells[i, cmyApartmentNumber].Text),
                           firstSheet.Cells[i, cEmail].Text,
                           firstSheet.Cells[i, cCreditCardNumber].Text);
                      
                    }
                    
                    //אם הלקוח קים אז רק נעדכן את פרטיו
                    else
                    {
                        clientRepository.Update(firstSheet.Cells[i, cclientId].Text,
                           firstSheet.Cells[i, cfirstName].Text,
                           firstSheet.Cells[i, clastName].Text,
                           firstSheet.Cells[i, cphoneNumber1].Text,
                           firstSheet.Cells[i, cphoneNumber2].Text,
                            myCityId,
                             firstSheet.Cells[i, cmyStreet].Text,
                           Convert.ToInt32(firstSheet.Cells[i, cmyBuldingNumber].Text),
                           Convert.ToInt32(firstSheet.Cells[i, cmyEntranceNumber].Text),
                           Convert.ToInt32(firstSheet.Cells[i, cmyApartmentNumber].Text),
                           firstSheet.Cells[i, cEmail].Text,
                           firstSheet.Cells[i, cCreditCardNumber].Text);

                    }
                    OrderViewModel order = new OrderViewModel
                    {
                        IdClient = firstSheet.Cells[i,cclientId].Text,
                        OrderDate = Convert.ToDateTime(firstSheet.Cells[i,cOrderDate].Text),
                        OrderWeight = Convert.ToDouble(firstSheet.Cells[i,cOrderWeight].Text),
                        OrderTime = TimeSpan.Parse(firstSheet.Cells[i,cOrderTime].Text),
                        //Status = 1;



                    };

                    //קריאת סוגי אריזות
                    for (int j = 0; j < numPackings; j++)
                    {
                        if (Convert.ToInt32(firstSheet.Cells[i,cPacking1+j ].Text) != 0)
                        {
                            packingAmount.Amount = Convert.ToInt32(firstSheet.Cells[i,cPacking1+j].Text);
                            packingAmount.IdPackingType = j+1;
                            listPackingAmount.Add(packingAmount);
                        }
                    }
                    //הוספת הזמנה לטבלת הזמנות
                    //קביעת תאריך קבלת משלוח
                    //ההוספה כוללת הוספה לטבלת פרטי תכולת הזמנה
                     Orders order1  = algorithmServiceRepository.EndOfReceivingOfNewOrder(order, listPackingAmount);
                    detailsOfTheContentsOfReservationRepository.Insert(listPackingAmount, order1.Id);

                    //בדיקה האם יש להוסיף כתובת שונה או שכתובת ההזמנה ככתובת הלקוח
                    //שליחת שם העיר של כתובת לשליחה שתחזיר את מזהה של העיר הזו
                    int cityId= cityRepository.GetCityCodeFromCityName(firstSheet.Cells[i, cCityId].Text);
                    adress.IdCityAdress = cityId;
                    adress.StreetAdress = firstSheet.Cells[i,cStreet].Text;
                    adress.BuldingNumber = Convert.ToInt32(firstSheet.Cells[i,cBuldingNumber].Text);
                    adress.ApartmentNumber = Convert.ToInt32(firstSheet.Cells[i, cApartmentNumber].Text);
                    adress.EntranceNumber = Convert.ToInt32(firstSheet.Cells[i, cEntranceNumber].Text);
                    //אם הכתובות לא שוות
                    if (!clientRepository.IsClientASdressEqualAdress(clientRepository.GetById(order1.IdClient), adress))
                    {
                        
                       differentShipingAdressRepository.SetDiffrentShipingAdresss(adress, order1.Id);
                    }

                }



             
            }

        }
     
    }
    }
