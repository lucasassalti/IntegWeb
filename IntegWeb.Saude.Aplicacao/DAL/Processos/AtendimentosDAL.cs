using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Entidades.Saude.Processos;
using IntegWeb.Entidades;
using IntegWeb.Framework;

namespace IntegWeb.Saude.Aplicacao.DAL
{
    public class AtendimentosDAL
    {
        List<QtdAtendimentos> lstQA = new List<QtdAtendimentos>();

        public List<QtdAtendimentos> GetDataDetalhado(int startRowIndex, int maximumRows, string paramCodEmpresa, string paramNumMatricula, string paramNumSubMatricula, string paramDtIni, string paramDtFim, string paramNumProcedimento, string sortParameter)
        {
            List<QtdAtendimentos> getAtendimentoDetalhado = new List<QtdAtendimentos>();

            if (String.IsNullOrEmpty(paramNumMatricula)) paramNumMatricula = "";
            if (String.IsNullOrEmpty(paramNumSubMatricula)) paramNumSubMatricula = "";

            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_COD_EMPRS", paramCodEmpresa);
                objConexao.AdicionarParametro("P_NUM_MATRICULA", paramNumMatricula);
                objConexao.AdicionarParametro("P_NUM_SUB_MATRIC", paramNumSubMatricula);
                objConexao.AdicionarParametro("P_DATA_INI", paramDtIni);
                objConexao.AdicionarParametro("P_DATA_FIM", paramDtFim);
                objConexao.AdicionarParametro("P_NUM_PROCEDIMENTO", paramNumProcedimento);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_ATENDIMENTOS.QUANTIDADE_PROC_DETALHADO");
                adpt.Fill(dt);
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

            lstQA.Clear();
            foreach (DataRow drRow in dt.Rows)
            {
                QtdAtendimentos objItem = new QtdAtendimentos();

                objItem.anoFatura = drRow["ano_fatura"].ToString();
                objItem.numSeqFatura = drRow["num_seq_fatura"].ToString();


                objItem.num_matricula = drRow["MATRICULA"].ToString();
                objItem.num_sub_matricula = drRow["SUBMATRICULA"].ToString();
                objItem.cod_empresa = Convert.ToInt32(drRow["COD_EMPRS"]);
                objItem.nome_participante = drRow["PARTICIPANTE"].ToString();
                objItem.procedimento = drRow["PROCEDIMENTO"].ToString();
                objItem.qtd_recurso = decimal.Parse(drRow["QTD"].ToString());
                lstQA.Add(objItem);
            }

            return lstQA.AsQueryable()
                        .GetData(startRowIndex, maximumRows, sortParameter)
                        .ToList();

        }



        public List<QtdAtendimentos> GetData(int startRowIndex, int maximumRows, string paramCodEmpresa, string paramNumMatricula, string paramNumSubMatricula, string paramDtIni, string paramDtFim, string paramNumProcedimento, string sortParameter)
        {

            if (String.IsNullOrEmpty(paramNumMatricula)) paramNumMatricula = "";
            if (String.IsNullOrEmpty(paramNumSubMatricula)) paramNumSubMatricula = "";

            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                //objConexao.AdicionarParametro("P_COD_EMPRS", paramCodEmpresa);
                objConexao.AdicionarParametro("P_NUM_MATRICULA", paramNumMatricula);
                objConexao.AdicionarParametro("P_NUM_SUB_MATRIC", paramNumSubMatricula);
                objConexao.AdicionarParametro("P_DATA_INI", paramDtIni);
                objConexao.AdicionarParametro("P_DATA_FIM", paramDtFim);
                objConexao.AdicionarParametro("P_NUM_PROCEDIMENTO", paramNumProcedimento);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_ATENDIMENTOS.QUANTIDADE_PROCEDIMENTOS");
                adpt.Fill(dt);
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

            lstQA.Clear();
            foreach (DataRow drRow in dt.Rows)
            {
                QtdAtendimentos objItem = new QtdAtendimentos();
                objItem.num_matricula = drRow["num_matricula"].ToString();

                
                objItem.num_sub_matricula = drRow["num_sub_matric"].ToString();
                objItem.cod_empresa = Convert.ToInt32(drRow["COD_EMPRS"]);
                objItem.nome_participante = drRow["NOM_PARTICIP"].ToString();
                objItem.procedimento = drRow["PROCEDIMENTO"].ToString();
                objItem.qtd_recurso = decimal.Parse(drRow["QTD_RECURSO"].ToString());
                lstQA.Add(objItem);
            }

            return lstQA.AsQueryable()
                        .GetData(startRowIndex, maximumRows, sortParameter)
                        .ToList();
        }

        public int GetDataCount()
        {
            return lstQA.Count();
        }

    }
}
