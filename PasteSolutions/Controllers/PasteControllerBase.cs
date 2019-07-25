using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using PasteSolutions.Database;
using PasteSolutions.Database.Models;
using PasteSolutions.Models;
using PasteSolutions.Objects;

namespace PasteSolutions.Controllers
{
    public abstract class PasteControllerBase : Controller
    {
        protected readonly DatabaseContext _databaseContext;

        protected readonly IConfiguration _config;

        protected PasteControllerBase(DatabaseContext databaseContext, IConfiguration config)
        {
            this._databaseContext = databaseContext;
            this._config = config;
        }

        protected bool TryGetSnippetById(string id, string ext, out Snippet snippet)
        {
            snippet = null;

            var files = Directory.GetFiles(this._config["staticPagesPath"], $"{id}.*");

            if (files.Count() > 0) // Get file from static storage
            {
                string file = string.Empty;

                if (!string.IsNullOrEmpty(ext))
                    file = files.FirstOrDefault(x => x.EndsWith(ext));

                if (string.IsNullOrEmpty(ext))
                    file = files.First();

                var content = System.IO.File.ReadAllText(file);

                snippet = new Snippet() { Id = id, Text = content, Language = Path.GetExtension(file) };

                return true;
            }

            var dbSnippet = this._databaseContext.Snippets.FirstOrDefault(x => x.Id == id);

            if (dbSnippet is null) return false;

            snippet = new Snippet() { Id = id, Text = dbSnippet.Content, Language = dbSnippet.Language };

            return true;
        }
    }
}
