using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class Inscripcion
    {
        public string IdInscripcion { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public string TipoInscripcion { get; set; }
        public string Modalidad { get; set; }
        public string EstadoInscripcion { get; set; }
        public int IdEvento { get; set; }
        public int IdAprendiz { get; set; }
        public int? IdGrupo { get; set; }
    }
}
