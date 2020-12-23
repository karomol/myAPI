using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KPI_Schedule.Models;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        public DbSchedule Db { get; }
        public FacultyController(DbSchedule db) => Db = db;

        /// <summary>
        /// Повертає список факультетів.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Faculty>>> Get()
        {
            return await Db.Faculties.ToListAsync();
        }

        /// <summary>
        /// Повертає факультет по заданому id.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Faculty>> Get(int id)
        {
            Faculty faculty = await Db.Faculties.FirstOrDefaultAsync(t => t.Id == id);
            if (faculty == null)
                return NotFound();

            return faculty;
        }

        /// <summary>
        /// Додає новий факультет до бази даних.
        /// </summary>
        /// <param name="faculty"></param>
        [HttpPost]
        public async Task<ActionResult<Faculty>> Post(Faculty faculty)
        {
            if (faculty == null)
                return BadRequest();

            Db.Faculties.Add(faculty);
            await Db.SaveChangesAsync();
            return Ok(faculty);
        }

        /// <summary>
        /// Оновлює існуючий факультет.
        /// </summary>
        /// <param name="faculty"></param>
        [HttpPut]
        public async Task<ActionResult<Faculty>> Put(Faculty faculty)
        {
            if (faculty == null)
                return BadRequest();

            if (!await Db.Faculties.AnyAsync(t => t.Id == faculty.Id))
                return NotFound();

            Db.Faculties.Update(faculty);
            await Db.SaveChangesAsync();
            return Ok(faculty);
        }

        /// <summary>
        /// Видаляє факультет по заданому id.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Faculty>> Delete(int id)
        {
            Faculty faculty = await Db.Faculties.FirstOrDefaultAsync(t => t.Id == id);
            if (faculty == null)
                return NotFound();

            Db.Faculties.Remove(faculty);
            await Db.SaveChangesAsync();
            return Ok(faculty);
        }
    }
}
