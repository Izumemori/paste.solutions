using Newtonsoft.Json;
using PasteSolutions.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasteSolutions.Models
{
    public class Snippet
    {
        [JsonProperty("snippet")]
        public string Text { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonIgnore]
        public DbSnippet DbSnippet { get; set; }
    }
}
