using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Interface;

namespace WebApplication2.Controllers
{
    public class WeatherController : Controller
    {
        private ICountry _dbCountry;
        private IWeather _dbWeather;
        public WeatherController(ICountry _country, IWeather weather)
        {
            _dbCountry = _country;
            _dbWeather = weather;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult getListCountry()
        {
            return Ok(_dbCountry.getListCountry());
        }

        public IActionResult getCity(string country)
        {
            return Ok(_dbCountry.getListCity(country));
        }

        public IActionResult getWeather(string city)
        {
            if (_dbCountry.checkExistingCity(city))
            {
                return Ok(_dbWeather.LoadAPI(city));
            }
            else
                return BadRequest();
        }
    }
}
