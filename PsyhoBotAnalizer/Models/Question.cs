using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PsyhoBotAnalizer.Models
{
    [JsonObject(IsReference = true)]
    public class Question
    {
        public int Id { get; set; }
        public string Body { get; set; }

        public virtual ICollection<Dialog> Dialogs { get; set; }
        public Question()
        {
            Dialogs = new List<Dialog>();
            Answers = new List<Answer>();
        }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}