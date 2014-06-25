using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lib.Web.Mvc.JQuery.JqGrid;
using Polla.DAL.Models;
using Polla.Web.DAL;
using Polla.Web.Utils;
using log4net;

namespace Polla.Web.Controllers
{
    public class PollaController : Controller
    {
        private ILog _logger = LogManager.GetLogger(typeof(PollaController));
        private IDictionary<string, string> _mapper;
        private DataTable _unMappedOrders;

        DAL_repository repository = new DAL_repository();

        public ActionResult Index()
        {
            _logger.Info("Index");
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Partidos(JqGridRequest request)
        {
            _logger.Info("START  Partidos");
            JqGridResponse response;
            try
            {
                int totalRecords = 0;

                List<PartidosView> partidos = repository.dal_partido.GetPartidosView(0);
                totalRecords = partidos.Count();
                //Prepare JqGridData instance
                response = new JqGridResponse()
                {
                    //Total pages count
                    TotalPagesCount = (int)Math.Ceiling((float)totalRecords / (float)request.RecordsCount),
                    //Page number
                    PageIndex = request.PageIndex,
                    //Total records count
                    TotalRecordsCount = totalRecords
                };

                foreach (PartidosView partido in partidos)
                {
                    response.Records.Add(new JqGridRecord(Convert.ToString(partido.PartidoId), new List<object>()
                {
                    partido.PartidoId,
                    partido.EquipoIdLocal,
                    partido.EquipoDescLocal,
                    partido.GolesLocal,
                    partido.Resultado,
                    partido.EquipoIdVisita,
                    partido.GolesVisita,
                    partido.EquipoDescVisita
                }));
                }

            }
            catch (Exception ex)
            {
                _logger.Error("Error : ", ex);
                throw;
            }
            finally
            {

            }
            //Return data as json
            return new JqGridJsonResult() { Data = response };
        }


        [HttpGet]
        public ActionResult Equipo(int value)
        {
            Equipo eq = repository.dal_equipo.GetEquipo(value);
            return View(eq);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetPartidos(JqGridRequest request, int EquipoId)
        {
            int totalRecords = 0;
            List<PartidosView> partidos = repository.dal_partido.GetPartidosView(EquipoId);
            totalRecords = partidos.Count();
            //Prepare JqGridData instance
            JqGridResponse response = new JqGridResponse()
            {
                //Total pages count
                TotalPagesCount = (int)Math.Ceiling((float)totalRecords / (float)request.RecordsCount),
                //Page number
                PageIndex = request.PageIndex,
                //Total records count
                TotalRecordsCount = totalRecords
            };

            foreach (PartidosView partido in partidos)
            {
                response.Records.Add(new JqGridRecord(Convert.ToString(partido.PartidoId), new List<object>()
                {
                    partido.PartidoId,
                    partido.EquipoIdLocal,
                    partido.EquipoDescLocal,
                    partido.GolesLocal,
                    partido.Resultado,
                    partido.EquipoIdVisita,
                    partido.GolesVisita,
                    partido.EquipoDescVisita
                }));
            }

            //Return data as json
            return new JqGridJsonResult() { Data = response };
        }


        [HttpGet]
        public ActionResult Grupos()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetEquipos(JqGridRequest request)
        {
            //int equipoId = ViewBag.ApuestaId;
            int totalRecords = 0;
            List<Equipo> equipos = repository.dal_equipo.GetEquipos(); ;
            totalRecords = equipos.Count();

            //Prepare JqGridData instance
            JqGridResponse response = new JqGridResponse()
            {
                //Total pages count
                TotalPagesCount = (int)Math.Ceiling((float)totalRecords / (float)request.RecordsCount),
                //Page number
                PageIndex = request.PageIndex,
                //Total records count
                TotalRecordsCount = totalRecords
            };

            foreach (Equipo equipo in equipos)
            {
                response.Records.Add(new JqGridRecord(Convert.ToString(equipo.Equipo_ID), new List<object>()
                {
                   equipo.Equipo_ID,
                   equipo.Grupo,
                   equipo.Equipo_desc,
                   equipo.Ptos,
                   equipo.PJ,
                   equipo.PG,
                   equipo.PE,
                   equipo.PP,
                   equipo.GF,
                   equipo.GC,
                   equipo.DG,
                }));
            }

            //Return data as json
            return new JqGridJsonResult() { Data = response };
        }


        [HttpGet]
        public ActionResult Apuestas(int value)
        {
            PartidosView apuesta = repository.dal_partido.GetPartidoViewByPartidoId(value);
            return View(apuesta);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetApuesta(JqGridRequest request, int PartidoId)
        {
            //int equipoId = ViewBag.ApuestaId;
            int totalRecords = 0;
            List<Apuestasview> apuestas = repository.dal_apuesta.GetApuestasByPartido(PartidoId); ;
            totalRecords = apuestas.Count();

            //Prepare JqGridData instance
            JqGridResponse response = new JqGridResponse()
            {
                //Total pages count
                TotalPagesCount = (int)Math.Ceiling((float)totalRecords / (float)request.RecordsCount),
                //Page number
                PageIndex = request.PageIndex,
                //Total records count
                TotalRecordsCount = totalRecords
            };

            foreach (Apuestasview apuesta in apuestas)
            {
                response.Records.Add(new JqGridRecord(Convert.ToString(apuesta.ApuestaId), new List<object>()
                {
                   apuesta.ApuestaId,
                   apuesta.ApostadorId,
                   apuesta.Nombre,
                   apuesta.Puntaje,
                   apuesta.Equipo_Id_local,
                   apuesta.Equipo_desc_local,
                   apuesta.GolesLocal,
                   apuesta.Resultado,
                   apuesta.Golesvisita,
                   apuesta.Equipo_Id_visita,
                   apuesta.Equipo_desc_visita,
                   apuesta.PartidoId
                }));
            }

            //Return data as json
            return new JqGridJsonResult() { Data = response };
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdatePartido(Partido partido)
        {
            var mensaje = "";
            mensaje = repository.dal_partido.UpdatePartido(partido);
            PartidoLog log = new PartidoLog();
            log.PartidoId = partido.Partido_ID;
            log.UpdateDate = DateTime.Now;
            log.UserId = Environment.MachineName;
            repository.dal_partido_log.CreateNewLog(log);
            verificarPosiciones(partido);
            return new JsonResult() { Data = mensaje };
        }

        private void verificarPosiciones(Partido partido)
        {
            string grupo = repository.dal_equipo.GetEquipo(repository.dal_partido.GetPartido(partido.Partido_ID).Equipo_Local).Grupo;
            int pj = repository.dal_equipo.VerificarPJdeGrupo(grupo);
            if (pj == 12)
            {
                repository.dal_equipo.ActualizarPuntajeDeGrupo(grupo);
            }
        }

        [HttpGet]
        public ActionResult Apostador()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GrabarApostador(Apostador apostador)
        {
            var mensaje = repository.dal_apostador.CreateNewApostador(apostador);
            var ApostadorId = repository.dal_apostador.GetApostadorLastId();
            return new JsonResult() { Data = mensaje + "-" + ApostadorId };
        }


        [HttpGet]
        public ActionResult ApostadorView(int value)
        {
            ViewBag.ApostadorId = value;
            Apostador apostador = new Apostador();
            apostador.ApostadorId = value;
            Apostador data = repository.dal_apostador.GetApostadorById(value);
            return View(data);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetApuestasByApostador(JqGridRequest request, int ApostadorId)
        {
            //int equipoId = ViewBag.ApuestaId;
            int totalRecords = 0;
            List<Apuestasview> apuestas = repository.dal_apuesta.GetApuestasByApostador(ApostadorId); ;
            totalRecords = apuestas.Count();

            //Prepare JqGridData instance
            JqGridResponse response = new JqGridResponse()
            {
                //Total pages count
                TotalPagesCount = (int)Math.Ceiling((float)totalRecords / (float)request.RecordsCount),
                //Page number
                PageIndex = request.PageIndex,
                //Total records count
                TotalRecordsCount = totalRecords
            };

            foreach (Apuestasview apuesta in apuestas)
            {
                response.Records.Add(new JqGridRecord(Convert.ToString(apuesta.ApuestaId), new List<object>()
                {
                   apuesta.ApuestaId,
                   apuesta.ApostadorId,
                   apuesta.Nombre,
                   apuesta.Puntaje,
                   apuesta.Equipo_Id_local,
                   apuesta.Equipo_desc_local,
                   apuesta.GolesLocal,
                   apuesta.Resultado,
                   apuesta.Golesvisita,
                   apuesta.Equipo_Id_visita,
                   apuesta.Equipo_desc_visita,
                   apuesta.PartidoId
                }));
            }

            //Return data as json
            return new JqGridJsonResult() { Data = response };
        }


        [HttpGet]
        public ActionResult NuevasApuestas(int value)
        {
            ViewBag.ApostadorId = value;
            List<PartidosView> partidos = repository.dal_partido.GetPartidosView(0);
            return View(partidos);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GrabarApuestas(List<Apuesta> list)
        {
            var mensaje="";
            try
            {
                foreach (Apuesta item in list)
                {
                    repository.dal_apuesta.CreateNewApuesta(item);
                }
                mensaje = "Se grabaron las apuestas! SUERTE!";
                mandarCorreo(list,list[0].ApostadorId);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                throw;
            }
            return new JsonResult() { Data = mensaje };
        }

        private void mandarCorreo(List<Apuesta> list, int ApostadorId)
        {
            Apostador apostador = repository.dal_apostador.GetApostadorById(ApostadorId);
            EmailMessage email=new EmailMessage();
            email.To = new List<string>();
            email.To.Add("rrojas@belatrixsf.com");
            email.To.Add(apostador.Correo);
            email.To.Add("pollaBelatrix@gmail.com");
            email.From = "pollaBelatrix@gmail.com";
            email.Subject = "Sr(a). "+apostador.Nombre +" su apuesta fue registrada";
            email.EmailServer = "smtp.gmail.com";

            var message = "";
            message = message + "<style>";

            message = message +"table.uitable {color:#333333;width:100%; border: 1px solid #A6C9E2;border-collapse: collapse; font-size: 12px;}"+
                        "table.uitable th {background-color:#F5F8F9;padding: 8px;text-align:left; color: #217BC0; border: 1px solid #A6C9E2; font-size: 12px;} " +
                        "table.uitable tr {background-color:#ffffff; } " +
                        "table.uitable td {border: none; border-right: 1px solid #A6C9E2; padding: 5px} " +
                        "table.uitable tr.odd {background-color: #efefef !important}";
            message = message + "</style>";

            message = message + "<table class=\"uitable\">";
            message = message + "<tr>";
            message = message + "<th>Equipo Local</th>";
            message = message + "<th>Goles Local</th>";
            message = message + "<th>Resultado</th>";
            message = message + "<th>Equipo Visita</th>";
            message = message + "<th>Goles Visita</th>";
            message = message + "</tr>";

            foreach (Apuesta apuesta in list)
            {
                PartidosView partidoView = repository.dal_partido.GetPartidoViewByPartidoId(apuesta.PartidoId);
                message = message + "<tr>";
                message = message + "<td>"+partidoView.EquipoDescLocal+"</td>";
                message = message + "<td>"+apuesta.Goles_Local+"</td>";
                message = message + "<td>"+apuesta.Resultado+"</td>";
                message = message + "<td>"+partidoView.EquipoDescVisita+"</td>";
                message = message + "<td>" + apuesta.Goles_Visita + "</td>";
                message = message + "</tr>";
            }

            message = message + "</table>";

            email.Body = message;
            email.Send();
        }


        [HttpGet]
        public ActionResult Puntajes()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetApostadores(JqGridRequest request)
        {
            //int equipoId = ViewBag.ApuestaId;
            int totalRecords = 0;
            List<Apostador> apuestas = repository.dal_apostador.GetApostadores(); ;
            totalRecords = apuestas.Count();

            //Prepare JqGridData instance
            JqGridResponse response = new JqGridResponse()
            {
                //Total pages count
                TotalPagesCount = (int)Math.Ceiling((float)totalRecords / (float)request.RecordsCount),
                //Page number
                PageIndex = request.PageIndex,
                //Total records count
                TotalRecordsCount = totalRecords
            };

            foreach (Apostador apuesta in apuestas)
            {
                response.Records.Add(new JqGridRecord(Convert.ToString(apuesta.ApostadorId), new List<object>()
                {
                   apuesta.ApostadorId,
                   apuesta.Nombre,
                   apuesta.Correo,
                   apuesta.Puntaje
                }));
            }

            //Return data as json
            return new JqGridJsonResult() { Data = response };
        }



        [HttpGet]
        public ActionResult PartidosUpdate()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetPartidosUpdate(JqGridRequest request)
        {
            //int equipoId = ViewBag.ApuestaId;
            int totalRecords = 0;
            List<PartidoLog> partidos = repository.dal_partido_log.GetPartidos();
            totalRecords = partidos.Count();

            //Prepare JqGridData instance
            JqGridResponse response = new JqGridResponse()
            {
                //Total pages count
                TotalPagesCount = (int)Math.Ceiling((float)totalRecords / (float)request.RecordsCount),
                //Page number
                PageIndex = request.PageIndex,
                //Total records count
                TotalRecordsCount = totalRecords
            };

            foreach (PartidoLog partido in partidos)
            {
                response.Records.Add(new JqGridRecord(Convert.ToString(partido.Id), new List<object>()
                {
                   partido.Id,
                   partido.PartidoId,
                   partido.UserId,
                   partido.UpdateDate.ToString("yy/MM/dd hh:mm:ss"),
                   partido.Partido.EquipoIdLocal,
                   partido.Partido.EquipoDescLocal,
                   partido.Partido.GolesLocal,
                   partido.Partido.Resultado,
                   partido.Partido.EquipoIdVisita,
                   partido.Partido.EquipoDescVisita,
                   partido.Partido.GolesVisita
                }));
            }

            //Return data as json
            return new JqGridJsonResult() { Data = response };
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Ingresar(int dni)
        {
            var mensaje = "";
            Apostador apostador = repository.dal_apostador.GetApostadorByDni(dni);
            if (apostador == null)
            {
                mensaje = "DNI no registrado";
            }
            else
            {
                if (repository.dal_apuesta.GetApuestasByApostador(apostador.ApostadorId).Count == 0)
                    mensaje = "Bienvenido " + apostador.Nombre + "-" + apostador.ApostadorId;
                else
                    mensaje = "Sr(a).  " + apostador.Nombre + ", Ya realizó sus apuestas!" ;
            }

            return new JsonResult() { Data = mensaje };
        }
    }
}
