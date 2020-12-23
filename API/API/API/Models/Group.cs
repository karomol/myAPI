using System.Collections.Generic;
using Newtonsoft.Json;

namespace KPI_Schedule.Models
{
    public class Group
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        [JsonIgnore]
        public Department Department { get; set; }
        public string Name { get; set; }
        public int Course { get; set; }

        [JsonIgnore]
        public List<Schedule> Schedules { get; set; }
        [JsonIgnore]
        public List<Student> Students { get; set; }
    }
}
