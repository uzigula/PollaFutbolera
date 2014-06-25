using NUnit.Framework;
using Polla.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Polla.DAL.Test
{
    // well done gato gordo
    [TestFixture]
    public class DAL_Partido_Test
    {
        DAL_Partido dal_partido = new DAL_Partido();

        [Test]
        public void GetPartido_Test_Should_NotBeNull()
        {
            Partido partido_found = new Partido(); 
            int Partido_ID = 1;

            partido_found = dal_partido.GetPartido(Partido_ID);
            partido_found.Should().NotBeNull();
        }
        [Test]
        public void GetPartido_Test_Should_BeNull()
        {
            Partido partido_found = new Partido();
            int Partido_ID = 0;

            partido_found = dal_partido.GetPartido(Partido_ID);
            partido_found.Should().BeNull();
        }

        [Test]
        public void UpdatePartido_Test_Should_Be_Successful()
        {
            Partido partido_search = new Partido();
            string mensaje = null;
            partido_search.Partido_ID = 1;
            partido_search.Goles_Local = 2;
            partido_search.Goles_Visita = 0;
            partido_search.Resultado = "L";
            mensaje = dal_partido.UpdatePartido(partido_search);
            mensaje.Should().Be("Se actualizo los datos del partido");
        }

        [Test]
        public void UpdatePartido_Test_Should_Be_NotExist()
        {
            Partido partido_search = new Partido();
            string mensaje = null;
            partido_search.Partido_ID = 0;
            mensaje = dal_partido.UpdatePartido(partido_search);
            mensaje.Should().Be("El partido no existe");
        }


    }
}
