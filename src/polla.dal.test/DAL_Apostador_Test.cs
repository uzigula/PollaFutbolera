using System;
using NUnit.Framework;
using FluentAssertions;
using Polla.DAL.Models;

namespace Polla.DAL.Test
{
    [TestFixture]
    public class DAL_Apostador_Test
    {
        DAL_Apostador dal_apostador = new DAL_Apostador();

        [Test]
        public void CreateNewApostador_Test_DNI_repetido()
        {
            Apostador apostador = new Apostador();
            apostador.Nombre = "Roy Rojas";
            apostador.Correo = "roy.rojval@gmail.com";
            apostador.DNI = "45155348";
            string mensaje = dal_apostador.CreateNewApostador(apostador);
            mensaje.Should().Be("DNI ya registrado");
        }
        [Test]
        public void CreateNewApostador_Test_Correo_repetido()
        {
            Apostador apostador = new Apostador();
            apostador.Nombre = "Roy Rojas";
            apostador.Correo = "roy.rojval@gmail.com";
            apostador.DNI = "01234567";
            string mensaje = dal_apostador.CreateNewApostador(apostador);
            mensaje.Should().Be("Correo ya registrado");
        }
    }
}
