using System.Collections.Generic;
using Newtonsoft.Json;

namespace KPI_Schedule.Models
{
    public class Department
    {
        public int Id { get; set; }
        public int FacultyId { get; set; }
        [JsonIgnore]
        public Faculty Faculty { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        [JsonIgnore]
        public List<Group> Groups { get; set; }
    }
}
