using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Presentacion
    {
        public static Dictionary<string, object> GetAll()
        {
            ML.Presentacion presentacion = new ML.Presentacion();
            Dictionary<string, object> diccionario = new Dictionary<string, object>
            {
                { "Excepcion", "" },
                { "Resultado", false },
                { "Presentacion", presentacion }
            };

            try
            {
                using (DL.RVazquezMedicamentoEntities context = new DL.RVazquezMedicamentoEntities())
                {
                    var registros = context.PresentacionGetAll().ToList();

                    if (registros != null)
                    {
                        presentacion.Presentaciones = new List<ML.Presentacion>();

                        foreach (var registro in registros)
                        {
                            ML.Presentacion pres = new ML.Presentacion();

                            pres.IdPresentacion = registro.IdPresentacion;
                            pres.NombrePresentacion = registro.Nombre;

                            presentacion.Presentaciones.Add(pres);
                        }
                        diccionario["Resultado"] = true;
                        diccionario["Presentacion"] = presentacion;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }

            return diccionario;
        }
    }
}

