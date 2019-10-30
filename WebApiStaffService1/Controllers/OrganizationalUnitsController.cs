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
    public class OrganizationalUnitsController : ControllerBase
    {
        private readonly EnterpriseStructDbContext _context;

        public OrganizationalUnitsController(EnterpriseStructDbContext context)
        {
            _context = context;
        }

        //// GET: api/OrganizationalUnits
        //[HttpGet]
        //public IEnumerable<OrganizationalUnit> GetOrganizationalUnits()
        //{
        //    return _context.OrganizationalUnits;
        //}

        [HttpGet]
        public async Task<IActionResult> GetOrganizationalUnits()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resp =
              await _context.OrganizationalUnits
          .Select(o => new OrganizationalUnitView
          {
              Id = o.Id,
              Name = o.Name,              
              IntegretionKey = o.IntegretionKey,
              //CreateOn = p.CreateOn,
              //ModifyOn = p.ModifyOn,
              State = o.State,

          })
           .ToListAsync();

            if (resp == null)
            {
                return NotFound();
            }

            return Ok(resp);


        }


        // GET: api/OrganizationalUnits/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganizationalUnit([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var organizationalUnit = await _context.OrganizationalUnits.FindAsync(id);

            if (organizationalUnit == null)
            {
                return NotFound();
            }

            return Ok(organizationalUnit);
        }

        // PUT: api/OrganizationalUnits/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganizationalUnit([FromRoute] Guid id, [FromBody] OrganizationalUnit organizationalUnit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != organizationalUnit.Id)
            {
                return BadRequest();
            }

            _context.Entry(organizationalUnit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationalUnitExists(id))
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

        // POST: api/OrganizationalUnits
        [HttpPost]
        public async Task<IActionResult> PostOrganizationalUnit([FromBody] OrganizationalUnit organizationalUnit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.OrganizationalUnits.Add(organizationalUnit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganizationalUnit", new { id = organizationalUnit.Id }, organizationalUnit);
        }

        // DELETE: api/OrganizationalUnits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizationalUnit([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var organizationalUnit = await _context.OrganizationalUnits.FindAsync(id);
            if (organizationalUnit == null)
            {
                return NotFound();
            }

            _context.OrganizationalUnits.Remove(organizationalUnit);
            await _context.SaveChangesAsync();

            return Ok(organizationalUnit);
        }

        private bool OrganizationalUnitExists(Guid id)
        {
            return _context.OrganizationalUnits.Any(e => e.Id == id);
        }
    }
}