//-----------------------------------------------------------------------
// <copyright file="RegistroBroker.cs" company="Notaria Sahagun">
//     Copyright (c) Notaria Sahagun. All rights reserved.
// </copyright>
// <author>Carlos Andrés Rodriguez</author>
//-----------------------------------------------------------------------
namespace NotariaSahagun.RegistroCivil.Negocio.Brokers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using System.Data.Common;
    using System.Data;
    using NotariaSahagun.RegistroCivil.Soporte.Entidades;
    using System.Configuration;

    internal class RegistroBroker
    {        
        /// <summary>
        /// Permite ingresar un registro civil.
        /// </summary>
        /// <param name="primerApellido">Primer Apellido de la persona registrada.</param>
        /// <param name="segundoApellido">Segundo apellido de la persona registrada.</param>
        /// <param name="primerNombre">Primer Nombre de la persona registrada.</param>
        /// <param name="segundoNombre">Segundo Nombre de la persona registrada.</param>
        /// <param name="ano">Año en el cual se registro la persona.</param>
        /// <param name="indSerial">Numero serial con el cual fue registrada la persona.</param>
        /// <returns>Nuevo registro.</returns>
        public static bool IngresarRegistro(string primerApellido, string segundoApellido, string primerNombre, string segundoNombre, int ano, int indSerial)
        {
            string connectionName = "default";
            bool canAdd = false;

            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings[connectionName];

            if (connectionString == null)
            {
                return (false);
            }

            DbProviderFactory factory = DbProviderFactories.GetFactory(connectionString.ProviderName);

            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString.ConnectionString;
                connection.Open();

                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "procRegistroInsert";

                    DbParameter paramPrimerAp = command.CreateParameter();
                    paramPrimerAp.ParameterName = "@PrimerApellido";
                    paramPrimerAp.Value = primerApellido;
                    command.Parameters.Add(paramPrimerAp);

                    DbParameter paramSegundoAp = command.CreateParameter();
                    paramSegundoAp.ParameterName = "@SegundoApellido";
                    paramSegundoAp.Value = segundoApellido;
                    command.Parameters.Add(paramSegundoAp);

                    DbParameter paramPrimerNombre = command.CreateParameter();
                    paramPrimerNombre.ParameterName = "@PrimerNombre";
                    paramPrimerNombre.Value = primerNombre;
                    command.Parameters.Add(paramPrimerNombre);

                    DbParameter paramSegundoNombre = command.CreateParameter();
                    paramSegundoNombre.ParameterName = "@SegundoNombre";
                    paramSegundoNombre.Value = segundoNombre;
                    command.Parameters.Add(paramSegundoNombre);

                    DbParameter paramAno = command.CreateParameter();
                    paramAno.ParameterName = "@Ano";
                    paramAno.Value = ano;
                    command.Parameters.Add(paramAno);

                    DbParameter paramIndSerial = command.CreateParameter();
                    paramIndSerial.ParameterName = "@SERIAL";
                    paramIndSerial.Value = indSerial;
                    command.Parameters.Add(paramIndSerial);
                 
                    try
                    {
                        if (command.ExecuteNonQuery() > 0)
                        {
                            canAdd = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al intentar ingresar el registro en la base de datos: " + ex.Message);
                    }
                    finally
                    {
                        command.Parameters.Clear();
                        command.Dispose();
                        connection.Close();
                    }
                }
            }
            return canAdd;
        }
       /// <summary>
       /// Permite consultar registros civiles.
       /// </summary>
       /// <param name="primerApellido">Primer Apellido de la persona registrada.</param>
       /// <param name="primerNombre">Primer Nombre de la persona registrada.</param>
       /// <param name="indSerial">Numero serial con el cual fue registrada la persona.</param>
       /// <returns>Registros civiles.</returns>
        public static List<Registro> ConsultarRegistro(string primerApellido, string primerNombre, int indSerial)
        {
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["default"];
            DbProviderFactory factory = DbProviderFactories.GetFactory(connectionStringSettings.ProviderName);

            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionStringSettings.ConnectionString;
                connection.Open();

                List<Registro> listRegistro = new List<Registro>();

                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "procConsultarRegistro";

                    DbParameter paramPrimerAp = command.CreateParameter();
                    paramPrimerAp.ParameterName = "@PrimerApellido";
                    paramPrimerAp.Value = primerApellido;
                    command.Parameters.Add(paramPrimerAp);

                    DbParameter paramPrimerNombre = command.CreateParameter();
                    paramPrimerNombre.ParameterName = "@PrimerNombre";
                    paramPrimerNombre.Value = primerNombre;
                    command.Parameters.Add(paramPrimerNombre);

                    DbParameter paramIndSerial = command.CreateParameter();
                    paramIndSerial.ParameterName = "@Serial";
                    paramIndSerial.Value = indSerial;
                    command.Parameters.Add(paramIndSerial);

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Registro reg = new Registro();

                            reg.PrimerApellido = reader["1er APELLIDO"].ToString();
                            reg.SegundoApellido = !string.IsNullOrEmpty(reader["2do APELLIDO"].ToString()) ? reader["2do APELLIDO"].ToString() : string.Empty;
                            reg.PrimerNombre = reader["1er. NOMBRE"].ToString();
                            reg.SegundoNombre = !string.IsNullOrEmpty(reader["2do APELLIDO"].ToString()) ? reader["2do APELLIDO"].ToString() : string.Empty;
                            reg.Ano = Int32.Parse(reader["ANO"].ToString());
                            reg.IndicativoSerial = Int32.Parse(reader["SERIAL"].ToString());

                            listRegistro.Add(reg);
                        }
                    }
                }
                return listRegistro;
            }
        }
        /// <summary>
        /// Permite consultar registros civiles.
        /// </summary>
        /// <param name="indSerial">Numero serial con el cual fue registrada la persona.</param>
        /// <returns>Registros civiles.</returns>
        public static List<Registro> ConsultarRegistroConSerial(int indSerial)
        {
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["default"];
            DbProviderFactory factory = DbProviderFactories.GetFactory(connectionStringSettings.ProviderName);

            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionStringSettings.ConnectionString;
                connection.Open();

                List<Registro> listRegistro = new List<Registro>();

                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "procConsultarRegistroConSerial";                    

                    DbParameter paramIndSerial = command.CreateParameter();
                    paramIndSerial.ParameterName = "@Serial";
                    paramIndSerial.Value = indSerial;
                    command.Parameters.Add(paramIndSerial);

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Registro reg = new Registro();

                            reg.PrimerApellido = reader["1er APELLIDO"].ToString();
                            reg.SegundoApellido = !string.IsNullOrEmpty(reader["2do APELLIDO"].ToString()) ? reader["2do APELLIDO"].ToString() : string.Empty;
                            reg.PrimerNombre = reader["1er. NOMBRE"].ToString();
                            reg.SegundoNombre = !string.IsNullOrEmpty(reader["2do APELLIDO"].ToString()) ? reader["2do APELLIDO"].ToString() : string.Empty;
                            reg.Ano = Int32.Parse(reader["ANO"].ToString());
                            reg.IndicativoSerial = Int32.Parse(reader["SERIAL"].ToString());

                            listRegistro.Add(reg);
                        }
                    }
                }
                return listRegistro;
            }
        }
        /// <summary>
        /// Permite consultar registros civiles.
        /// </summary>
        /// <param name="primerApellido">Primer Apellido de la persona registrada.</param>
        /// <param name="primerNombre">Primer Nombre de la persona registrada.</param>
        /// <returns>Registros civiles.</returns>
        public static List<Registro> ConsultarRegistroConNombres(string primerApellido, string primerNombre)
        {
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["default"];
            DbProviderFactory factory = DbProviderFactories.GetFactory(connectionStringSettings.ProviderName);

            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionStringSettings.ConnectionString;
                connection.Open();

                List<Registro> listRegistro = new List<Registro>();

                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "procConsultarRegistroConNombres";

                    DbParameter paramPrimerAp = command.CreateParameter();
                    paramPrimerAp.ParameterName = "@PrimerApellido";
                    paramPrimerAp.Value = primerApellido;
                    command.Parameters.Add(paramPrimerAp);

                    DbParameter paramPrimerNombre = command.CreateParameter();
                    paramPrimerNombre.ParameterName = "@PrimerNombre";
                    paramPrimerNombre.Value = primerNombre;
                    command.Parameters.Add(paramPrimerNombre);

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Registro reg = new Registro();

                            reg.PrimerApellido = reader["1er APELLIDO"].ToString();
                            reg.SegundoApellido = !string.IsNullOrEmpty(reader["2do APELLIDO"].ToString()) ? reader["2do APELLIDO"].ToString() : string.Empty;
                            reg.PrimerNombre = reader["1er. NOMBRE"].ToString();
                            reg.SegundoNombre = !string.IsNullOrEmpty(reader["2do APELLIDO"].ToString()) ? reader["2do APELLIDO"].ToString() : string.Empty;
                            reg.Ano = Int32.Parse(reader["ANO"].ToString());
                            reg.IndicativoSerial = Int32.Parse(reader["SERIAL"].ToString());

                            listRegistro.Add(reg);
                        }
                    }
                }
                return listRegistro;
            }
        }
    }
}
