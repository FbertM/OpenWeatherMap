using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;
using WebApplication2.Interface;
using WebApplication2;
using WebApplication2.Controllers;
using System.Linq;
using WebApplication2.Models;
using Moq;

namespace TestProject1
{
    public class Tests
    {
        private DependencyResolver _serviceProvider;
        private ICountry _countryDB;
        private MockWeather _mock;
        private WeatherController controller;

        [SetUp]
        public void Setup()
        {
            var webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .Build();
            _serviceProvider = new DependencyResolver(webHost);
            _countryDB = _serviceProvider.GetService<ICountry>();
            _mock = new MockWeather(_serviceProvider);

        }

      


        [Test]
        public void GetCountry()
        {
            var res = _countryDB.getListCountry();
            Assert.AreEqual(true, res.Any());
        }

        [Test]
        [TestCase("United Kingdom")]
        [TestCase("China")]
        public void GetCity(string country)
        {
            var res = _countryDB.getListCity(country);
            Assert.AreEqual(true, res.Any());
        }
        [Test]
        [TestCase("Surabaya")]
        [TestCase("Jakarta")]
        public void GetWeather(string city)
        {
            var res = _mock.GetWeather(city);
            var tmp = res.humid.tempC * 1.8 + 32;
            Assert.AreEqual(tmp, res.humid.tempF);
            Assert.AreNotEqual(null, res);
        }
    }
}