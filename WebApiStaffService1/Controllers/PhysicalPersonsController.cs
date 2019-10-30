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
    public class PhysicalPersonsController : ControllerBase
    {
        private readonly EnterpriseStructDbContext _context;

        public PhysicalPersonsController(EnterpriseStructDbContext context)
        {
            _context = context;
        }

        //// GET: api/PhysicalPersons
        //[HttpGet]
        //public IEnumerable<PhysicalPerson> GetPhysicalPersons()
        //{
        //    return _context.PhysicalPersons;
        //}


        [HttpGet]
        public async Task<IActionResult> GetPhysicalPersons()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resp =
              await _context.PhysicalPersons
          .Select(p => new PhysicalPersonView
          {
              Id = p.Id,
              Name = p.Name,
              Surname = p.Surname,
              Patronymic = p.Patronymic,
              Photo = p.Photo,
              IntegretionKey = p.IntegretionKey,
              Birthdate = p.Birthdate,
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



        // GET: api/PhysicalPersons/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhysicalPerson([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var physicalPerson = await _context.PhysicalPersons.FindAsync(id);

            if (physicalPerson == null)
            {
                return NotFound();
            }

            return Ok(physicalPerson);
        }

        // PUT: api/PhysicalPersons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhysicalPerson([FromRoute] Guid id, [FromBody] PhysicalPerson physicalPerson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != physicalPerson.Id)
            {
                return BadRequest();
            }

            _context.Entry(physicalPerson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhysicalPersonExists(id))
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

        // POST: api/PhysicalPersons
        [HttpPost]
        public async Task<IActionResult> PostPhysicalPerson([FromBody] PhysicalPerson physicalPerson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PhysicalPersons.Add(physicalPerson);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhysicalPerson", new { id = physicalPerson.Id }, physicalPerson);
        }

        // DELETE: api/PhysicalPersons/5
        //[HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhysicalPerson([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var physicalPerson = await _context.PhysicalPersons.FindAsync(id);
            if (physicalPerson == null)
            {
                return NotFound();
            }

            _context.PhysicalPersons.Remove(physicalPerson);
            await _context.SaveChangesAsync();

            return Ok(physicalPerson);
        }

        private bool PhysicalPersonExists(Guid id)
        {
            return _context.PhysicalPersons.Any(e => e.Id == id);
        }
    }
}