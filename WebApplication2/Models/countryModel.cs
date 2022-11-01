using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Interface;

namespace WebApplication2.Models
{
    public class CountryModel :ICountry
    {
        private IMemoryCache _countryCache  ;

        public CountryModel(IMemoryCache _cache)
        {
            _countryCache = _cache;
        }
        private List<CountryDBModel> loadJSONData()
        {
            return _countryCache.GetOrCreate("JSONData", expr =>
            {
                expr.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
                string json = File.ReadAllText("./Models/countries.json");
                var objList = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(json);
                List<CountryDBModel> model = new List<CountryDBModel>();
                foreach (var obj in objList)
                {
                    model.Add(new CountryDBModel { country = obj.Key, city = obj.Value });
                }
                return model;
            });
        }
        public List<string> getListCity(string _country)
        {
            var data = loadJSONData();
            return CityModel.getFetchData(data, _country);
        }

        public List<string> getListCountry()
        {
            var data = loadJSONData();
            return data.Select(o => o.country).ToList();
        }

        public bool checkExistingCity(string city)
        {
            var data = loadJSONData();
            var countCity  = data.Where(o => o.city.Contains(city)).Count();
            if (countCity > 0) return true;
            return false;
        }

    }
}
