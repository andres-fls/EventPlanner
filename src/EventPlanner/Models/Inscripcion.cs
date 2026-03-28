using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class Inscripcion
    {
        public int idInscripcion { get; set; }
        public DateTime fechaInscripcion { get; set; }
        public string tipoInscripcion { get; set; }
        public string modalidad { get; set; }
        public string estadoInscripcion { get; set; }
        public int idEvento { get; set; }
        public int idAprendiz { get; set; }
        public int? idGrupo { get; set; }
    }
}
