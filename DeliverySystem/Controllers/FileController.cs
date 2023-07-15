using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppServices;
using Repositories;
using Common;

namespace DeliverySystem.Controllers
{
   
    public class FileController : ApiBaseController
    {
        const string nameWorkSheet = "1";
        IExcelOrdersService excelOrdersService;
        public FileController(IExcelOrdersService excelOrdersService)
        {
            this.excelOrdersService = excelOrdersService;
        }
        [HttpPost("single")]
        public async Task<IActionResult> UploadSingleFileAsync(IFormFile formFile)
        {
            //check if file is null...
            long size = formFile == null ? 0 : formFile.Length;
            string path = @"C:\Temp\";

            if (formFile?.Length > 0)
            {
                //YOU decide where to save the file!
                //the most important part is the file extension
                //Don't rely on or trust the FileName property without validation.
                //ONLY EXAMPLE, DO NOT USE IN REAL WORLD
                var filePath = path + formFile.FileName;//Path.GetTempFileName();

                using (var stream = System.IO.File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                }
                //יש לבדוק האם זה קובץ מסוג אקסל
                //הפעלת פונקצית קריאת קובץ אקסל
                excelOrdersService.readExcel(path + formFile.FileName, nameWorkSheet);
            }

            return Ok(new { count = 1, size });
        }
       

    }
}
