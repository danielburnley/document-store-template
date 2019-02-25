using System;
using System.Linq;
using DocumentStoreTemplate.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentStoreTemplate.Contexts
{
    public class DocumentContext : DbContext
    {
        private string _databaseUrl;
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentRow> DocumentRows { get; set; }

        public DocumentContext()
        {
            _databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(
                BuildConnectionStringFromUrl(_databaseUrl));

        private string BuildConnectionStringFromUrl(string databaseUrl)
        {
            Uri uri = new Uri(databaseUrl);
            string[] splitUserInfo = uri.UserInfo.Split(':');
            string server = uri.Host;
            int port = uri.Port;
            string userId = splitUserInfo.ElementAtOrDefault(0);
            string password = splitUserInfo.ElementAtOrDefault(1);
            string database = uri.LocalPath.Substring(1);
            string ngpsqlConnectionString =
                $"Server={server};Port={port};User Id={userId};Password={password};Database={database};SSL Mode=Prefer;";
            return ngpsqlConnectionString;
        }
    }
}