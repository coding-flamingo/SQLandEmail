using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SQLandEmailwithBlazorPage.Models;
using System.Data.SqlClient;

namespace SQLandEmailwithBlazorPage.Data
{
    public class TutorialDBContext : DbContext
    {
        public virtual DbSet<PersonModel> People { get; set; }

        private readonly IConfiguration _configuration;

        private AzureServiceTokenProvider _azureServiceTokenProvider;
        public TutorialDBContext(IConfiguration configuration, DbContextOptions<TutorialDBContext> options, AzureServiceTokenProvider azureServiceTokenProvider) : base(options)
        {
            _configuration = configuration;
            _azureServiceTokenProvider = azureServiceTokenProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = _configuration.GetConnectionString("SQLDBConnection"),
                AccessToken = _azureServiceTokenProvider.GetAccessTokenAsync("https://database.windows.net/").Result
        };
            optionsBuilder.UseSqlServer(connection);
        }

    }
}
