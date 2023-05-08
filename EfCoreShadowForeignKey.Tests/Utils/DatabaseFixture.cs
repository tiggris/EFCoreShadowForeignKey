using EFCoreShadowForeignKey.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EFCoreShadowForeignKey.Tests.Utils
{
    public class DatabaseFixture : IDisposable
    {
        private static readonly DbContextOptionsBuilder<DataContext> DbContextOptionsBuilder;
        private const string SqlServerConnectionString = @"Server=(localdb)\mssqllocaldb";
        private const string DatabaseName = "TestDb2";

        static DatabaseFixture()
        {
            DbContextOptionsBuilder = new DbContextOptionsBuilder<DataContext>();
            DbContextOptionsBuilder.UseSqlServer(DatabaseConnectionString);
        }

        public DatabaseFixture()
        {
            CreateDatabase();
            InitializeDatabase();
        }

        public static string DatabaseConnectionString => $"{SqlServerConnectionString};Database={DatabaseName}";

        public static DbContextOptions<DataContext> DbContextOptions => DbContextOptionsBuilder.Options;

        public void Dispose()
        {
            DropDatabase();
        }

        private void CreateDatabase()
        {
            using var connection = new SqlConnection(SqlServerConnectionString);
            connection.Open();
            connection.ExecuteRawSql($"CREATE DATABASE {DatabaseName}");
            connection.Close();
        }

        private void DropDatabase()
        {
            using var connection = new SqlConnection(DatabaseConnectionString);
            connection.Open();
            SqlConnection.ClearPool(connection);
            connection.ChangeDatabase("master");
            connection.ExecuteRawSql($"DROP DATABASE {DatabaseName}");
            connection.Close();
        }

        private void InitializeDatabase()
        {
            using var dataContext = new DataContext(DbContextOptions);
            dataContext.Database.EnsureCreated();
        }
    }
}
