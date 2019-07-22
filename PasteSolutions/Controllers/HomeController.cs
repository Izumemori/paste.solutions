using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PasteSolutions.Database;
using PasteSolutions.Database.Models;
using PasteSolutions.Models;
using PasteSolutions.Objects;

namespace PasteSolutions.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        private readonly IConfiguration _config;

        public HomeController(DatabaseContext databaseContext, IConfiguration config)
        {
            this._databaseContext = databaseContext;
            this._config = config;
        }

        [HttpGet("/{id}")]
        [HttpGet("/{id}.{ext}")]
        public IActionResult GetSnippet(string id, string ext = null)
        {
            if (!TryGetSnippetById(id, ext, out var snippet)) return NotFound();

            if (string.IsNullOrEmpty(ext)) ext = snippet.Language;

            return View(new ViewPasteModel(id, snippet.Text, ext));
        }

        [HttpGet("/{id}/raw")]
        [HttpGet("/{id}.{ext}/raw")]
        public IActionResult GetRawSnippet(string id, string ext = null)
        {
            if (!TryGetSnippetById(id, ext, out var snippet)) return NotFound();

            return Content(snippet.Text);
        }

        [HttpPost("/api/upload")]
        public async Task<IActionResult> InsertSnippet([FromBody] Snippet snippet)
        {
            if (snippet.Text.Length > 1_000_000) return BadRequest(Errors.ContentTooLong);
            if (snippet.Text.Length < 5) return BadRequest(Errors.ContentTooShort);

            var entity = this._databaseContext.Snippets.Add(new DbSnippet
                {
                    Content = snippet.Text,
                    Language = snippet.Language,
                    Expires = DateTimeOffset.Now.AddDays(10)
                });

            await this._databaseContext.SaveChangesAsync();

            var id = entity.Entity.Id;

            return Ok(new { id = id });
        }

        [HttpGet("/")]
        public IActionResult Index([FromQuery] string template = null)
        {
            if (!string.IsNullOrWhiteSpace(template))
            {
                if (TryGetSnippetById(template, null, out var snippet))
                    return View(new IndexViewModel(snippet.Text));
            }

            return View(new IndexViewModel());
        }

        private bool TryGetSnippetById(string id, string ext, out Snippet snippet)
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
