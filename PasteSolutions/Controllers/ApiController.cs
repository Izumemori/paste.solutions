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
    [ApiController]
    [Route("/api")]
    public class ApiController : PasteControllerBase
    {
        public ApiController(DatabaseContext databaseContext, IConfiguration config)
            : base(databaseContext, config)
        {}

        [HttpGet("snippet/{id}")]
        public async Task<ActionResult> GetSnippet(string id)
        {
            if (!TryGetSnippetById(id, string.Empty, out var snippet) || snippet.DbSnippet is null)
                return NotFound(Errors.SnippetNotFound);

            snippet.DbSnippet.LastAccess = DateTimeOffset.UtcNow;

            this._databaseContext.Update(snippet.DbSnippet);
            await this._databaseContext.SaveChangesAsync();

            return Ok(snippet);
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateNewSnippet([FromBody] Snippet snippet)
        {
            if (snippet.Text.Length > 1_000_000) return BadRequest(Errors.ContentTooLong);
            if (snippet.Text.Length < 5) return BadRequest(Errors.ContentTooShort);

            var entity = this._databaseContext.Snippets.Add(new DbSnippet
                {
                    Content = snippet.Text,
                    Language = snippet.Language,
                    LastAccess = DateTimeOffset.Now
                });

            await this._databaseContext.SaveChangesAsync();

            var dbSnippet = entity.Entity;

            return Ok(new { id = dbSnippet.Id, language = dbSnippet.Language });
        }
    }
}
