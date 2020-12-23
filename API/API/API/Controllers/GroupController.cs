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
    public class GroupController : ControllerBase
    {
        public DbSchedule Db { get; }
        public GroupController(DbSchedule db) => Db = db;

        /// <summary>
        /// отримати список груп
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> Get()
        {
            return await Db.Groups.Select(t => new
            {
                id = t.Id,
                departmentName = t.Department.Name,
                name = t.Name,
                course = t.Course,
            }).ToListAsync();
        }

        /// <summary>
        /// отримати групу по номеру
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            var group = await Db.Groups.Select(t => new
            {
                id = t.Id,
                departmentId = t.DepartmentId,
                departmentName = t.Department.Name,
                name = t.Name,
                course = t.Course,
            }).FirstOrDefaultAsync(t => t.id == id);
            if (group == null)
                return NotFound();

            return group;
        }

        /// <summary>
        /// додати нову групу 
        /// </summary>
        /// <param name="group"></param>
        [HttpPost]
        public async Task<ActionResult<Group>> Post(Group group)
        {
            if (group == null)
                return BadRequest();

            Db.Groups.Add(group);
            await Db.SaveChangesAsync();
            return Ok(group);
        }

        /// <summary>
        /// оновити групу
        /// </summary>
        /// <param name="group"></param>
        [HttpPut]
        public async Task<ActionResult<Group>> Put(Group group)
        {
            if (group == null)
                return BadRequest();

            if (!await Db.Groups.AnyAsync(t => t.Id == group.Id))
                return NotFound();

            Db.Groups.Update(group);
            await Db.SaveChangesAsync();
            return Ok(group);
        }

        /// <summary>
        /// видалити групу по номеру
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Group>> Delete(int id)
        {
            Group group = await Db.Groups.FirstOrDefaultAsync(t => t.Id == id);
            if (group == null)
                return NotFound();

            Db.Groups.Remove(group);
            await Db.SaveChangesAsync();
            return Ok(group);
        }
    }
}
