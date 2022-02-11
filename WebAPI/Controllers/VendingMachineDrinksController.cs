#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DB;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendingMachineDrinksController : ControllerBase
    {
        private readonly DrinkSaleContext _context;

        public VendingMachineDrinksController(DrinkSaleContext context)
        {
            _context = context;
        }

        // GET: api/VendingMachineDrinks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendingMachineDrink>>> GetVendingMachineDrinks()
        {
            return await _context.VendingMachineDrinks.ToListAsync();
        }

        // GET: api/VendingMachineDrinks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VendingMachineDrink>> GetVendingMachineDrink(int id)
        {
            var vendingMachineDrink = await _context.VendingMachineDrinks.FindAsync(id);

            if (vendingMachineDrink == null)
            {
                return NotFound();
            }

            return vendingMachineDrink;
        }

        // PUT: api/VendingMachineDrinks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendingMachineDrink(int id, VendingMachineDrink vendingMachineDrink)
        {
            if (id != vendingMachineDrink.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendingMachineDrink).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendingMachineDrinkExists(id))
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

        // POST: api/VendingMachineDrinks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VendingMachineDrink>> PostVendingMachineDrink(VendingMachineDrink vendingMachineDrink)
        {
            _context.VendingMachineDrinks.Add(vendingMachineDrink);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendingMachineDrink", new { id = vendingMachineDrink.Id }, vendingMachineDrink);
        }

        // DELETE: api/VendingMachineDrinks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendingMachineDrink(int id)
        {
            var vendingMachineDrink = await _context.VendingMachineDrinks.FindAsync(id);
            if (vendingMachineDrink == null)
            {
                return NotFound();
            }

            _context.VendingMachineDrinks.Remove(vendingMachineDrink);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendingMachineDrinkExists(int id)
        {
            return _context.VendingMachineDrinks.Any(e => e.Id == id);
        }
    }
}
