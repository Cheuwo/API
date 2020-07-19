using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CheuwoAPI.Models
{
    public class User : IdentityUser
    {
        [InverseProperty(nameof(Offer.Creator))]
        [JsonIgnore]
        public virtual ICollection<Offer> Offers { get; set; }

    }
}
