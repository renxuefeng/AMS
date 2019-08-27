using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AMS.Models.Entitys
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    [Table("Log")]
    public class LogInfo : Entity
    {
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Exception { get; set; }
        public string LogEvent { get; set; }
        public string UserName { get; set; }
    }
}
