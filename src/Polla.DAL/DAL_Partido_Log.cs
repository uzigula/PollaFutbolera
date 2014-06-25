using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polla.DAL.Models;
using log4net;

namespace Polla.DAL
{
    public class DAL_Partido_Log : DAL_Base
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(DAL_Partido_Log));

        DAL_Partido dal_partido = new DAL_Partido();
        public List<PartidoLog> GetPartidos()
        {
            List<PartidoLog> list = new List<PartidoLog>();

            try
            {
                //para obtener el Equipo
                queryString = " SELECT Id,PartidoId,UserID,UpdateDate FROM tb_partido_log ";
                reader = ExecuteReader();
                while (reader.Read())
                {
                    PartidoLog obj = new PartidoLog();
                    obj.Id = int.Parse(reader["Id"].ToString());
                    obj.PartidoId = int.Parse(reader["PartidoId"].ToString());
                    obj.UserId = reader["UserID"].ToString();
                    obj.UpdateDate = DateTime.Parse(reader["UpdateDate"].ToString());
                    obj.Partido = dal_partido.GetPartidoViewByPartidoId(obj.PartidoId);
                    list.Add(obj);
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                _logger.Error("Error in GetPartidos", ex);
                throw;
            }
            finally
            {
                DisposeObjects();
            }
            return list;
        }

        public string CreateNewLog(PartidoLog log)
        {
            string mensaje;
            try
            {
                queryString = " INSERT INTO tb_partido_log VALUES(" + log.PartidoId + ",'" + log.UserId + "','" +
                              "" + log.UpdateDate.Year.ToString() + "" +
                              "" +
                              ((log.UpdateDate.Month < 10)
                                   ? "0" + log.UpdateDate.Month.ToString()
                                   : log.UpdateDate.Month.ToString()) + "" +
                              "" +
                              ((log.UpdateDate.Day < 10)
                                   ? "0" + log.UpdateDate.Day.ToString()
                                   : log.UpdateDate.Day.ToString()) + "" +
                              " " +
                              ((log.UpdateDate.Hour < 10)
                                   ? "0" + log.UpdateDate.Hour.ToString()
                                   : log.UpdateDate.Hour.ToString()) + ":" +
                              "" +
                              ((log.UpdateDate.Minute < 10)
                                   ? "0" + log.UpdateDate.Minute.ToString()
                                   : log.UpdateDate.Minute.ToString()) + ":" +
                              "" +
                              ((log.UpdateDate.Second < 10)
                                   ? "0" + log.UpdateDate.Second.ToString()
                                   : log.UpdateDate.Second.ToString()) + "')";
                ExecuteNonQuery();
                mensaje = "Se creo un log para la actualizacion del partido";
            }
            catch (Exception ex)
            {
                _logger.Error("Error in CreateNewLog", ex);
                throw;
            }
            finally
            {
                DisposeObjects();
            }
            return mensaje;
        }

    }
}
