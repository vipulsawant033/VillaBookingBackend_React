// Controllers/VillasController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi1.Models;


[Route("api/[controller]")]
[ApiController]
public class VillaBarChartController : ControllerBase
{
    private readonly VillaContext _context;

    public VillaBarChartController(VillaContext context)
    {
        _context = context;
    }

    [HttpGet("villaBarChartData")]
    public IActionResult GetVillaBarChartData()
    {
        var data = _context.VillaBarCharts.ToList();
        return Ok(data);
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<VillaBarChart>>> GetVillas()
    {
        return await _context.VillaBarCharts.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VillaBarChart>> GetVilla(int id)
    {
        var villa = await _context.VillaBarCharts.FindAsync(id);

        if (villa == null)
        {
            return NotFound();
        }

        return villa;
    }

    [HttpPost]
    public async Task<ActionResult<VillaBarChart>> PostVilla(VillaBarChart villa)
    {
        _context.VillaBarCharts.Add(villa);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVilla), new { id = villa.Id }, villa);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutVilla(int id, VillaBarChart villa)
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
        var villa = await _context.VillaBarCharts.FindAsync(id);
        if (villa == null)
        {
            return NotFound();
        }

        _context.VillaBarCharts.Remove(villa);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool VillaExists(int id)
    {
        return _context.VillaBarCharts.Any(e => e.Id == id);
    }
}
