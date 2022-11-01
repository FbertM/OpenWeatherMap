using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication2.Interface;

namespace WebApplication2.Models
{
    public class APIConfig
    {
        public string APIKey { get; set; }
        public string APIUrl { get; set; }

    }

    public class WeatherModel : IWeather
    {
        public readonly IMemoryCache _cache;
        public (string, string) db;
        private readonly IHttpClientFactory _client;
        private APIConfig config;
        public string cache_id = "Weather_";

        public WeatherModel(IMemoryCache cache, IHttpClientFactory client, IConfiguration configuration)
        {
            _cache = cache;
            _client = client;
            this.config = configuration.GetSection("APIOpenWeather").Get<APIConfig>();
        }

        public WeatherDBModel getWeatherURI(string city)
        {
            return _cache.GetOrCreateAsync<WeatherDBModel>(cache_id + city, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                var response = default(WeatherDBModel);
                string endpoint = config.APIUrl;
                using (var client = _client.CreateClient())
                {
                    var res = await client.GetAsync(String.Format(endpoint, city, config.APIKey));
                    if (res.IsSuccessStatusCode)
                    {
                        var content = await res.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<WeatherDBModel>(content);
                    }
                    return response;
                }
            }).Result;
        }

        public WeatherDBModel LoadAPI(string city)
        {
            return getWeatherURI(city);
        }
    }
}

