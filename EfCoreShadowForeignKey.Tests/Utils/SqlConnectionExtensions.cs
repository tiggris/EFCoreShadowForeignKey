using Microsoft.Data.SqlClient;

namespace EFCoreShadowForeignKey.Tests.Utils
{
    public static class SqlConnectionExtensions
    {
        public static void ExecuteRawSql(this SqlConnection connection, string commandText)
        {
            var command = connection.CreateCommand();
            command.CommandText = commandText;

            command.ExecuteNonQuery();
        }
    }
}
