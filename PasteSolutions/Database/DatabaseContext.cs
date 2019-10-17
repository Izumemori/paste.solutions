using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;
using PasteSolutions.Database.Generators;
using PasteSolutions.Database.Models;
using PasteSolutions.Objects;
using UniqueID;

namespace PasteSolutions.Database
{
    public class DatabaseContext : DbContext
    {
        public GeneratorBucket GeneratorBucket { get; private set; }
        public DbSet<DbSnippet> Snippets { get; set; }

        private readonly DatabaseConfiguration _config;

        public DatabaseContext(IOptions<DatabaseConfiguration> config, GeneratorBucket generatorBucket, DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            this.GeneratorBucket = generatorBucket;
            this._config = config.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var csb = new NpgsqlConnectionStringBuilder()
            {
                Host = this._config.Host,
                Port = this._config.Port,
                Database = this._config.Database,
                Username = this._config.Username,
                Password = this._config.Password,

                SslMode = SslMode.Prefer,
                TrustServerCertificate = true // or not, if you are sure that your certs are issued by well-known CAs
            };

            optionsBuilder.UseNpgsql(csb.ConnectionString);
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
