using System;
using Newtonsoft.Json;

namespace automaat.Models
{
    public class OrderMessage
    {
        [JsonProperty("id")]
        public string OrderId { get; set; }
        [JsonProperty("product")]
        public string Product { get; set; }
        [JsonProperty("amount")]
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string Location { get; set; }
    }
}