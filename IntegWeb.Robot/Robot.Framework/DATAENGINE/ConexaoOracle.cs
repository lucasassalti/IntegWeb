using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Data.OracleClient;

namespace Robot.Framework
{
    public class ConexaoOracle : IDisposable
    {
        private OracleConnection m_conexao;
        private OracleCommand m_comando;

        public ConexaoOracle()
        {
            m_conexao = new OracleConnection();
            m_conexao.ConnectionString = ConfigAplication.GetConnectString();

            m_comando = new OracleCommand();
            m_comando.CommandType = CommandType.StoredProcedure;
            m_comando.Connection = m_conexao;

            m_conexao.Open();
        }

        public void Dispose()
        {
            m_comando.Dispose();

            m_conexao.Close();
            m_conexao.Dispose();
        }

        public void AdicionarParametro(string nome, object valor)
        {
            m_comando.Parameters.AddWithValue(nome, valor);
        }

        public void AdicionarListaParametros(Dictionary<string, object> lstParam)
        {            
            foreach (KeyValuePair<string, object> key in lstParam)
            {
                m_comando.Parameters.AddWithValue(key.Key, key.Value);
            }            
        }

        public void LimpaParametros() {

            m_comando.Parameters.Clear();

        }

        public void AdicionarParametroOut(string nome, OracleType OTYPE)
        {
            OracleParameter parametro = new OracleParameter();
            parametro.ParameterName = nome;
            parametro.OracleType = OTYPE;
            if (OTYPE == OracleType.Number || OTYPE == OracleType.Float)
            {
                parametro.Size = 10;
            }
            else
            {
                parametro.Size = 1000;
            }
            parametro.Direction = ParameterDirection.Output;
            m_comando.Parameters.Add(parametro);
        }

        public void AdicionarParametroOut(string nome)
        {
            OracleParameter parametro = new OracleParameter();

            parametro.ParameterName = nome;
            parametro.OracleType = OracleType.Number;
            parametro.Size = 10;
            parametro.Direction = ParameterDirection.Output;

            m_comando.Parameters.Add(parametro);
        }

        public void AdicionarParametroCursor(string nome)
        {
            OracleParameter parametro = new OracleParameter();

            parametro.ParameterName = nome;
            parametro.OracleType = OracleType.Cursor;
            parametro.Direction = ParameterDirection.Output;

            m_comando.Parameters.Add(parametro);
        }

        public OracleParameterCollection ParametrosOut()
        {
            return m_comando.Parameters;
        }

        public OracleParameter ReturnParemeterOut()
        {
            OracleParameter param = new OracleParameter();
            for (var cont = 0; cont < m_comando.Parameters.Count; cont++)
            {
                if (m_comando.Parameters[cont].Direction == ParameterDirection.Output)
                {
                    param = m_comando.Parameters[cont];
                }
            }
            return param;
        }

        public OracleDataReader ObterLeitor(string nomeProcedure)
        {
            m_comando.CommandText = nomeProcedure;

            return m_comando.ExecuteReader();
        }

        public void ExecutarDML(string nomeProcedure)
        {
            m_comando.CommandText = nomeProcedure;

            m_comando.ExecuteNonQuery();
        }

        public int ExecutarDMLOutput(string nomeProcedure, string paramOUT)
        {
            m_comando.CommandText = nomeProcedure;

            m_comando.ExecuteNonQuery();

            return Convert.ToInt32(m_comando.Parameters[paramOUT].Value);
        }

        public OracleDataAdapter ExecutarAdapter(string nomeProcedure)
        {
            m_comando.CommandText = nomeProcedure;

            // 01/07/2015 - Guilherme Provenzano
            // Linha abaixo comentada para evitar a execução do comando execute 2 vezes:

            //m_comando.ExecuteNonQuery();

            // 
            // new instace do Objeto OracleDataAdapter já excuta o ExecuteNonQuery automaticamente:
            //

            return new OracleDataAdapter(m_comando);
        }

        public string ConnectionStrings { get; set; }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public bool ExecutarNonQuery(string nomeProcedure)
        {
            bool retorno = false;
            m_comando.CommandText = nomeProcedure;
            retorno = m_comando.ExecuteNonQuery() > 0;
            return retorno;            
        }

        public object ExecutarScalar(string queryString)
        {
            object retorno = null;
            m_comando.CommandText = queryString;
            m_comando.CommandType = CommandType.Text;
            retorno = m_comando.ExecuteOracleScalar();
            return retorno;            
        }

        public OracleDataAdapter ExecutarQueryAdapter(string queryString)
        {
            m_comando.CommandText = queryString;
            m_comando.CommandType = CommandType.Text;
            return new OracleDataAdapter(m_comando);
        }

        public static string ConnectionString { get; set; }
    }
}
