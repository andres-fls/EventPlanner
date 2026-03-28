using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class Evento
    {
        public int idEvento { get; set; }
        public string nombreEvento { get; set; }
        public string tipoEvento { get; set; }
        public string lugarEvento { get; set; }
        public string descripcionEvento { get; set; }
        public DateTime fechaInicioEvento { get; set; }
        public DateTime fechaFinEvento { get; set; }
        public DateTime fechaInicioInscripcion { get; set; }
        public DateTime fechaFinInscripcion { get; set; }
        public int cupoMaximo { get; set; }
        public bool activo { get; set; }
        public int idUsuarioCreador { get; set; }
    }
}
