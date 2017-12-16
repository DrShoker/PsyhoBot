using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PsyhoBotAnalizer.Models
{
    [JsonObject(IsReference = true)]
    public class Patient
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string Diagnosis { get; set; }
        public string Sex { get; set; }
        public string DiseasePeriod { get; set; }
        public string NormalCondition { get; set; }
        public string Characteristic { get; set; }

        public int? DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        public virtual ICollection<Dialog> Dialogs { get; set; }
        public Patient()
        {
            Dialogs = new List<Dialog>();
        }
    }
}