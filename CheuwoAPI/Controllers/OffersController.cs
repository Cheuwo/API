using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CheuwoAPI.Models;
using CheuwoAPI.Models.Offers;
using CheuwoAPI.Helpers;

namespace CheuwoAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly ApiContext _context;

        public OffersController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Offers
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OfferDTO>>> GetOffer(ListOffersDTO model)
        {
            var dispOffers = new List<OfferDTO>();
            var offers = await _context.Offer.Skip(model.Offset).Take(model.Count).ToListAsync();
            foreach(Offer offer in offers)
            {
                dispOffers.Add(new OfferDTO { ID = offer.ID, CreatorEmail = offer.Creator.Email, Description = offer.Description, Rating = offer.Rating, Price = offer.Price });
            }
            return dispOffers;
        }

        // GET: api/Offers/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<OfferDTO>> GetOffer(int id)
        {
            var offer = await _context.Offer.FindAsync(id);

            if (offer == null)
            {
                return NotFound();
            }

            return new OfferDTO { ID = offer.ID, CreatorEmail = offer.Creator.Email, Description = offer.Description, Rating = offer.Rating, Price = offer.Price };
        }

        // PUT: api/Offers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutOffer(int id, Offer offer)
        {
            if (id != offer.ID)
            {
                return BadRequest();
            }

            _context.Entry(offer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfferExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Offers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Offer>> PostOffer(CreateOfferDTO model)
        {
            var user = (User) HttpContext.Items["User"];
            var offer = new Offer { Creator = user, Description = model.Description, Price = model.Price, Rating = model.Rating };

            _context.Offer.Add(offer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOffer", new { id = offer.ID }, 
                new OfferDTO { ID = offer.ID, CreatorEmail = offer.Creator.Email, Description = offer.Description, Rating = offer.Rating, Price = offer.Price });
        }

        // DELETE: api/Offers/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<OfferDTO>> DeleteOffer(int id)
        {
            var offer = await _context.Offer.FindAsync(id);
            if (offer == null)
            {
                return NotFound();
            }

            _context.Offer.Remove(offer);
            await _context.SaveChangesAsync();

            return new OfferDTO { ID = offer.ID, CreatorEmail = offer.Creator.Email, Description = offer.Description, Rating = offer.Rating, Price = offer.Price };
        }

        private bool OfferExists(int id)
        {
            return _context.Offer.Any(e => e.ID == id);
        }
    }
}
