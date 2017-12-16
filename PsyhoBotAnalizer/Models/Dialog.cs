using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PsyhoBotAnalizer.Models
{
    [JsonObject(IsReference = true)]
    public class Dialog
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int? PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public Dialog()
        {
            Questions = new List<Question>();
        }
    }
}