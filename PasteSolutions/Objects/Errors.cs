using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasteSolutions.Objects
{
    public static class Errors
    {
        public static object ContentTooLong = new { error = "Content too long.", error_code = 1000 };

        public static object ContentTooShort = new { error = "Content too short.", error_code = 1001 };

        public static object SnippetNotFound = new { error = "Snippet not found.", error_code = 1002 };
    }
}
