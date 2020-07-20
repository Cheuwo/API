using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CheuwoAPI.Models.Offers
{
    public class CreateOfferDTO
    {
        public int ID { get; set; }

        [JsonPropertyName("creator_id")]
        [Required]
        public int CreatorID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public string Description { get; set; }

        public float Rating { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
