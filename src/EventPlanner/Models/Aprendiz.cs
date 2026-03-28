using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class Aprendiz
    {
        public int IdAprendiz { get; set; }
        public string CedulaAprendiz { get; set; }
        public string NombreAprendiz { get; set; }
        public int EdadAprendiz { get; set; }
        public string GeneroAprendiz { get; set; }
        public string CorreoAprendiz { get; set; }
        public string TelefonoAprendiz { get; set; }
        public int CodigoFicha { get; set; }
        public int IdUsuario { get; set; }
    }
}
