using Kata.WebApp.UI.Models;
using Kata.WebApp.UI.MoldeViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Kata.WebApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SelectFile()
        {
             return new StatusCodeResult((int)HttpStatusCode.OK);
        }


        // Upload Trips
        [HttpPost]
        public async Task<IActionResult> UploadTrips()
        {
            // Declare dictionary to create pair values                      
            var formValues = new Dictionary<string, string>();

            // Set data Form
            formValues.Add("File", "Trips");                        
            HttpContent DictionaryItems = new FormUrlEncodedContent(formValues);            
            MultipartFormDataContent form = new MultipartFormDataContent();

            // Add parameters
            form.Add(DictionaryItems, "Parameters");

            // Get files
            if (Request.Form.Files.Count > 0)
            {
                foreach (var f in Request.Form.Files)
                {
                    // Add the file content to the form
                    form.Add(new StreamContent(f.OpenReadStream())
                    {
                        Headers =
                                {
                                    ContentLength = f.Length,
                                    ContentType = new MediaTypeHeaderValue(f.ContentType)
                                }
                    }, "File", f.FileName);
                }
            }

            // Create the htpp client
            var client = new HttpClient();                        
            var response = await client.PostAsync("http://localhost:52131/" + "UploadTrips", form);
           
            // Holder result
            var tripModelView = new TripModelView();                        

            // Verify the request was exceuted successfully
            if (response.IsSuccessStatusCode)
            {
                // Read the result
                var result = response.Content.ReadAsStringAsync().Result;

                // Format the result
                tripModelView = JsonConvert.DeserializeObject<TripModelView>(result);

                return View("Report", tripModelView);
            }
            else
            {
                return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });                
            }            
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
