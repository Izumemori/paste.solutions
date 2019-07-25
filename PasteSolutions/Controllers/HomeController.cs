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
    [Route("/")]
    public class HomeController : PasteControllerBase
    {
        public HomeController(DatabaseContext databaseContext, IConfiguration config)
            : base(databaseContext, config)
        {}

        [HttpGet("{id}")]
        [HttpGet("{id}.{ext}")]
        public IActionResult GetSnippet(string id, string ext = null)
        {
            var files = Directory.GetFiles("wwwroot");
            var file = files.FirstOrDefault(x => Path.GetFileNameWithoutExtension(x) == id);
            if (file != null)
            {
                var provider = new FileExtensionContentTypeProvider();
                
                provider.TryGetContentType(file, out var contentType);

                return File(System.IO.File.ReadAllBytes(file), contentType ?? "text/plain");
            }

            if (!TryGetSnippetById(id, ext, out var snippet)) return NotFound();

            if (string.IsNullOrEmpty(ext)) ext = snippet.Language;

            return View(new ViewPasteModel(id, snippet.Text, ext));
        }

        [HttpGet("{id}/raw")]
        [HttpGet("{id}.{ext}/raw")]
        public IActionResult GetRawSnippet(string id, string ext = null)
        {
            if (!TryGetSnippetById(id, ext, out var snippet)) return NotFound();

            return Content(snippet.Text);
        }

        [HttpGet]
        public IActionResult Index([FromQuery] string template = null)
        {
            if (!string.IsNullOrWhiteSpace(template))
            {
                if (TryGetSnippetById(template, null, out var snippet))
                    return View(new IndexViewModel(snippet.Text));
            }

            return View(new IndexViewModel());
        }
    }
}
