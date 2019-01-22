using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Entidades.Saude.Processos;
using IntegWeb.Entidades;
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
    public class ExtratoUtilizacaoDAL
    {

        public ExtratoUtilizacao Consultar(int CodEmpresa, int CodMatricula, int NumIdntfRptant, DateTime DatIni, DateTime DatFim)
        {

            ExtratoUtilizacao cRetorno = new ExtratoUtilizacao();
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("anCodEmprs", CodEmpresa);
                objConexao.AdicionarParametro("anNumRgtroEmprg", CodMatricula);
                objConexao.AdicionarParametro("anNumIdntfRptant", NumIdntfRptant);
                objConexao.AdicionarParametro("adDatIni", DatIni);
                objConexao.AdicionarParametro("adDatFim", DatFim);
                objConexao.AdicionarParametro("adDatMov", DateTime.Now);
                objConexao.AdicionarParametro("anCodPlano", 0);
                objConexao.AdicionarParametro("asSubMatric", ' ');
                objConexao.AdicionarParametro("asQuadro", 1);
                objConexao.AdicionarParametroCursor("srcreturn");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OWN_FUNCESP.SAU_PRC_DADOS_EXTRATO_MENSAL");

                adpt.Fill(dt);
                adpt.Dispose();

                if (dt.Rows.Count > 0)
                {
                    cRetorno.empresa = CodEmpresa.ToString();
                    cRetorno.registro = CodMatricula.ToString();
                    cRetorno.periodo_desconto = dt.Rows[0]["periodo_desconto"].ToString();
                    cRetorno.data_emissao = dt.Rows[0]["data_emissao"].ToString();
                    cRetorno.total_servicos = dt.Rows[0]["total_servicos"].ToString();
                    cRetorno.total_pagar = dt.Rows[0]["total_pagar"].ToString();
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

            ExtratoUtilizacao cRetorno = new ExtratoUtilizacao();
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("anCodEmprs", CodEmpresa);
                objConexao.AdicionarParametro("anNumRgtroEmprg", CodMatricula);
                objConexao.AdicionarParametro("anNumIdntfRptant", NumIdntfRptant);
                objConexao.AdicionarParametro("adDatIni", DatIni);
                objConexao.AdicionarParametro("adDatFim", DatFim);
                objConexao.AdicionarParametro("adDatMov", DateTime.Now);
                objConexao.AdicionarParametro("anCodPlano", 0);
                objConexao.AdicionarParametro("asSubMatric", NumSubMatric);
                objConexao.AdicionarParametro("asQuadro", 1);
                objConexao.AdicionarParametroCursor("srcreturn");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OWN_FUNCESP.SAU_PRC_DADOS_EXTRATO_MENSAL");

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

        public DataTable ListarUsuarios(int CodEmpresa, int CodMatricula, int NumIdntfRptant)
        {

            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("anCodEmprs", CodEmpresa);
                objConexao.AdicionarParametro("anNumRgtroEmprg", CodMatricula);
                objConexao.AdicionarParametro("anNumIdntfRptant", NumIdntfRptant);
                objConexao.AdicionarParametro("adDatIni", DateTime.Now);
                objConexao.AdicionarParametro("adDatFim", DateTime.Now);
                objConexao.AdicionarParametro("adDatMov", DateTime.Now);
                objConexao.AdicionarParametro("anCodPlano", 0);
                objConexao.AdicionarParametro("asSubMatric", " ");
                objConexao.AdicionarParametro("asQuadro", 2);
                objConexao.AdicionarParametroCursor("srcreturn");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OWN_FUNCESP.SAU_PRC_DADOS_EXTRATO_MENSAL");

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

        public int ConsultarQtdRegistrosCarga(DateTime DatMovimento)
        {

            DataTable dt = new DataTable();
            SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();

            var query = from c in m_DbContext.SAU_TBL_EXT_UTIL_DADGER
                        where c.DAT_MOVIMENTO == DatMovimento
                        select c;

            return query.ToList().Count;
        }

        public bool ExecutarCargaExtratoUtilizacao(DateTime DatMovimento)
        {

            bool resultado = false;
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("ld_dat_movimento", DatMovimento);

                resultado = objConexao.ExecutarNonQuery("OWN_FUNCESP.SAU_PRC_CARGA_EXTRATO_MENSAL");

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema://n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

            return resultado;
        }

     }
}