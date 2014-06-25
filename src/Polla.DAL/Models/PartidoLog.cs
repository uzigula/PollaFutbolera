using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polla.DAL.Models
{
    public class PartidoLog
    { 
        public int Id { get; set; }
        public int PartidoId { get; set; }
        public string UserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public PartidosView Partido { get; set; }
    }
}
