using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using WebApplication2.Interface;
using WebApplication2.Models;

namespace TestProject1
{
    class DependencyResolver
    {
        private readonly IWebHost _webHost;
        public DependencyResolver(IWebHost webHost)
        {
            _webHost= webHost;
        }
        public T GetService<T>()
        {
            using (var serviceScope = _webHost.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var scopedService = services.GetRequiredService<T>();
                    return scopedService;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            };
        }

    }
    class MockWeather
    {
        IWeather _weather;
        public MockWeather(DependencyResolver _dependency)
        {
            _weather = _dependency.GetService<IWeather>();
        }

        public WeatherDBModel GetWeather(string city)
        {
            try
            {
                return _weather.LoadAPI(city);
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException.GetType() == typeof(HttpRequestException))
                    return new WeatherDBModel();
                throw;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }

}
