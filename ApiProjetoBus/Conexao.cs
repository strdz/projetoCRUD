using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Linq;

namespace ConexaoSQL
{
    class Conexao
    {
        private MySqlConnection conexaoMySql;
        private SqlConnection conexaoSqlServer;
        public Dictionary<string, dynamic> RetornoDadosInsert = null;
        // NOVO

        public DataSet BuscarDados(string consulta = "")
        {
            DataSet dsConsulta = new DataSet();

            conexaoMySql = new MySqlConnection()
            {
                ConnectionString = ConfigurationManager.AppSettings["Conexao"].ToString()
            };

            try
            {

                conexaoMySql.Open();
                MySqlCommand cmdMySql = new MySqlCommand()
                {
                    Connection = conexaoMySql,
                    CommandText = consulta
                };
                MySqlDataAdapter adapter = new MySqlDataAdapter(consulta, conexaoMySql);
                adapter.Fill(dsConsulta);
                conexaoMySql.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conexaoMySql.State == ConnectionState.Open) conexaoMySql.Close();
            }

            return dsConsulta;
        }

        public Int64 InserirDados(string consulta = "")
        {

            conexaoMySql = new MySqlConnection()
            {
                ConnectionString = ConfigurationManager.AppSettings["Conexao"].ToString()
            };

            try
            {

                conexaoMySql.Open();
                MySqlCommand cmdMySql = new MySqlCommand()
                {
                    Connection = conexaoMySql,
                    CommandText = consulta
                };

                int numRowsUpdated = cmdMySql.ExecuteNonQuery();
                cmdMySql.Dispose();
                conexaoMySql.Close();

                return Convert.ToInt64(0);

            }
            catch (Exception ex)
            {
                //Codigo_Excecao = ((System.Data.OracleClient.OracleException)ex).Code;
                //Excecao = ex.Message.ToString();
                return Convert.ToInt64(-1);
                //throw ex;
            }
        }

        public Boolean AlterarDados(string consulta = "")
        {
            Boolean bRetorno = true;

            conexaoMySql = new MySqlConnection()
            {
                ConnectionString = ConfigurationManager.AppSettings["Conexao"].ToString()
            };

            try
            {
                conexaoMySql.Open();
                MySqlCommand cmdMySql = new MySqlCommand()
                {
                    Connection = conexaoMySql,
                    CommandText = consulta
                };

                int numRowsUpdated = cmdMySql.ExecuteNonQuery();
                cmdMySql.Dispose();
                conexaoMySql.Close();

            }
            catch (Exception ex)
            {
                //Codigo_Excecao = ((System.Data.OracleClient.OracleException)ex).Code;
                //Excecao = ex.Message.ToString();

                //var a = "a";
                bRetorno = false;
            }
            return bRetorno;
        }

        public IEnumerable<Dictionary<string, dynamic>> CarregaLista(string consulta = "")
        {

            List<Dictionary<string, dynamic>> retorno = new List<Dictionary<string, dynamic>>();

            conexaoMySql = new MySqlConnection()
            {
                ConnectionString = ConfigurationManager.AppSettings["Conexao"].ToString()
            };

            try
            {

                conexaoMySql.Open();
                MySqlCommand cmdMySql = new MySqlCommand()
                {
                    Connection = conexaoMySql,
                    CommandText = consulta
                };


                using (MySqlDataReader reader = cmdMySql.ExecuteReader())
                {
                    List<string> colunas = new List<string>();


                    for (int contador = 0; contador < reader.FieldCount; contador++)
                    {
                        colunas.Add(reader.GetName(contador));
                    }

                    while (reader.Read())
                    {
                        retorno.Add(colunas.ToDictionary(x => x, x => reader[x]));
                    }
                }

                cmdMySql.Dispose();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conexaoMySql.State == ConnectionState.Open) conexaoMySql.Close();
            }
            return retorno;
        }

    }

}
