using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiStaffService1.Data;
using WebApiStaffService1.ViewModels;

namespace WebApiStaffService1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("EnterpriseStructPolicy")]
    public class EmployeesController : ControllerBase
    {
        private readonly EnterpriseStructDbContext _context;

        public EmployeesController(EnterpriseStructDbContext context)
        {
            _context = context;
        }

        #region GET
        // // GET: api/Employees
        // [HttpGet]
        //// public IEnumerable<TestEmpoleeView> GetEmployees()
        // public IEnumerable<object> GetEmployees()
        // {
        //     //var ret = _context.Employees
        //     //.Include(p => p.Position)
        //     //.Include(o => o.OrganizationalUnit)
        //     //.Include(ph => ph.PhysicalPerson)
        //     //.Select(e => new TestEmpoleeView
        //     //{
        //     //    Id = e.Id,
        //     //    PersonName = e.PhysicalPerson.Name,
        //     //    CreateOn = e.CreateOn,
        //     //    UnitName = e.OrganizationalUnit.Name,
        //     //    PositionName = e.Position.Name,
        //     //})
        //     //.ToList();
        //     //return ret;

        //     //return _context.Employees
        //     //        .Include(a => a.Position)
        //     //        .Include(a => a.OrganizationalUnit)
        //     //        .Include(a => a.PhysicalPerson).
        //     //        Select(e => new { e.Id, })
        //     //        .ToList();

        //     var resp =
        //        //return 
        //        _context.Employees
        //    //.Include(a => a.PositionId)
        //    //.Include(a => a.OrganizationalUnitId)
        ////    //.Include(a => a.PhysicalPersonId)
        //    .Select(e => new //Employee
        //     {
        //        Id = e.Id,
        //        //PhysicalPersonId = e.PhysicalPersonId,
        //        PhysicalPersonName = e.PhysicalPerson.Name,
        //        OrganizationalUnitId = e.OrganizationalUnitId,
        //        PositionId = e.PositionId,
        //        State = e.State,
        //     })

        //      .ToList();
        //     //return Json(resp);
        //     return (resp);

        //     //return _context.Employees;

        // }


        // GET: api/Employees

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resp =
              await _context.Employees
          .Select(e => new EmployeeView
          {
              Id = e.Id,
              PhysicalPersonId = e.PhysicalPersonId,
              OrganizationalUnitId = e.OrganizationalUnitId,
              PositionId = e.PositionId,
              //PhysicalPersonName = e.PhysicalPerson.Name,
              IntegretionKey = e.IntegretionKey,
              Employmentdate = e.Employmentdate,
              //CreateOn = e.CreateOn,
              //ModifyOn = e.ModifyOn,
              //OrganizationalUnitName = e.OrganizationalUnit.Name,
              //e.OrganizationalUnitId,
              //e.PositionId,
              State = e.State,
          })
           .ToListAsync();

            if (resp == null)
            {
                return NotFound();
            }

            return Ok(resp);

            //var employee = await _context.Employees.FindAsync(id);

            //var resp =
            //    //return 
            //    await _context.Employees
            //.Select(e => new //Employee
            //{
            //    e.Id,
            //    //PhysicalPersonId = e.PhysicalPersonId,
            //    PhysicalPersonName = e.PhysicalPerson.Name,
            //    OrganizationalUnitName = e.OrganizationalUnit.Name,
            //    e.OrganizationalUnitId,
            //    e.PositionId,
            //    e.State,
            //})
            // .ToListAsync();

        }


        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var employee = await _context.Employees.FindAsync(id);

            var employee = await _context.Employees
                //.Include(i => i.Position)
                //.Include(i => i.OrganizationalUnit)
                //.Include(i => i.PhysicalPerson)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // GET api/patients/3/PhysicalPerson
        //[HttpGet("{id}/PhysicalPerson")]
        //[Route("{id}/PhysicalPerson")]
        //[HttpGet]
        [HttpGet("{id}/PhysicalPerson")]
        public async Task<IActionResult> GetPhysicalPersons([FromRoute] Guid id)
        {
            var employee = await _context.Employees
               .Include(m => m.PhysicalPerson)
              .FirstOrDefaultAsync(i => i.Id == id);

            if (employee == null)
                return NotFound();

            return Ok(employee.PhysicalPerson);
        }

        // GET api/employee/3/Position
        [HttpGet("{id}/Position")]
        public async Task<IActionResult> GetPosition(Guid id)
        {
            var employee = await _context.Employees
              .Include(m => m.Position)
              .FirstOrDefaultAsync(i => i.Id == id);

            if (employee == null)
                return NotFound();

            return Ok(employee.Position);
        }

        #endregion

        #region PUT
        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee([FromRoute] Guid id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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
        #endregion

        #region POST
        // POST: api/Employees
        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);


            //Проверка на уже созданных ID

        }


        #endregion

        #region Delite
        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(employee);
        }
        #endregion

        private bool EmployeeExists(Guid id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

    }
}