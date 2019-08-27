using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AMS.Models
{
    /**********************************************************************************************//**
 * @class   Entity<TPrimaryKey>
 *
 * @brief   实体基类
 *
 * @author  rxf
 * @date    2017/12/27
 *
 * @tparam  TPrimaryKey Type of the primary key.
 **************************************************************************************************/
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class Entity<TPrimaryKey>
    {
        [JsonProperty("id")]
        //[Range(1, 9223372036854775807,ErrorMessage ="请输入正确的ID")]
        public virtual TPrimaryKey Id { get; set; }

    }

    /**********************************************************************************************//**
     * @class   Entity
     *
     * @brief   实体类
     *
     * @author  rxf
     * @date    2017/12/27
     **************************************************************************************************/
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class Entity : Entity<Int64>
    {
        protected Entity()
        {
            down = new Dictionary<string, object>();
            up = new Dictionary<string, object>();
        }
        [NonSerialized]
        private Dictionary<string, object> down = null;
        [JsonProperty("down")]
        [NotMapped]
        public Dictionary<string, object> Down
        {
            get
            {
                return down;
            }
        }

        [NonSerialized]
        private Dictionary<string, object> up = null;
        [JsonProperty("up")]
        [NotMapped]
        public Dictionary<string, object> Up
        {
            get
            {
                return up;
            }
        }
    }
}
