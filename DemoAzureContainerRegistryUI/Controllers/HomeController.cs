using DemoAzureContainerRegistryUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DemoAzureContainerRegistryUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            string APIURL = _configuration["APIURL"];
            try
            {
                using(HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(APIURL);
                    var response = await client.GetAsync("weatherforecast");
                    if (response.IsSuccessStatusCode)
                    {
                        string resbody = await response.Content.ReadAsStringAsync();
                        ViewBag.Message = resbody;
                    }
                    else
                    {
                        ViewBag.Message = "Error in project";
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}