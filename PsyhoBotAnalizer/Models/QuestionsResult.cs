using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PsyhoBotAnalizer.Models
{
    [JsonObject(IsReference = true)]
    public class QuestionsResult
    {
        
            public int Id { get; set; }

            public string Question { get; set; }

            public string Answer { get; set; }

            public int? DialogId { get; set; }

            public virtual Dialog Dialog { get; set; }
        }
}