using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasteSolutions.Objects
{
    public static class Errors
    {
        public static object ContentTooLong = new { error = "Content too long.", error_code = 200 };

        public static object ContentTooShort = new { error = "Content too short.", error_code = 201 };
    }
}
