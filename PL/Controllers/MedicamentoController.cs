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
            Dictionary<string, object> result = BL.Medicamento.GetAll();
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
        public ActionResult Form()
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
    }
}