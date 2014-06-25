using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Polla.DAL;

namespace Polla.Web.DAL
{
    public class DAL_repository
    {
        public DAL_Apuesta dal_apuesta = new DAL_Apuesta();
        public DAL_Apostador dal_apostador = new DAL_Apostador();
        public DAL_Equipo dal_equipo = new DAL_Equipo();
        public DAL_Partido dal_partido = new DAL_Partido();
        public DAL_Partido_Log dal_partido_log = new DAL_Partido_Log();
    }
}