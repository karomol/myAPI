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
    public class ScheduleController : ControllerBase
    {
        public DbSchedule Db { get; }
        public ScheduleController(DbSchedule db) => Db = db;

        /// <summary>
        /// отримати розклад
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> Get()
        {
            return await Db.Schedules.Select(t => new
            {
                id = t.Id,
                name = t.Name,
                teacherName = t.Teacher.Name,
                disciplineName = t.Discipline.Name,
                groupName = t.Group.Name,
                time = t.Time,
                classroom = t.Classroom,
            }).ToListAsync();
        }

        /// <summary>
        /// отримати розклад на день по номеру
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            var schedule = await Db.Schedules.Select(t => new
            {
                id = t.Id,
                name = t.Name,
                teacherId = t.TeacherId,
                teacherName = t.Teacher.Name,
                disciplineId = t.DisciplineId,
                disciplineName = t.Discipline.Name,
                groupId = t.GroupId,
                groupName = t.Group.Name,
                time = t.Time,
                classroom = t.Classroom,
            }).FirstOrDefaultAsync(t => t.id == id);

            if (schedule == null)
                return NotFound();

            return schedule;
        }

        /// <summary>
        /// додати розклад на день
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///       "name": "string",
        ///       "teacherId": 0,
        ///       "disciplineId": 0,
        ///       "groupId": 0,
        ///       "time": "string",
        ///       "classroom": "string"
        ///     }
        /// </remarks>
        /// <param name="schedule"></param>
        [HttpPost]
        public async Task<ActionResult<Schedule>> Post(Schedule schedule)
        {
            if (schedule == null)
                return BadRequest();

            Db.Schedules.Add(schedule);
            await Db.SaveChangesAsync();
            return Ok(schedule);
        }

        /// <summary>
        /// оновити розклад дня
        /// </summary>
        /// <param name="schedule"></param>
        [HttpPut]
        public async Task<ActionResult<Schedule>> Put(Schedule schedule)
        {
            if (schedule == null)
                return BadRequest();

            if (!await Db.Schedules.AnyAsync(t => t.Id == schedule.Id))
                return NotFound();

            Db.Schedules.Update(schedule);
            await Db.SaveChangesAsync();
            return Ok(schedule);
        }

        /// <summary>
        /// видалити розклад по номеру
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Schedule>> Delete(int id)
        {
            Schedule schedule = await Db.Schedules.FirstOrDefaultAsync(t => t.Id == id);
            if (schedule == null)
                return NotFound();

            Db.Schedules.Remove(schedule);
            await Db.SaveChangesAsync();
            return Ok(schedule);
        }
    }
}
