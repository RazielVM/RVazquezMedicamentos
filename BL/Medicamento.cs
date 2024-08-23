using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Medicamento
    {
        public static Dictionary<string, object> GetAll2()
        {
            ML.Medicamento medicamento = new ML.Medicamento();
            Dictionary<string, object> diccionario = new Dictionary<string, object>
            {
                { "Resultado", false },
                { "Excepcion", "" },
                { "Medicamento", medicamento }
            };

            try
            {
                using (DL.RVazquezMedicamentoEntities context = new DL.RVazquezMedicamentoEntities())
                {
                    var registros = context.MedicamentoGetAll().ToList();

                    if (registros != null)
                    {
                        medicamento.Medicamentos = new List<ML.Medicamento>();
                        foreach (var registro in registros)
                        {
                            ML.Medicamento medicamento1 = new ML.Medicamento();

                            medicamento1.IdMedicamento = registro.IdMedicamento;
                            medicamento1.Nombre = registro.Nombre;
                            medicamento1.Descripcion = registro.Descripcion;

                            medicamento1.Presentacion = new ML.Presentacion();
                            medicamento1.Presentacion.NombrePresentacion = registro.Presentacion;
                            medicamento1.Dosis = registro.Dosis;
                            medicamento1.NombreLaboratio = registro.FechaCaducidad.ToString("dd/MM/yyyy");

                            medicamento.Medicamentos.Add(medicamento1);
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

        public static Dictionary<string, object> Add(ML.Medicamento medicamento)
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
                    int medicamentoAgregado = context.MedicamentoAdd(
                        medicamento.Nombre,
                        medicamento.Descripcion,
                        medicamento.Presentacion.IdPresentacion,
                        medicamento.Dosis,
                        medicamento.NombreLaboratio,
                        DateTime.Parse(medicamento.FechaCaducidad));

                    if (medicamentoAgregado > 0)
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
                using (OleDbConnection context = new OleDbConnection(DL_OleDB.DataBaseAcces.GetStringConnection()))
                {
                    context.Open();

                    using(OleDbCommand command = new OleDbCommand("MedicamentoDelete", context))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@IdMedicamento", idMedicamento);

                        int medicamentoEliminado = command.ExecuteNonQuery();

                        if (medicamentoEliminado > 0)
                        {
                            diccionario["Resultado"] = true;
                        }
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

                    if (registro != null)
                    {
                        medicamento.IdMedicamento = registro.IdMedicamento;
                        medicamento.Nombre = registro.Nombre;
                        medicamento.Descripcion = registro.Descripcion;
                        medicamento.Presentacion = new ML.Presentacion
                        {
                            NombrePresentacion = registro.Presentacion,
                            IdPresentacion = registro.IdPresentacion,
                        };
                        //medicamento.Presentacion.NombrePresentacion = registro.Presentacion;
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

                    if (registros != null)
                    {
                        medicamento.Medicamentos = new List<ML.Medicamento>();

                        foreach (var registro in registros)
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

                            medicamento.Medicamentos.Add(medic);
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

        public static Dictionary<string, object> GetAll_OleDB()
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
                using (OleDbConnection context = new OleDbConnection(DL_OleDB.DataBaseAcces.GetStringConnection()))
                {
                    context.Open();

                    using (OleDbCommand command = new OleDbCommand("MedicamentoGetAll", context))
                    {
                        // Configurar el comando para que ejecute un procedimiento almacenado
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                medicamento.Medicamentos = new List<ML.Medicamento>();
                                while (reader.Read())
                                {
                                    ML.Medicamento medicamento1 = new ML.Medicamento
                                    {
                                        IdMedicamento = reader.GetInt32(reader.GetOrdinal("IdMedicamento")),
                                        Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                        Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                        Presentacion = new ML.Presentacion
                                        {
                                            NombrePresentacion = reader.GetString(reader.GetOrdinal("Presentacion"))
                                        },
                                        Dosis = reader.GetString(reader.GetOrdinal("Dosis")),
                                        NombreLaboratio = reader.GetString(reader.GetOrdinal("NombreLaboratorio")),
                                        FechaCaducidad = reader.GetDateTime(reader.GetOrdinal("FechaCaducidad")).ToString("dd/MM/yyyy")
                                    };

                                    //medicamento1.IdMedicamento = reader.GetInt32(reader.GetOrdinal("IdMedicamento"));
                                    //medicamento1.Nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                                    //medicamento1.Descripcion = reader.GetString(reader.GetOrdinal("Descripcion"));
                                    //medicamento1.Presentacion = new ML.Presentacion();
                                    //medicamento1.Presentacion.NombrePresentacion = reader.GetString(reader.GetOrdinal("Presentacion"));
                                    //medicamento1.Dosis = reader.GetString(reader.GetOrdinal("Dosis"));
                                    //medicamento1.NombreLaboratio = reader.GetString(reader.GetOrdinal("NombreLaboratorio"));
                                    //medicamento1.FechaCaducidad = reader.GetDateTime(reader.GetOrdinal("FechaCaducidad")).ToString();
                                    

                                    medicamento.Medicamentos.Add(medicamento1);
                                }
                            }
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

        public static Dictionary<string, object> Add_OleDB(ML.Medicamento medicamento)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>
            {
                { "Excepcion", "" },
                { "Resultado", false }
            };

            try
            {
                using (OleDbConnection context = new OleDbConnection(DL_OleDB.DataBaseAcces.GetStringConnection()))
                {
                    context.Open();

                    using(OleDbCommand command =  new OleDbCommand("MedicamentoAdd", context))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Nombre", medicamento.Nombre);
                        command.Parameters.AddWithValue("@Descripcion", medicamento.Descripcion);
                        command.Parameters.AddWithValue("@Presentacion", medicamento.Presentacion.IdPresentacion);
                        command.Parameters.AddWithValue("@Dosis", medicamento.Dosis);
                        command.Parameters.AddWithValue("@NombreLaboratorio", medicamento.NombreLaboratio);
                        command.Parameters.AddWithValue("@FechaCaducidad", DateTime.Parse(medicamento.FechaCaducidad));

                        // Ejecutar el procedimiento almacenado
                        int medicamentoAgregado = command.ExecuteNonQuery();

                        if (medicamentoAgregado > 0)
                        {
                            diccionario["Resultado"] = true;
                        }

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
