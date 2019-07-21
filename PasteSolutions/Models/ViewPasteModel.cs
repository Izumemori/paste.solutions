using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasteSolutions.Models
{
    public class ViewPasteModel
    {
        public string Id { get; }

        public int Lines { get; }

        public string Code { get; }

        public string Extension { get; }

        public ViewPasteModel(string id, string code, string extension = null)
        {
            this.Id = id;
            this.Lines = code.Split('\n').Length;
            this.Code = code;
            this.Extension = extension;
        }
    }
}
