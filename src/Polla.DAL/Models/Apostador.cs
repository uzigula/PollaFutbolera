using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polla.DAL.Models
{
    public class Apostador
    {
        public int ApostadorId { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string DNI { get; set; }
        public int Puntaje { get; set; }
    }
}
