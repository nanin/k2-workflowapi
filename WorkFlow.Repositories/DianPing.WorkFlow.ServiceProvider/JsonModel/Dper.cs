using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DianPing.WorkFlow.ServiceProvider.JsonModel
{
    public class Dper
    {
        [JsonProperty("loginId")]
        public int LoginId { get; set; }
        [JsonProperty("reportToLoginId")]
        public int ReportToLoginId { get; set; }
    }
}
