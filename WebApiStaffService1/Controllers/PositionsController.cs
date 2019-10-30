using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiStaffService1.Data;
using WebApiStaffService1.Data.Models;
using WebApiStaffService1.ViewModels;

namespace WebApiStaffService1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly EnterpriseStructDbContext _context;

        public PositionsController(EnterpriseStructDbContext context)
        {
            _context = context;
        }

        //// GET: api/Positions
        //[HttpGet]
        //public IEnumerable<Position> GetPositions()
        //{
        //    return _context.Positions;
        //}

        [HttpGet]
        public async Task<IActionResult> GetPositions()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resp =
              await _context.Positions
          .Select(p => new PositionView
          {
              Id = p.Id,
              Name = p.Name,
              IntegretionKey = p.IntegretionKey,
              //CreateOn = p.CreateOn,
              //ModifyOn = p.ModifyOn,
              State = p.State,

          })
           .ToListAsync();

            if (resp == null)
            {
                return NotFound();
            }

            return Ok(resp);


        }



        // GET: api/Positions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPosition([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var position = await _context.Positions.FindAsync(id);

            if (position == null)
            {
                return NotFound();
            }

            return Ok(position);
        }



        // PUT: api/Positions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosition([FromRoute] Guid id, [FromBody] Position position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != position.Id)
            {
                return BadRequest();
            }

            _context.Entry(position).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionExists(id))
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

        // POST: api/Positions
        [HttpPost]
        public async Task<IActionResult> PostPosition([FromBody] Position position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Positions.Add(position);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPosition", new { id = position.Id }, position);
        }

        // DELETE: api/Positions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }

            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();

            return Ok(position);
        }

        private bool PositionExists(Guid id)
        {
            return _context.Positions.Any(e => e.Id == id);
        }
    }
}