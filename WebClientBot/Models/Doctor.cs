using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebClientBot.Models
{
    [JsonObject(IsReference = true)]
    public class Doctor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string Category { get; set; }
        public string Charasteristic { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
        public Doctor()
        {
            Patients = new List<Patient>();
        }
    }
}