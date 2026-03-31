// ============================================================
// Archivo: Conexion.cs
// Propósito: Proporciona la configuración y el método para 
//            establecer la conexión con la base de datos SQL Server.
// Contiene la cadena de conexión y un método que retorna 
//            un objeto SqlConnection listo para usar.
// ============================================================

using System.Data.SqlClient;

namespace EventPlanner.Data
{
    // Clase encargada de manejar la conexión a la base de datos.
    public class Conexion
    {
        // Cadena de conexión a SQL Server.
        // - "Server=.;" -> Usa el servidor local (por defecto, la instancia local).
        // - "Database=EventPlanner;" -> Nombre de la base de datos a la que se conecta.
        // - "Trusted_Connection=True;" -> Usa autenticación de Windows (usuario del sistema).
        private string cadenaConexion =
            "Server=.;Database=EventPlanner;Trusted_Connection=True;";

        // Método público que crea y retorna una nueva conexión a la base de datos.
        // Retorna: SqlConnection - Objeto de conexión listo para abrir y usar.
        public SqlConnection Conectar()
        {
            // Crea una instancia de SqlConnection con la cadena almacenada.
            return new SqlConnection(cadenaConexion);
        }
    }
}