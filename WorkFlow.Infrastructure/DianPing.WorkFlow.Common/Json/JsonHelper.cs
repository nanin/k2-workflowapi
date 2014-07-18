using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DianPing.WorkFlow.Common.Json
{
    public class JsonHelper
    {
        public string Content { get; set; }

        public JsonHelper(string content)
        {
            Content = content;
        }

        public string Read(string propertyName)
        {
            var msgObj = JsonConvert.DeserializeObject(Content);
            var jObject = msgObj as Newtonsoft.Json.Linq.JObject;

            if (jObject == null)
                return string.Empty;

            return jObject[propertyName] != null ? (string)jObject[propertyName] : string.Empty;
        }
    }
}
