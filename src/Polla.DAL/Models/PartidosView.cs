using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polla.DAL.Models
{
    public class PartidosView
    {
        public int PartidoId { get; set; }
        public int EquipoIdLocal { get; set; }
        public string EquipoDescLocal { get; set; }
        public int GolesLocal { get; set; }
        public string Resultado { get; set; }
        public int EquipoIdVisita { get; set; }
        public string EquipoDescVisita { get; set; }
        public int GolesVisita { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
