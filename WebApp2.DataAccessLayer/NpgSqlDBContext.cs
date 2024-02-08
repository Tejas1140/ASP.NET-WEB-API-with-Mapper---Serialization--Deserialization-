using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using WebApp2.Model.Entity;


namespace WebApp2.DataAccessLayer
{
    public class NpgSqlDBContext : DbContext
    {

        private readonly string _connectionString;
        private readonly string _applicationName;
        private readonly ILogger<NpgSqlDBContext> _logger;

        public NpgSqlDBContext(IConfiguration config, ILogger<NpgSqlDBContext> logger)
        {
            _applicationName = "TemplateAPI";
            _connectionString = config["ConnectionStrings:" + _applicationName] ?? "Host=localhost;Port=5432;Database=Member_Database;Username=postgres;Password=admin123";
            _logger = logger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                var connection = new NpgsqlConnection(_connectionString);
                _logger.LogInformation("ConnectionStrings key : ConnectionStrings:" + _applicationName);
                optionsBuilder.UseNpgsql(connection);
                base.OnConfiguring(optionsBuilder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in ApplicationDBContext => OnConfiguring");
                throw;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.HasDefaultSchema("public");
                base.OnModelCreating(modelBuilder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in ApplicationDBContext => OnModelCreating");
                throw;
            }
        }

        public DbSet<MemberDetailsEntity> MemberDetails { get; set; }

    }
}
