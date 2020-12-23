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
    public class DisciplineController : ControllerBase
    {
        public DbSchedule Db { get; }
        public DisciplineController(DbSchedule db) => Db = db;

        /// <summary>
        /// отримати список предметів.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Discipline>>> Get()
        {
            return await Db.Disciplines.ToListAsync();
        }

        /// <summary>
        /// отримати предмет по номеру
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Discipline>> Get(int id)
        {
            Discipline discipline = await Db.Disciplines.FirstOrDefaultAsync(t => t.Id == id);
            if (discipline == null)
                return NotFound();

            return discipline;
        }

        /// <summary>
        /// додати новий предмет
        /// </summary>
        /// <param name="discipline"></param>
        [HttpPost]
        public async Task<ActionResult<Discipline>> Post(Discipline discipline)
        {
            if (discipline == null)
                return BadRequest();

            Db.Disciplines.Add(discipline);
            await Db.SaveChangesAsync();
            return Ok(discipline);
        }

        /// <summary>
        /// оновити предмет
        /// </summary>
        /// <param name="discipline"></param>
        [HttpPut]
        public async Task<ActionResult<Discipline>> Put(Discipline discipline)
        {
            if (discipline == null)
                return BadRequest();

            if (!await Db.Disciplines.AnyAsync(t => t.Id == discipline.Id))
                return NotFound();

            Db.Disciplines.Update(discipline);
            await Db.SaveChangesAsync();
            return Ok(discipline);
        }

        /// <summary>
        /// видалити предмет по номеру
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Discipline>> Delete(int id)
        {
            Discipline discipline = await Db.Disciplines.FirstOrDefaultAsync(t => t.Id == id);
            if (discipline == null)
                return NotFound();

            Db.Disciplines.Remove(discipline);
            await Db.SaveChangesAsync();
            return Ok(discipline);
        }
    }
}
