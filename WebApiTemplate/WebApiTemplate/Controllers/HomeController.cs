using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApiTemplate.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            using(var fo = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/robot.txt")))
            {
                var allLine= fo.ReadToEnd();    
                Console.WriteLine(allLine);
            }

            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = $"Hello world at {DateTime.Now}<br>Check if swagger available <a href='/swagger'>Swagger UI</a>"
            };

        }
    }
}
