using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public string NombreEvento { get; set; }
        public string TipoEvento { get; set; }
        public string LugarEvento { get; set; }
        public string DescripcionEvento { get; set; }
        public DateTime FechaInicioEvento { get; set; }
        public DateTime FechaFinEvento { get; set; }
        public DateTime FechaInicioInscripcion { get; set; }
        public DateTime FechaFinInscripcion { get; set; }
        public int CupoMaximo { get; set; }
        public bool Activo { get; set; }
        public int IdUsuarioCreador { get; set; }
    }
}
