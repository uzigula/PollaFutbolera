using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Polla.DAL.Models;

namespace Polla.DAL.Test
{
    [TestFixture]
    public class DAL_Partido_Log_Test
    {
        DAL_Partido_Log dal_partido_log = new DAL_Partido_Log();
        [Test]
        public void GetListEquipos()
        {
            List<PartidoLog> list = dal_partido_log.GetPartidos(); 
        }
    }
}
