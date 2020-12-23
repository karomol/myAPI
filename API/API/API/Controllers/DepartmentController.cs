using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KPI_Schedule.Models;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        public DbSchedule Db { get; }
        public DepartmentController(DbSchedule db) => Db = db;

        /// <summary>
        /// отримати список кафедр
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> Get()
        {
            return await Db.Departments.Select(t => new
            {
                id = t.Id,
                facultyName = t.Faculty.Name,
                name = t.Name,
                shortName = t.ShortName,
            }).ToListAsync();
        }

        /// <summary>
        /// отримати кафедру по номеру
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            var department = await Db.Departments.Select(t => new
            {
                id = t.Id,
                facultyId = t.FacultyId,
                facultyName = t.Faculty.Name,
                name = t.Name,
                shortName = t.ShortName,
            }).FirstOrDefaultAsync(t => t.id == id);

            if (department == null)
                return NotFound();

            return department;
        }

        /// <summary>
        /// додати нову кафедру
        /// </summary>
        /// <param name="department"></param>
        [HttpPost]
        public async Task<ActionResult<Department>> Post(Department department)
        {
            if (department == null)
                return BadRequest();

            Db.Departments.Add(department);
            await Db.SaveChangesAsync();
            return Ok(department);
        }

        /// <summary>
        /// оновити кафедру
        /// </summary>
        /// <param name="department"></param>
        [HttpPut]
        public async Task<ActionResult<Department>> Put(Department department)
        {
            if (department == null)
                return BadRequest();

            if (!await Db.Departments.AnyAsync(t => t.Id == department.Id))
                return NotFound();

            Db.Departments.Update(department);
            await Db.SaveChangesAsync();
            return Ok(department);
        }

        /// <summary>
        /// видалити кафедру по номеру
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> Delete(int id)
        {
            Department department = await Db.Departments.FirstOrDefaultAsync(t => t.Id == id);
            if (department == null)
                return NotFound();

            Db.Departments.Remove(department);
            await Db.SaveChangesAsync();
            return Ok(department);
        }
    }
}
