using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace InternExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherAPI : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var client = new RestClient("http://api.openweathermap.org/data/2.5/group?id=1580578,1581129,1581297,1581188,1587923&units=metric&appid=91b7466cc755db1a94caf6d86a9c788a");
            RestRequest request = new RestRequest();
            RestResponse response = await client.ExecuteAsync(request);
            dynamic result = JsonConvert.DeserializeObject(response.Content);
            dynamic final;
            foreach (var data in result["list"])
            {
                Console.WriteLine("cityId: " + data["id"]);
                Console.WriteLine("cityName: " + data["name"]);
                foreach (var weatherdata in data["weather"])
                {
                    Console.WriteLine("weatherMain: " + weatherdata["main"]);
                    Console.WriteLine("weatherDescription: " + weatherdata["description"]);
                }
                //foreach (var maindata in data["main"])
                //{
                //    Console.WriteLine("mainTemp: " + maindata["temp"]);
                //    Console.WriteLine("mainHumidity: " + maindata["humidity"]);
                //}
            }
            return Ok(response.Content);

        }
    }
}