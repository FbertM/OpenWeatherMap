using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;
using WebApplication2.Interface;
using WebApplication2;
using WebApplication2.Controllers;
using WebApplication2.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;

namespace TestProject1
{
    public class Tests
    {
        private DependencyResolver _serviceProvider;
        private ICountry _countryDB;
        private IWeather _weatherDB;
        private MockWeather _mock;
        private WeatherController controller;
        private Mock<IWeather> _mockRepo;

        [SetUp]
        public void Setup()
        {
            var webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .Build();
            _serviceProvider = new DependencyResolver(webHost);
            _countryDB = _serviceProvider.GetService<ICountry>();
            _weatherDB = _serviceProvider.GetService<IWeather>();
            _mock = new MockWeather(_serviceProvider);
            _mockRepo = new Mock<IWeather>();
            controller = new WeatherController(_countryDB, _mockRepo.Object);
        }

        [Test]
        [TestCase("Jepang")]
        public void TestLoadWeatherWithException(string city)
        {
            _mockRepo.Setup(repo => repo.LoadAPI(city))
        .Returns(new WeatherDBModel());
            var weather = controller.getWeather(city);
            System.Console.Write(weather);
            Assert.IsInstanceOf<BadRequestResult>(weather);
        }

        [Test]
        [TestCase("Kandahar")]
        public void TestLoadWeather(string city)
        {
            _mockRepo.Setup(repo => repo.LoadAPI(city))
        .Returns(new WeatherDBModel());
            var weather = controller.getWeather(city);
            System.Console.Write(weather);
            Assert.IsInstanceOf<OkObjectResult>(weather);
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
            Location loc = new Location() ;
            loc.lat = 2.11;
            loc.lon = 1.5;
            Humid hum = new Humid();
            hum.tempC = 30;

            _mockRepo.Setup(repo => repo.LoadAPI(city))
        .Returns(new WeatherDBModel { location = loc,humid = hum });
            var res = _mockRepo.Object.LoadAPI(city);
            var tmp = res.humid.tempC * 1.8 + 32;
            Assert.AreEqual(res.humid.tempC, 30);
            Assert.AreEqual(tmp, res.humid.tempF);
            Assert.AreNotEqual(null, res);
        }
    }
}