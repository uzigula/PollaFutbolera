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
    [TestFixture]
    public class DAL_Equipo_Test
    {
        DAL_Equipo dal_equipo = new DAL_Equipo();

        [Test]
        public void GetEquipo_Test_Should_NotBeNull()
        { 
            Equipo equipo_found = null;
            int Equipo_ID = 1;

            equipo_found = dal_equipo.GetEquipo(Equipo_ID);
            equipo_found.Should().NotBeNull();
        }
        [Test]
        public void GetEquipo_Test_Should_BeNull()
        {
            Equipo equipo_found = null;
            int Equipo_ID = 0;

            equipo_found = dal_equipo.GetEquipo(Equipo_ID);
            equipo_found.Should().BeNull();
        }
        [Test]
        public void GetListEquipos() {
            List<Equipo> list = dal_equipo.GetEquipos();
            list.Count.Should().Be(32);
        }
    
    }
}
