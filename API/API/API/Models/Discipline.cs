using System.Collections.Generic;
using Newtonsoft.Json;

namespace KPI_Schedule.Models
{
    public class Discipline
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<Schedule> Schedules { get; set; }
    }
}
