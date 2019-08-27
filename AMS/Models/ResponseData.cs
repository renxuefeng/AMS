using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.Models
{
    [JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
    public class ResponseData
    {
        public ResponseData()
        {
        }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public virtual object Data { get; set; } = new object();
    }
}
