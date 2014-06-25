using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polla.DAL.Models
{
    public class Apuesta
    {
        public int ApuestaId { get; set; }
        public int ApostadorId { get; set; }
        public int PartidoId { get; set; }
        public int Goles_Local { get; set; }
        public int Goles_Visita { get; set; }
        public string Resultado { get; set; }
        public int Puntaje { get; set; } 
    }
}
