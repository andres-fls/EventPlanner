using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class Programa
    {
        public int idPrograma { get; set; }
        public int codigoPrograma { get; set; }
        public string nombrePrograma { get; set; }
        public string duracionPrograma { get; set; }
        public string nivelPrograma { get; set; }
    }
}
