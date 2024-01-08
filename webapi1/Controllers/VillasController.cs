// Controllers/VillasController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi1.Models;


[Route("api/[controller]")]
[ApiController]
public class VillasController : ControllerBase
{
    private readonly VillaContext _context;

    public VillasController(VillaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Villa>>> GetVillas()
    {
        return await _context.Villas.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Villa>> GetVilla(int id)
    {
        var villa = await _context.Villas.FindAsync(id);

        if (villa == null)
        {
            return NotFound();
        }

        return villa;
    }

    [HttpPost]
    public async Task<ActionResult<Villa>> PostVilla(Villa villa)
    {
        _context.Villas.Add(villa);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVilla), new { id = villa.Id }, villa);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutVilla(int id, Villa villa)
    {
        if (id != villa.Id)
        {
            return BadRequest();
        }

        _context.Entry(villa).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!VillaExists(id))
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVilla(int id)
    {
        var villa = await _context.Villas.FindAsync(id);
        if (villa == null)
        {
            return NotFound();
        }

        _context.Villas.Remove(villa);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool VillaExists(int id)
    {
        return _context.Villas.Any(e => e.Id == id);
    }
}
