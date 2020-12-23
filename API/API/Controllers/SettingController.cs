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
    public class SettingController : ControllerBase
    {
        public DbSchedule Db { get; }
        public SettingController(DbSchedule db) => Db = db;

        /// <summary>
        /// Повертає список налаштувань.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SettingUniversity>>> Get()
        {
            return await Db.SettingUniversity.ToListAsync();
        }

        /// <summary>
        /// Повертає налаштування по заданому id.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<SettingUniversity>> Get(int id)
        {
            SettingUniversity settingUniversity = await Db.SettingUniversity.FirstOrDefaultAsync(t => t.Id == id);
            if (settingUniversity == null)
                return NotFound();

            return settingUniversity;
        }

        /// <summary>
        /// Додає нове налаштування до бази даних.
        /// </summary>
        /// <param name="settingUniversity"></param>
        [HttpPost]
        public async Task<ActionResult<SettingUniversity>> Post(SettingUniversity settingUniversity)
        {
            if (settingUniversity == null)
                return BadRequest();

            Db.SettingUniversity.Add(settingUniversity);
            await Db.SaveChangesAsync();
            return Ok(settingUniversity);
        }

        /// <summary>
        /// Оновлює існуюче налаштування.
        /// </summary>
        /// <param name="settingUniversity"></param>
        [HttpPut]
        public async Task<ActionResult<SettingUniversity>> Put(SettingUniversity settingUniversity)
        {
            if (settingUniversity == null)
                return BadRequest();

            if (!await Db.SettingUniversity.AnyAsync(t => t.Id == settingUniversity.Id))
                return NotFound();

            Db.SettingUniversity.Update(settingUniversity);
            await Db.SaveChangesAsync();
            return Ok(settingUniversity);
        }

        /// <summary>
        /// Видаляє налаштування по заданому id.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<SettingUniversity>> Delete(int id)
        {
            SettingUniversity settingUniversity = await Db.SettingUniversity.FirstOrDefaultAsync(t => t.Id == id);
            if (settingUniversity == null)
                return NotFound();

            Db.SettingUniversity.Remove(settingUniversity);
            await Db.SaveChangesAsync();
            return Ok(settingUniversity);
        }
    }
}
