using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polla.DAL.Models
{
    public class Partido
    {
        public int Partido_ID { get; set; }
        public int Equipo_Local { get; set; }
        public int Equipo_Visita { get; set; }
        public int Goles_Local { get; set; }
        public int Goles_Visita { get; set; }
        public string Resultado { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
