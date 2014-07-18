using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DianPing.WorkFlow.ServiceProvider.JsonModel
{
    public class Department
    {
        [JsonProperty("departmentId")]
        public int DepartmentId { get; set; }
        [JsonProperty("level")]
        public int Level { get; set; }
        [JsonProperty("leaderID")]
        public int LeaderId { get; set; }
    }
}
