using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class MedicamentoController : Controller
    {
        // GET: Medicamento
        public ActionResult GetAll()
        {
            Dictionary<string, object> result = BL.Medicamento.GetAll_OleDB();
            bool resultado = (bool)result["Resultado"];

            if (resultado)
            {
                ML.Medicamento medicamento = new ML.Medicamento();
                medicamento = (ML.Medicamento)result["Medicamento"];
                return View(medicamento);
            }
            else
            {
                string excepcion = (string)result["Excepcion"];
                return View(excepcion);
            }
        }

        [HttpGet]
        public ActionResult Form(int? idMedicamento)
        {
            if (idMedicamento == null)
            {
                Dictionary<string, object> resultP = BL.Presentacion.GetAll();

                bool resultado = (bool)resultP["Resultado"];

                if (resultado)
                {
                    ML.Medicamento medicamento = new ML.Medicamento();

                    medicamento.Presentacion = new ML.Presentacion();
                    medicamento.Presentacion = (ML.Presentacion)resultP["Presentacion"];

                    return View(medicamento);
                }
                else
                {
                    string excepcion = (string)resultP["Excepcion"];
                    return View(excepcion);
                }
            }
            else
            {
                Dictionary<string, object> result = BL.Medicamento.GetById(idMedicamento.Value);

                bool resultado = (bool)result["Resultado"];

                

                if (resultado)
                {
                    ML.Medicamento medicamento = (ML.Medicamento)result["Medicamento"];

                    Dictionary<string, object> resultP = BL.Presentacion.GetAll();
                    bool resultadoP = (bool)resultP["Resultado"];

                    if (resultadoP)
                    {
                        medicamento.Presentacion = (ML.Presentacion)resultP["Presentacion"];

                        return View(medicamento);
                    }
                    else
                    {
                        string excepcion = (string)resultP["Excepcion"];
                        return View(excepcion);
                    }
                }
                else
                {
                    string excepcion = (string)result["Excepcion"];
                    return View(excepcion);
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Medicamento medicamento)
        {
            Dictionary<string, object> result = BL.Medicamento.Add_OleDB(medicamento);

            bool resultado = (bool)result["Resultado"];

            if (resultado)
            {
                ViewBag.Mensaje = "Se ha agregado el medicamento de manera correcta!";
                
            }
            else
            {
                string excepcion = (string)result["Excepcion"];
                ViewBag.Mensaje = "Error! no se pudo completar el registro!";
                
            }

            Dictionary<string, object> resultP = BL.Presentacion.GetAll();
            bool resultadoPresentacion = (bool)resultP["Resultado"];
            if (resultado)
            {
                //ML.Medicamento medicamento = new ML.Medicamento();

                medicamento.Presentacion = new ML.Presentacion();
                medicamento.Presentacion = (ML.Presentacion)resultP["Presentacion"];

                return View(medicamento);
            }
            else
            {
                string excepcion = (string)resultP["Excepcion"];
                return View(excepcion);
            }
        }

        [HttpGet]
        public ActionResult Delete(int idMedicamento)
        {
            Dictionary<string, object> result = BL.Medicamento.Delete(idMedicamento);
            bool resultado = (bool)result["Resultado"];

            if (resultado)
            {
                return RedirectToAction("GetAll");
            }
            else
            {
                string ex = (string)result["Excepcion"];
                return RedirectToAction("GetAll" + ex);
            }
        }

        [HttpGet]
        public ActionResult GetAll2()
        {
            Dictionary<string, object> result = BL.Medicamento.GetAll2();
            bool resultado = (bool)result["Resultado"];

            if( resultado)
            {
                ML.Medicamento medicamento = (ML.Medicamento)result["Medicamento"];
                return View(medicamento);
            }
            else
            {
                string ex = (string)result["Excepcion"];
                return View(ex);
            }
        }
    }
}