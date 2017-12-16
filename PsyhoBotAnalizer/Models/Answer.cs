using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PsyhoBotAnalizer.Models
{
    [JsonObject(IsReference = true)]
    public class Answer
    {
        public int Id { get; set; }
        public string Body { get; set; }

        public int? QuestionId { get; set; }
        public Question Question { get; set; }
    }
}