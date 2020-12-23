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
    public class StudentController : ControllerBase
    {
        public DbSchedule Db { get; }
        public StudentController(DbSchedule db) => Db = db;

        /// <summary>
        /// Повертає список студентів.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> Get()
        {
            return await Db.Students.Select(t => new
            {
                id = t.Id,
                groupId = t.GroupId,
                groupName = t.Group.Name,
                name = t.Name,
                email = t.Email,
                phone = t.Phone,
            }).ToListAsync();
        }

        /// <summary>
        /// Повертає студента по заданому id.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            var student = await Db.Students.Select(t => new
            {
                id = t.Id,
                groupId = t.GroupId,
                groupName = t.Group.Name,
                name = t.Name,
                email = t.Email,
                phone = t.Phone,
            }).FirstOrDefaultAsync(t => t.id == id);

            if (student == null)
                return NotFound();

            return student;
        }

        /// <summary>
        /// Додає нового студента до бази даних.
        /// </summary>
        /// <param name="student"></param>
        [HttpPost]
        public async Task<ActionResult<Student>> Post(Student student)
        {
            if (student == null)
                return BadRequest();

            Db.Students.Add(student);
            await Db.SaveChangesAsync();
            return Ok(student);
        }

        /// <summary>
        /// Оновлює існуючого студента.
        /// </summary>
        /// <param name="student"></param>
        [HttpPut]
        public async Task<ActionResult<Student>> Put(Student student)
        {
            if (student == null)
                return BadRequest();

            if (!await Db.Students.AnyAsync(t => t.Id == student.Id))
                return NotFound();

            Db.Students.Update(student);
            await Db.SaveChangesAsync();
            return Ok(student);
        }

        /// <summary>
        /// Видаляє студента по заданому id.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> Delete(int id)
        {
            Student student = await Db.Students.FirstOrDefaultAsync(t => t.Id == id);
            if (student == null)
                return NotFound();

            Db.Students.Remove(student);
            await Db.SaveChangesAsync();
            return Ok(student);
        }
    }
}
