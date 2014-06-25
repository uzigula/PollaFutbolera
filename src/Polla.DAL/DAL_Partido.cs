using System.Data.SqlClient;
using Polla.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Polla.DAL
{
    public class DAL_Partido : DAL_Base
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(DAL_Partido));

        public Partido GetPartido(int Partido_ID)
        {
            Partido obj = null;
            try
            {
                //para obtener el Equipo
                queryString = " SELECT Partido_Id,Eq_Local,Eq_Visita,Goles_Local,Goles_Visita,Resultado " +
                              " FROM tb_partido WHERE Partido_Id=" + Partido_ID;
                reader = ExecuteReader();
                while (reader.Read())
                {
                    obj = new Partido
                              {
                                  Partido_ID = int.Parse(reader["Partido_Id"].ToString()),
                                  Equipo_Local = int.Parse(reader["Eq_Local"].ToString()),
                                  Equipo_Visita = int.Parse(reader["Eq_Visita"].ToString()),
                                  Goles_Local = int.Parse(reader["Goles_Local"].ToString()),
                                  Goles_Visita = int.Parse(reader["Goles_Visita"].ToString()),
                                  Resultado = reader["Resultado"].ToString(),

                              };

                    if (reader["Fecha"] == DBNull.Value)
                    {
                        obj.Fecha = null;
                    }
                    else
                    {
                        obj.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                _logger.Error("Error - Exception in GetPartido", ex);
                throw;
            }
            finally
            {
                this.DisposeObjects();
            }
            return obj;
        }

        public string UpdatePartido(Partido partido)
        {
            string mensaje = string.Empty;
            try
            {
                Partido obj = GetPartido(partido.Partido_ID);
                if (obj == null)
                {
                    mensaje = "El partido no existe";
                    return mensaje;
                }

                queryString = " UPDATE tb_partido SET Goles_Local=" + partido.Goles_Local + "," +
                              " Goles_Visita=" + partido.Goles_Visita + "," +
                              " resultado='" + partido.Resultado + "' " +
                              " WHERE Partido_Id=" + partido.Partido_ID;
                ExecuteNonQuery();
                mensaje = "Se actualizo los datos del partido";
            }
            catch (Exception ex)
            {
                _logger.Error("Error - Exception in UpdatePartido",ex);
                throw;
            }
            finally
            {
                this.DisposeObjects();
            }
            return mensaje;
        }

        public List<PartidosView> GetPartidosView(int equipo)
        {
            var listPartidos = new List<PartidosView>();
            try
            {
                queryString = " SELECT Partido_Id,equ_id_loc,Equ_desc_loc,Goles_Local, " +
                              " Resultado,equ_id_vis,Equ_desc_vis,Goles_Visita,fecha_partido " +
                              " FROM polla_partidos_view ";
                if (equipo != 0)
                {
                    queryString = queryString + "WHERE equ_id_loc =" + equipo + " OR equ_id_vis=" + equipo;
                }
                reader = ExecuteReader();
                while (reader.Read())
                {
                    var partido = new PartidosView();
                    partido.PartidoId = int.Parse(reader["Partido_Id"].ToString());
                    partido.EquipoIdLocal = int.Parse(reader["equ_id_loc"].ToString());
                    partido.EquipoIdVisita = int.Parse(reader["equ_id_vis"].ToString());
                    partido.GolesLocal = int.Parse(reader["Goles_Local"].ToString());
                    partido.GolesVisita = int.Parse(reader["Goles_Visita"].ToString());
                    partido.EquipoDescLocal = reader["Equ_desc_loc"].ToString();
                    partido.EquipoDescVisita = reader["Equ_desc_vis"].ToString();
                    partido.Resultado = reader["Resultado"].ToString();

                    if (reader["fecha_partido"] == DBNull.Value)
                        partido.Fecha = null;
                    else partido.Fecha = DateTime.Parse(reader["fecha_partido"].ToString());

                    listPartidos.Add(partido);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                _logger.Error("Error - Exception in GetPartidosView",ex);
                throw;
            }
            finally
            {
                this.DisposeObjects();
            }
            return listPartidos;
        }

        public PartidosView GetPartidoViewByPartidoId(int partidoId)
        {
            var partido = new PartidosView();
            try
            {
                queryString = " SELECT Partido_Id,equ_id_loc,Equ_desc_loc,Goles_Local, " +
                              " Resultado,equ_id_vis,Equ_desc_vis,Goles_Visita " +
                              " FROM polla_partidos_view " +
                              " WHERE Partido_Id =" + partidoId ;
                reader = ExecuteReader();
                while (reader.Read())
                {
                    partido.PartidoId = int.Parse(reader["Partido_Id"].ToString());
                    partido.EquipoIdLocal = int.Parse(reader["equ_id_loc"].ToString());
                    partido.EquipoIdVisita = int.Parse(reader["equ_id_vis"].ToString());
                    partido.GolesLocal = int.Parse(reader["Goles_Local"].ToString());
                    partido.GolesVisita = int.Parse(reader["Goles_Visita"].ToString());
                    partido.EquipoDescLocal = reader["Equ_desc_loc"].ToString();
                    partido.EquipoDescVisita = reader["Equ_desc_vis"].ToString();
                    partido.Resultado = reader["Resultado"].ToString();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                _logger.Error("Error - Exception in GetPartidoViewByPartidoId",ex);
                throw;
            }
            finally
            {
                this.DisposeObjects();
            }
            return partido;
        }

    }
}
