using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CheuwoAPI.Models
{
    public class Offer
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [JsonIgnore]
        public User Creator { get; set; }

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
