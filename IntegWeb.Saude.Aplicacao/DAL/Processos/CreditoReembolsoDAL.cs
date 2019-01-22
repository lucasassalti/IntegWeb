using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Entidades.Saude.Processos;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace IntegWeb.Saude.Aplicacao.DAL
{
    public class CreditoReembolsoDAL
    {
        
        public CargaCreditoReembolsoTotal getTotalizadorReembolso(String dataInicio, String dataFim)
        {
            SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();
            CargaCreditoReembolsoTotal getTotalizador;
            IEnumerable<CargaCreditoReembolsoTotal> totalizador = m_DbContext.Database.SqlQuery<CargaCreditoReembolsoTotal>
                ("select quantidadeUsuario, valorTotal, quantidadeProtocolo from own_funcesp.vw_totaliza_reemb where  dat_pagamento between to_date('" + dataInicio + " ','dd/mm/yyyy') and  to_date('" + dataFim + " ','dd/mm/yyyy')");

            return getTotalizador = totalizador.FirstOrDefault();
        }

        public CreditoReembolso Consultar(int CodEmpresa, int CodMatricula, int NumIdntfRptant, string NumSubMatric, DateTime DatIni, DateTime DatFim)
        {

            CreditoReembolso cRetorno = new CreditoReembolso();
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("anCodEmprs", CodEmpresa);
                objConexao.AdicionarParametro("anNumRgtroEmprg", CodMatricula);
                objConexao.AdicionarParametro("anNumIdntfRptant", NumIdntfRptant);
                objConexao.AdicionarParametro("asNumSubMatric", NumSubMatric);
                objConexao.AdicionarParametro("adDatIni", DatIni);
                objConexao.AdicionarParametro("adDatFim", DatFim);
                objConexao.AdicionarParametro("adDatPagamento", DateTime.Now);
                objConexao.AdicionarParametro("asQuadro", 2);
                objConexao.AdicionarParametroCursor("srcreturn");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OWN_FUNCESP.SAU_PRC_DADOS_CREDITO_RMB");

                adpt.Fill(dt);
                adpt.Dispose();

                if (dt.Rows.Count > 0)
                {
                    cRetorno.empresa = CodEmpresa.ToString();
                    cRetorno.registro = CodMatricula.ToString();
                    cRetorno.usuario = dt.Rows[0]["usuario"].ToString();
                    cRetorno.num_sub_matric = dt.Rows[0]["num_sub_matric"].ToString();
                    cRetorno.emissao = dt.Rows[0]["emissao"].ToString();
                    cRetorno.previsao_credito = dt.Rows[0]["previsao_credito"].ToString();
                    cRetorno.reembolsado = dt.Rows[0]["reembolsado"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return cRetorno;
        }

        public DataTable Listar(int CodEmpresa, int CodMatricula, int NumIdntfRptant, string NumSubMatric, DateTime DatIni, DateTime DatFim)
        {
            CreditoReembolso cRetorno = new CreditoReembolso();
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("anCodEmprs", CodEmpresa);
                objConexao.AdicionarParametro("anNumRgtroEmprg", CodMatricula);
                objConexao.AdicionarParametro("anNumIdntfRptant", NumIdntfRptant);
                objConexao.AdicionarParametro("asNumSubMatric", NumSubMatric);
                objConexao.AdicionarParametro("adDatIni", DatIni);
                objConexao.AdicionarParametro("adDatFim", DatFim);
                objConexao.AdicionarParametro("adDatPagamento", DateTime.Now);
                objConexao.AdicionarParametro("asQuadro", 2);
                objConexao.AdicionarParametroCursor("srcreturn");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OWN_FUNCESP.SAU_PRC_DADOS_CREDITO_RMB");

                adpt.Fill(dt);
                adpt.Dispose();

                dt.DefaultView.Sort = "previsao_credito DESC";

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

        public DataTable ListarUsuarios(int CodEmpresa, int CodMatricula, int NumIdntfRptant, int Quadro)
        {

            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("anCodEmprs", CodEmpresa);
                objConexao.AdicionarParametro("anNumRgtroEmprg", CodMatricula);
                objConexao.AdicionarParametro("anNumIdntfRptant", NumIdntfRptant);
                objConexao.AdicionarParametro("asNumSubMatric", 0);
                objConexao.AdicionarParametro("adDatIni", DateTime.Now);
                objConexao.AdicionarParametro("adDatFim", DateTime.Now);
                objConexao.AdicionarParametro("adDatPagamento", DateTime.Now);
                objConexao.AdicionarParametro("asQuadro", Quadro);
                objConexao.AdicionarParametroCursor("srcreturn");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OWN_FUNCESP.SAU_PRC_DADOS_CREDITO_RMB");

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
            return dt;
        }

        //public List<VW_USUARIOS_PORTAL> ConsultarRepresentantes(int CodEmpresa, int CodMatricula, int? NumIdntfRptant)
        //{

        //    DataTable dt = new DataTable();
        //    EntitiesConn m_DbContext = new EntitiesConn();

        //    var query = from c in m_DbContext.VW_USUARIOS_PORTAL
        //                where c.COD_EMPRS == CodEmpresa
        //                   && c.NUM_RGTRO_EMPRG == CodMatricula
        //                   && (c.NUM_IDNTF_RPTANT == NumIdntfRptant && NumIdntfRptant != null || NumIdntfRptant == null)
        //                select c;

        //    return query.ToList();
        //}

        public int ConsultarQtdRegistrosCarga(DateTime DatIni, DateTime DatFim)
        {

            DataTable dt = new DataTable();
            SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();

            var query = from c in m_DbContext.SAU_TBL_CREDITO_RMB
                        where c.DAT_PAGAMENTO >= DatIni
                          && c.DAT_PAGAMENTO <= DatFim
                        select c;

            return query.ToList().Count;
        }

        public bool ExecutarCargaCredito(DateTime DatIni, DateTime DatFim)
        {

            bool resultado = false;
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("ad_datini", DatIni);
                objConexao.AdicionarParametro("ad_datfim", DatFim);

                resultado = objConexao.ExecutarNonQuery("OWN_FUNCESP.SAU_PRC_CARGA_CREDITO_RMB");

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

            return resultado;
        }

    }
}