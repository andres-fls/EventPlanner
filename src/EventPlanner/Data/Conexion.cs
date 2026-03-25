using System.Configuration;
using System.Data.SqlClient;

class Conexion
{
    public SqlConnection Conectar()
    {
        var cs = ConfigurationManager.ConnectionStrings["EventPlannerDB"];

        if (cs == null)
            throw new ConfigurationErrorsException(
                "Falta la cadena de conexión 'EventPlannerDB' en App.config");

        SqlConnection conexion = new SqlConnection(cs.ConnectionString);
        conexion.Open();

        return conexion;
    }
}