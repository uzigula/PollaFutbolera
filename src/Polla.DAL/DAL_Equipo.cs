using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polla.DAL.Models;
using log4net;

namespace Polla.DAL
{
    public class DAL_Equipo:DAL_Base
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(DAL_Equipo));

        public Equipo GetEquipo(int Equipo_ID)
        {
            Equipo obj = null;
            try
            {
                //para obtener el Equipo
                queryString = " SELECT Equipo_id,Grupo,Equipo_desc,Ptos,PJ,PG,PE,PP,GF,GC,DG "+
                              " FROM tb_equipo WHERE equipo_id='" + Equipo_ID + "' "+
							  " ORDER BY grupo,ptos desc,dg desc ";
                reader = ExecuteReader();
                while (reader.Read())   
                {
                    obj = new Equipo();
                    obj.Equipo_ID = int.Parse(reader["Equipo_id"].ToString());
                    obj.Grupo = reader["Grupo"].ToString();
                    obj.Equipo_desc = reader["equipo_desc"].ToString();
                    obj.Ptos = int.Parse(reader["ptos"].ToString());
                    obj.PJ = int.Parse(reader["PJ"].ToString());
                    obj.PG = int.Parse(reader["PG"].ToString());
                    obj.PE = int.Parse(reader["PE"].ToString());
                    obj.PP = int.Parse(reader["PP"].ToString());
                    obj.GF = int.Parse(reader["GF"].ToString());
                    obj.GC = int.Parse(reader["GC"].ToString());
                    obj.DG = int.Parse(reader["DG"].ToString());
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                _logger.Error("Error in GetEquipo", ex);
                throw;
            }
            finally
            {
                DisposeObjects();
            }
            return obj;
        }

        public List<Equipo> GetEquipos() {
            List<Equipo> list = new List<Equipo>();

            try
            {
                //para obtener el Equipo
                queryString = " SELECT Equipo_id,Grupo,Equipo_desc,Ptos,PJ,PG,PE,PP,GF,GC,DG " +
                              " FROM tb_equipo ORDER BY grupo,ptos desc,dg desc ";
                reader = ExecuteReader();
                while (reader.Read())
                {
                    Equipo obj = new Equipo();
                    obj.Equipo_ID = int.Parse(reader["Equipo_id"].ToString());
                    obj.Grupo = reader["Grupo"].ToString();
                    obj.Equipo_desc = reader["equipo_desc"].ToString();
                    obj.Ptos = int.Parse(reader["ptos"].ToString());
                    obj.PJ = int.Parse(reader["PJ"].ToString());
                    obj.PG = int.Parse(reader["PG"].ToString());
                    obj.PE = int.Parse(reader["PE"].ToString());
                    obj.PP = int.Parse(reader["PP"].ToString());
                    obj.GF = int.Parse(reader["GF"].ToString());
                    obj.GC = int.Parse(reader["GC"].ToString());
                    obj.DG = int.Parse(reader["DG"].ToString());
                    list.Add(obj);
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                _logger.Error("Error in GetEquipos", ex);
                throw;
            }
            finally
            {
                DisposeObjects();
            }
            return list;
        }

        public int VerificarPJdeGrupo(string grupo)
        {
            int pj;
            try
            {
                queryString = "SELECT SUM(PJ) as PJ from tb_equipo  " +
                              " WHERE grupo='" + grupo + "'";
                string mensaje = ExecuteScalar();
                pj = int.Parse(mensaje);
            }
            catch (Exception ex)
            {
                _logger.Error("Error in VerificarPJdeGrupo", ex);
                throw;
            }
            finally
            {
                DisposeObjects();
            }
            return pj;
        }

        public void ActualizarPuntajeDeGrupo(string grupo) {
            queryString = "EXEC uspFinalizarPuntuacionGrupos @grupo = '" + grupo + "'";
            ExecuteScalar();
        }
    }
}
