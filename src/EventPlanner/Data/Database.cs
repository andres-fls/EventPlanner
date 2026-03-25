using System.Data.SqlClient;

namespace EventPlanner.Data
{
    public class Database
    {
        private static string connectionString = "Data Source=.;Initial Catalog=EventPlannerDB;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
