using Polla.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Polla.DAL
{
    public class DAL_Apuesta : DAL_Base
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(DAL_Apuesta));

        public Apuesta GetApuesta(Apuesta apuesta) {
            Apuesta obj = null;
            try
            {
                //para obtener la Apuesta
                queryString = " SELECT Apuesta_ID,Partido_Id,Apostador_Id,Goles_Local,Goles_Visita,Resultado,Puntaje " +
                              " FROM tb_apuesta WHERE Apuesta_ID=" + apuesta.ApuestaId;
                reader = ExecuteReader();
                while (reader.Read())
                {
                    obj = new Apuesta();
                    obj.ApuestaId = int.Parse(reader["Apuesta_ID"].ToString());
                    obj.PartidoId = int.Parse(reader["Partido_Id"].ToString());
                    obj.ApostadorId = int.Parse(reader["Apostador_Id"].ToString());
                    obj.Goles_Local = int.Parse(reader["Goles_Local"].ToString());
                    obj.Goles_Visita = int.Parse(reader["Goles_Visita"].ToString());
                    obj.Resultado = reader["Resultado"].ToString();
                    obj.Puntaje = int.Parse(reader["Puntaje"].ToString());
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                _logger.Error("Error in GetApuesta", ex);
                throw;
            }
            finally
            {
                DisposeObjects();
            }

            return obj;
        }

        public string CreateNewApuesta(Apuesta apuesta) {
            string mensaje = "";
            queryString = " Insert tb_apuesta (partido_id,apostador_id,goles_local,goles_visita,resultado,puntaje) " +
                              " Values (" + apuesta.PartidoId + "," + apuesta.ApostadorId + "," +
                                            apuesta.Goles_Local + "," + apuesta.Goles_Visita + ",'" +
                                            apuesta.Resultado + "',0"  + 
                                     ")";
            ExecuteNonQuery();
            mensaje = "Se ingreso una nueva apuesta";
            return mensaje;
        }

        public List<Apuestasview> GetApuestasByPartido(int partidoId)
        {
            List<Apuestasview> apuestas = new List<Apuestasview>();

            try
            {
                //para obtener la Apuesta
                queryString = " SELECT Apuesta_ID, Apostador_Id, Nombre, Puntaje, equ_id_loc, Equ_desc_loc, Goles_Local, " +
                              " Resultado, equ_id_vis, Goles_Visita, Equ_desc_vis, Partido_Id " +
                              " FROM polla_apuestas_view WHERE partido_id=" + partidoId;
                reader = ExecuteReader();
                while (reader.Read())
                {
                    Apuestasview obj = new Apuestasview();
                    obj.ApuestaId = int.Parse(reader["Apuesta_ID"].ToString());
                    obj.ApostadorId = int.Parse(reader["apostador_id"].ToString());
                    obj.Nombre = reader["nombre"].ToString();
                    obj.Puntaje = int.Parse(reader["puntaje"].ToString());
                    obj.Equipo_Id_local = int.Parse(reader["equ_id_loc"].ToString());
                    obj.Equipo_desc_local = reader["equ_desc_loc"].ToString();
                    obj.GolesLocal = int.Parse(reader["goles_local"].ToString());
                    obj.Equipo_Id_visita = int.Parse(reader["equ_id_vis"].ToString());
                    obj.Equipo_desc_visita = reader["equ_desc_vis"].ToString();
                    obj.Golesvisita = int.Parse(reader["goles_visita"].ToString());
                    obj.PartidoId = int.Parse(reader["partido_id"].ToString());
                    obj.Resultado = reader["resultado"].ToString();
                    apuestas.Add(obj);
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                _logger.Error("Error in GetApuestasByPartido", ex);
                throw;
            }
            finally
            {
                DisposeObjects();
            }

            return apuestas;
        }

        public List<Apuestasview> GetApuestasByApostador(int apostadorId)
        {
            List<Apuestasview> apuestas = new List<Apuestasview>();

            try
            {
                //para obtener la Apuesta
                queryString = " SELECT Apuesta_ID, Apostador_Id, Nombre, Puntaje, equ_id_loc, Equ_desc_loc, Goles_Local, " +
                              " Resultado, equ_id_vis, Goles_Visita, Equ_desc_vis, Partido_Id " +
                              " FROM polla_apuestas_view WHERE Apostador_Id=" + apostadorId;
                reader = ExecuteReader();
                while (reader.Read())
                {
                    Apuestasview obj = new Apuestasview();
                    obj.ApuestaId = int.Parse(reader["Apuesta_ID"].ToString());
                    obj.ApostadorId = int.Parse(reader["apostador_id"].ToString());
                    obj.Nombre = reader["nombre"].ToString();
                    obj.Puntaje = int.Parse(reader["puntaje"].ToString());
                    obj.Equipo_Id_local = int.Parse(reader["equ_id_loc"].ToString());
                    obj.Equipo_desc_local = reader["equ_desc_loc"].ToString();
                    obj.GolesLocal = int.Parse(reader["goles_local"].ToString());
                    obj.Equipo_Id_visita = int.Parse(reader["equ_id_vis"].ToString());
                    obj.Equipo_desc_visita = reader["equ_desc_vis"].ToString();
                    obj.Golesvisita = int.Parse(reader["goles_visita"].ToString());
                    obj.PartidoId = int.Parse(reader["partido_id"].ToString());
                    obj.Resultado = reader["resultado"].ToString();
                    apuestas.Add(obj);
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                _logger.Error("Error in GetApuestasByApostador", ex);
                throw;
            }
            finally
            {
                DisposeObjects();
            }

            return apuestas;
        }
    }
}
