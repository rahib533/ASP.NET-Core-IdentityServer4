using API_2.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CarsController : ControllerBase
    {
        public static List<Car> cars = new List<Car>
        {
            new Car{Id=1, Name="Nissan", Price=5,IsActive=true },
            new Car{Id=2, Name="BMW", Price=2,IsActive=true },
            new Car{Id=3, Name="Toyota", Price=3,IsActive=true }
        };
        [HttpGet]
        public IActionResult GetCars()
        {
            return Ok(cars);
        }
    }
}
