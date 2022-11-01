using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Interface
{
    public interface ICountry
    {
        List<string> getListCountry();
        List<string> getListCity(string _country);
        bool checkExistingCity(string city);
    }

    public interface IWeather
    {
        WeatherDBModel LoadAPI(string city);
    }
}
