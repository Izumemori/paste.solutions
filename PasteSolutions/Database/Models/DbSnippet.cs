using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasteSolutions.Database.Models
{
    public class DbSnippet
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string Language { get; set; }

        public DateTimeOffset? LastAccess { get; set; }
    }
}
