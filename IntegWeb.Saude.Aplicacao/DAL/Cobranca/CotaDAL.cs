using IntegWeb.Entidades.Saude.Cobranca;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL.Saude
{
    internal class CotaDAL
    {
        public List<Cota> ConsultarCota(string  str_cota)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            Cota cota;
            List<Cota> list = new List<Cota>();
            try
            {
                objConexao.AdicionarParametro("P_DATA", str_cota);

                objConexao.AdicionarParametroCursor("dados");

                OracleDataReader leitor = objConexao.ObterLeitor("SAU_PKG_COTA.LISTA_COTA");

                while (leitor.Read())
                {
                    cota = new Cota();

                    cota.dtfimgeracao = leitor["DTFIMGERACAO"].ToString() == "" ? (DateTime?)null : DateTime.Parse(leitor["DTFIMGERACAO"].ToString());
                    cota.dtinigeracao = leitor["DTINIGERACAO"].ToString() == "" ? (DateTime?)null : DateTime.Parse(leitor["DTINIGERACAO"].ToString());
                    cota.dtmov = leitor["DTMOV"].ToString() == "" ? (DateTime?)null : DateTime.Parse(leitor["DTMOV"].ToString());
                    cota.id = leitor["ID"].ToString() == "" ? (int?)null : int.Parse(leitor["ID"].ToString());
                    cota.username = leitor["USERNAME"].ToString();

                    list.Add(cota);
                }

                leitor.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return list;
        }

        public int GeraCota(string login, string dat_mov)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_DTMOV", dat_mov);
                objConexao.AdicionarParametro("P_USERNAME", login );
                objConexao.AdicionarParametroOut("P_RETORNO");
                objConexao.ExecutarNonQuery("SAU_PKG_COTA.GERAR_COTAS");

                return int.Parse(objConexao.ReturnParemeterOut().Value.ToString());

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
        }

        public DataSet ListaExcel(int id)
        {

            DataSet dt = new DataSet();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");
                objConexao.AdicionarParametroCursor("DADOS1");
                objConexao.AdicionarParametroCursor("DADOS2");
                objConexao.AdicionarParametroCursor("DADOS3");
                objConexao.AdicionarParametro("P_ID", id);

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_COTA.LISTAR_EXCEL");

                adpt.Fill(dt);

                dt.Tables[0].TableName = "EMPRESA_COTA";
                dt.Tables[1].TableName = "VAL_EMP_COTA";
                dt.Tables[2].TableName = "VAL_PARTICP_COTA";
                dt.Tables[3].TableName = "EXECUTIVO_COTA";

                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;


        }
    }
}
