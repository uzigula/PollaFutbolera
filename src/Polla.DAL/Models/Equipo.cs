using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polla.DAL.Models
{
    public class Equipo
    {
        public int Equipo_ID { get; set; }
        public string Grupo { get; set; }
        public string Equipo_desc { get; set; }
        public int Ptos { get; set; }
        public int PJ { get; set; }
        public int PG { get; set; }
        public int PE { get; set; }
        public int PP { get; set; }
        public int GF { get; set; }
        public int GC { get; set; }
        public int DG { get; set; }
    }
}
