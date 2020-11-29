using Kata.WebApi.Restful.MetaData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace Kata.WebApi.Restful.Controllers
{
    public class TripController : BaseController
    {
        public TripController(IOptions<AppSettings> appSettings, IWebHostEnvironment environment) : base(appSettings, environment)
        {
            AppSettings = appSettings;
            Environment = environment;
        }


        [HttpPost]
        [Route("UploadTrips")]
        public IActionResult UploadTrips()
        {
            try
            {    
                //----------------------------------------------
                // Read the File and save it in the File System
                // ---------------------------------------------

                // Set file name with current TimeStamp
                string prevFileName = DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";
                string fileName = Environment.WebRootPath + $@"\files" + $@"\{ prevFileName }";                  
                    
                // Save the file in File System
                using (FileStream fs = System.IO.File.Create(fileName))
                {                    
                    Request.Form.Files[0].CopyTo(fs);
                    fs.Flush();
                }

                // Read file
                string[] fileRead = System.IO.File.ReadAllLines(fileName);


                //----------------------------------------------
                //             Process the File
                // ---------------------------------------------

                // Add the new File and register new Drivers                
                TripUoW.TripRepository.AddFile(prevFileName);
                TripUoW.Save();

                // Add the new Drivers
                TripUoW.TripRepository.AddDrivers(fileRead, prevFileName);
                TripUoW.Save();

                // Add the new Trips
                TripUoW.TripRepository.AddTrips(fileRead, prevFileName);
                TripUoW.Save();

                // Get the Report
                var TripDto = TripUoW.TripRepository.GetTripsByDriver(prevFileName);

                // Send back the Report            
                return new ObjectResult(TripDto);

            }

            catch (Exception ex)
            {                
                // Exception handler                
                return BadRequest(ex);
            }

        }

    }
}
