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
    public class TeacherController : ControllerBase
    {
        public DbSchedule Db { get; }
        public TeacherController(DbSchedule db) => Db = db;

        /// <summary>
        /// отримати список викладачів
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> Get()
        {
            return await Db.Teachers.ToListAsync();
        }

        /// <summary>
        /// отримати викладача по номеру
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> Get(int id)
        {
            Teacher teacher = await Db.Teachers.FirstOrDefaultAsync(t => t.Id == id);
            if (teacher == null)
                return NotFound();

            return teacher;
        }

        /// <summary>
        /// додати нового викладача
        /// </summary>
        /// <param name="teacher"></param>
        [HttpPost]
        public async Task<ActionResult<Teacher>> Post(Teacher teacher)
        {
            if (teacher == null)
                return BadRequest();

            Db.Teachers.Add(teacher);
            await Db.SaveChangesAsync();
            return Ok(teacher);
        }

        /// <summary>
        /// оновити викладача
        /// </summary>
        /// <param name="teacher"></param>
        [HttpPut]
        public async Task<ActionResult<Teacher>> Put(Teacher teacher)
        {
            if (teacher == null)
                return BadRequest();

            if (!await Db.Teachers.AnyAsync(t => t.Id == teacher.Id))
                return NotFound();

            Db.Teachers.Update(teacher);
            await Db.SaveChangesAsync();
            return Ok(teacher);
        }

        /// <summary>
        /// видалити викладача по номеру
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Teacher>> Delete(int id)
        {
            Teacher teacher = await Db.Teachers.FirstOrDefaultAsync(t => t.Id == id);
            if (teacher == null)
                return NotFound();

            Db.Teachers.Remove(teacher);
            await Db.SaveChangesAsync();
            return Ok(teacher);
        }
    }
}
