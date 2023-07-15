using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices
{
  public  interface IExcelOrdersService
    {
        public void readExcel(string filePath, string nameWorkSheet);
    }
}
