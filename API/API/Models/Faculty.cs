using System.Collections.Generic;
using Newtonsoft.Json;

namespace KPI_Schedule.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        [JsonIgnore]
        public List<Department> Departments { get; set; }
    }
}
