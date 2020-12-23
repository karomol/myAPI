using System.Collections.Generic;
using Newtonsoft.Json;

namespace KPI_Schedule.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [JsonIgnore]
        public List<Schedule> Schedules { get; set; }
    }
}
