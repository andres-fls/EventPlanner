using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class Aprendiz
    {
        public int idAprendiz { get; set; }
        public string cedulaAprendiz { get; set; }
        public string nombreAprendiz { get; set; }
        public int edadAprendiz { get; set; }
        public string generoAprendiz { get; set; }
        public string correoAprendiz { get; set; }
        public string telefonoAprendiz { get; set; }
        public int codigoFicha { get; set; }
        public int idUsuario { get; set; }
    }
}
