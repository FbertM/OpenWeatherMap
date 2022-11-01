using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class CityModel
    {
        [HttpGet]
        public static List<string> getFetchData(List<CountryDBModel> cityList, string country)
        {
            return (List<string>)cityList.Where(o => o.country == country).Select(o => o.city).FirstOrDefault();
        }

    }
}
