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
    public  class DAL_Apuesta_Test
    {
        DAL_Apuesta dal_apuesta = new DAL_Apuesta();

        [Test]
        public void GetEquipo_Test_Should_NotBeNull()
        {
            Apuesta apuesta_search = new Apuesta();
            Apuesta apuesta_found = null;
            apuesta_search.ApuestaId = 1;

            apuesta_found = dal_apuesta.GetApuesta(apuesta_search);
            apuesta_found.Should().NotBeNull();
        }

        [Test]
        public void GetEquipo_Test_Should_BeNull()
        {
            Apuesta apuesta_search = new Apuesta();
            Apuesta apuesta_found = null;
            apuesta_search.ApuestaId = 0;

            apuesta_found = dal_apuesta.GetApuesta(apuesta_search);
            apuesta_found.Should().BeNull();
        }
    }
}