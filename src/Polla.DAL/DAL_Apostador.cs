using Polla.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using log4net;

namespace Polla.DAL
{
    public class DAL_Apostador : DAL_Base
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(DAL_Apostador));

        public void GetApostador(Apostador apostador, out string mensaje)
        {
            try
            {
                var count = "";
                //para checar DNI
                queryString = "Select count(*) from tb_apostador where dni='" + apostador.DNI + "'";
                count = ExecuteScalar();
                if (count != "0")
                {
                    mensaje = "DNI ya registrado";
                    return;
                }
                //para checar DNI
                queryString = "Select count(*) from tb_apostador where correo='" + apostador.Correo + "'";
                count = ExecuteScalar();
                if (count != "0")
                {
                    mensaje = "Correo ya registrado";
                    return;
                }
                mensaje = "";

            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                _logger.Error("Error in GetApostador",ex);
                throw;
            }
            finally
            {
                DisposeObjects();
            }
        }

        public string CreateNewApostador(Apostador apostador)
        {
            string mensaje;
            GetApostador(apostador, out mensaje);

            try
            {
                if (string.IsNullOrEmpty(mensaje))
                {
                    queryString = " Insert tb_apostador (nombre,correo,dni,puntaje) " +
                                  " Values ('" + apostador.Nombre + "','" + apostador.Correo + "','" + apostador.DNI +
                                  "',0)";
                    ExecuteNonQuery();
                    mensaje = "Se grabo un nuevo apostador";
                }
            } catch (Exception ex)
            {
                _logger.Error("Error in CreateNewApostador",ex);
            }
            finally
            {
                DisposeObjects();
            }
            return mensaje;
        }

        public List<Apostador> GetApostadores()
        {
            List<Apostador> apostadores = null;
            try
            {
                //para obtener el Equipo
                queryString = "SELECT Apostador_Id,Nombre,Correo,dni,puntaje " +
                              "FROM tb_apostador ORDER BY puntaje DESC";
                reader = ExecuteReader();
                apostadores = new List<Apostador>();
                while (reader.Read())
                {
                    Apostador apostador = new Apostador();
                    apostador.ApostadorId = int.Parse(reader["Apostador_Id"].ToString());
                    apostador.Nombre = reader["Nombre"].ToString();
                    apostador.Correo = reader["Correo"].ToString();
                    apostador.DNI = reader["dni"].ToString();
                    apostador.Puntaje = int.Parse(reader["puntaje"].ToString());
                    apostadores.Add(apostador);
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                _logger.Error("Error in GetApostadores",ex);
                throw;
            }
            finally
            {
                DisposeObjects();
            }
            return apostadores;
        }

        public Apostador GetApostadorById(int apostadorId)
        {
            Apostador apostador = null;
            try
            {
                //para obtener el Equipo
                queryString = "SELECT Apostador_Id,Nombre,Correo,dni,puntaje " +
                              "FROM tb_apostador WHERE Apostador_Id='" + apostadorId + "'";
                reader = ExecuteReader();
                while (reader.Read())
                {
                    apostador = new Apostador();
                    apostador.ApostadorId = int.Parse(reader["Apostador_Id"].ToString());
                    apostador.Nombre = reader["Nombre"].ToString();
                    apostador.Correo = reader["Correo"].ToString();
                    apostador.DNI = reader["dni"].ToString();
                    apostador.Puntaje = int.Parse(reader["puntaje"].ToString());
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                _logger.Error("Error in GetApostadorById",ex);
                throw;
            }
            finally
            {
                DisposeObjects();
            }
            return apostador;
        }

        public int GetApostadorLastId()
        {
            int ApostadorId = 0;
            try
            {
                queryString = "Select MAX(Apostador_Id) from tb_apostador";
                ApostadorId = int.Parse(ExecuteScalar());
            } catch (Exception ex)
            {
                _logger.Error("Error in GetApostadorLastId",ex);
            }
            finally
            {
                DisposeObjects();
            }
            return ApostadorId;
        }

        public Apostador GetApostadorByDni(int apostadorDNI)
        {
            Apostador apostador = null;
            try
            {
                //para obtener el Equipo
                queryString = "SELECT Apostador_Id,Nombre,Correo,dni,puntaje " +
                              "FROM tb_apostador WHERE dni='" + apostadorDNI + "'";
                reader = ExecuteReader();
                while (reader.Read())
                {
                    apostador = new Apostador();
                    apostador.ApostadorId = int.Parse(reader["Apostador_Id"].ToString());
                    apostador.Nombre = reader["Nombre"].ToString();
                    apostador.Correo = reader["Correo"].ToString();
                    apostador.DNI = reader["dni"].ToString();
                    apostador.Puntaje = int.Parse(reader["puntaje"].ToString());
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                _logger.Error("Error in GetApostadorByDni", ex);
                throw;
            }
            finally
            {
                DisposeObjects();
            }
            return apostador;
        }
    }
}