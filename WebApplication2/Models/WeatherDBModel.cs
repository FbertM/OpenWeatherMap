using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class WeatherDBModel
    {
        [JsonProperty("Coord")]
        public Location location { get; set; }
        [JsonProperty("dt")]
        public int time { get; set; }
        public Wind wind { get; set; }
        public long visibility { get; set; }
        [JsonProperty("clouds")]
        public SkyCondition skyCondition { get; set; }
        [JsonProperty("main")]
        public Humid humid { get; set; }
        

    }

    public class Humid
    {
        [JsonProperty("temp")]
        public double tempC { get; set; }
        public double tempF => tempC * 1.8 + 32;
        public double humidity { get; set; }
        public double pressure { get; set; }
        public double dew_point => (tempC - (100 - humidity) / 5);
    }
    public class Wind {
        public double speed { get; set; }
        public int deg { get; set; }
    }
    public class Location
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class SkyCondition
    {
       public int all { get; set; }
    }


}
