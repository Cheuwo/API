using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CheuwoAPI.Models.Offers
{
    public class OfferDTO
    {
        public int ID { get; set; }

        [JsonPropertyName("creator_email")]
        public string CreatorEmail { get; set; }

        public string Description { get; set; }

        public float Rating { get; set; }

        public decimal Price { get; set; }
    }
}
