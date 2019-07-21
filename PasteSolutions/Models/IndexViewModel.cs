using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasteSolutions.Models
{
    public class IndexViewModel
    {
        public string Template { get; }

        public IndexViewModel(string template = null)
            => this.Template = template;
    }
}
