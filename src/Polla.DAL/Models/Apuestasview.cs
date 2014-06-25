using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polla.DAL.Models
{
    public class Apuestasview
    {
        public int ApuestaId { get; set; }
        public int ApostadorId { get; set; }
        public string Nombre { get; set; }
        public int Puntaje { get; set; }
        public int Equipo_Id_local { get; set; }
        public string Equipo_desc_local { get; set; }
        public int GolesLocal { get; set; }
        public string Resultado { get; set; }
        public int Equipo_Id_visita { get; set; }
        public string Equipo_desc_visita { get; set; }
        public int Golesvisita { get; set; }
        public int PartidoId { get; set; }
    }
}
