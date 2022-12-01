using System;
using System.Collections.Generic;
using System.Linq;
using automaat.Models;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;

namespace automaat.Models
{
    public class AutomaatConfig
    {
        [JsonProperty("pricewater")]
        public decimal PriceWater { get; set; }
        [JsonProperty("pricecola")]
        public decimal PriceCola { get; set; }
        [JsonProperty("pricefruitsap")]
        public decimal PriceFruitsap { get; set; }

    }
}