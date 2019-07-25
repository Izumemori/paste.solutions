using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PasteSolutions.Database.Generators;
using PasteSolutions.Database.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using UniqueID;

namespace PasteSolutions.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _config;
        public GeneratorBucket GeneratorBucket { get; private set; }
        public DbSet<DbSnippet> Snippets { get; set; }

        public DatabaseContext(IConfiguration config, GeneratorBucket generatorBucket, DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            this._config = config;
            this.GeneratorBucket = generatorBucket;
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<DbSnippet>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.Id)
                    .HasColumnName("id")
                    .HasValueGenerator<IdGenerator>();
                x.Property(x => x.Language)
                    .HasColumnName("language");
                x.Property(x => x.Content)
                    .HasColumnName("content");
                x.Property(x => x.LastAccess)
                    .HasColumnType("timezonetz")
                    .HasColumnName("last_access");
                x.ToTable("snippets");
            });

            base.OnModelCreating(model);
        }
    }
}
