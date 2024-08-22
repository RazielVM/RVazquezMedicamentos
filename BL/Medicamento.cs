using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Medicamento
    {
        public static Dictionary<string, object> Add(ML.Medicamento medicamento)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>
            {
                { "Excepcion", "" },
                { "Resultado", false }
            };

            try
            {
                using(DL.RVazquezMedicamentoEntities context = new DL.RVazquezMedicamentoEntities())
                {
                    int medicamentoAgregado = context.MedicamentoAdd(
                        medicamento.Nombre,
                        medicamento.Descripcion,
                        medicamento.Presentacion.IdPresentacion,
                        medicamento.Dosis,
                        medicamento.NombreLaboratio,
                        DateTime.Parse(medicamento.FechaCaducidad));

                    if(medicamentoAgregado > 0 )
                    {
                        diccionario["Resultado"] = true;
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

        public static Dictionary<string, object> Update(ML.Medicamento medicamento)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>
            {
                { "Excepcion", "" },
                { "Resultado", false }
            };

            try
            {
                using (DL.RVazquezMedicamentoEntities context = new DL.RVazquezMedicamentoEntities())
                {
                    int medicamentoActualizado = context.MedicamentoUpdate(
                        medicamento.IdMedicamento,
                        medicamento.Nombre,
                        medicamento.Descripcion,
                        medicamento.Presentacion.IdPresentacion,
                        medicamento.Dosis,
                        medicamento.NombreLaboratio,
                        DateTime.Parse(medicamento.FechaCaducidad));

                    if (medicamentoActualizado > 0)
                    {
                        diccionario["Resultado"] = true;
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

        public static Dictionary<string, object> Delete(int idMedicamento)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>
            {
                { "Excepcion", "" },
                { "Resultado", false }
            };

            try
            {
                using (DL.RVazquezMedicamentoEntities context = new DL.RVazquezMedicamentoEntities())
                {
                    int medicamentoEliminado = context.MedicamentoDelete(idMedicamento);

                    if(medicamentoEliminado > 0)
                    {
                        diccionario["Resultado"] = true;
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

        public static Dictionary<string, object> GetById(int idMedicamento)
        {
            ML.Medicamento medicamento = new ML.Medicamento();
            Dictionary<string, object> diccionario = new Dictionary<string, object>
            {
                { "Excepcion", "" },
                { "Resultado", false },
                { "Medicamento", medicamento }
            };

            try
            {
                using (DL.RVazquezMedicamentoEntities context = new DL.RVazquezMedicamentoEntities())
                {
                    var registro = context.MedicamentoGetById(idMedicamento).FirstOrDefault();

                    if( registro != null)
                    {
                        medicamento.IdMedicamento = registro.IdMedicamento;
                        medicamento.Nombre = registro.Nombre;
                        medicamento.Descripcion = registro.Descripcion;
                        medicamento.Presentacion.NombrePresentacion = registro.Presentacion;
                        medicamento.Dosis = registro.Dosis;
                        medicamento.NombreLaboratio = registro.NombreLaboratorio;
                        medicamento.FechaCaducidad = registro.FechaCaducidad.ToString("dd/MM/yyyy");

                        diccionario["Resultado"] = true;
                        diccionario["Medicamento"] = medicamento;
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

        public static Dictionary<string, object> GetAll()
        {
            ML.Medicamento medicamento = new ML.Medicamento();
            Dictionary<string, object> diccionario = new Dictionary<string, object>
            {
                { "Excepcion", "" },
                { "Resultado", false },
                { "Medicamento", medicamento }
            };

            try
            {
                using (DL.RVazquezMedicamentoEntities context = new DL.RVazquezMedicamentoEntities())
                {
                    var registros = context.MedicamentoGetAll().ToList();

                    if ( registros != null )
                    {
                        medicamento.Medicamentos = new List<ML.Medicamento>();

                        foreach ( var registro in registros)
                        {
                            ML.Medicamento medic = new ML.Medicamento();

                            medic.IdMedicamento = registro.IdMedicamento;
                            medic.Nombre = registro.Nombre;
                            medic.Descripcion = registro.Descripcion;

                            medic.Presentacion = new ML.Presentacion();
                            medic.Presentacion.NombrePresentacion = registro.Presentacion;
                            medic.Dosis = registro.Dosis;
                            medic.NombreLaboratio = registro.NombreLaboratorio;
                            medic.FechaCaducidad = registro.FechaCaducidad.ToString("dd/MM/yyyy");

                            medicamento.Medicamentos.Add( medic );
                        }

                        diccionario["Resultado"] = true;
                        diccionario["Medicamento"] = medicamento;
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
