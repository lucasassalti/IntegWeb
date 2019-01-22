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
    public class ExtratoComponenteDAL
    {

        public class ExtratoComponente
        {
            public string empresa { get; set; }
            public string registro { get; set; }
            public string usuario { get; set; }
            public string num_sub_matric { get; set; }
            public string periodo { get; set; }
            public string data_inicio { get; set; }
            public string data_fim { get; set; }
            public string total_servicos { get; set; }
            public string valor_total { get; set; }

        }

        public ExtratoComponente Consultar(int CodEmpresa, int CodMatricula, int NumIdntfRptant)
        {

            ExtratoComponente cRetorno = new ExtratoComponente();
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("anCodEmprs", CodEmpresa);
                objConexao.AdicionarParametro("anNumRgtroEmprg", CodMatricula);
                objConexao.AdicionarParametro("anNumIdntfRptant", NumIdntfRptant);
                objConexao.AdicionarParametro("asSubMatric", "00");
                objConexao.AdicionarParametro("adSemestre", ' ');
                objConexao.AdicionarParametro("adNumAno", ' ');
                objConexao.AdicionarParametro("asQuadro", 1);
                objConexao.AdicionarParametroCursor("srcReturn");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OWN_FUNCESP.SAU_PRC_COMP_EXTRATO_UTIL");

                adpt.Fill(dt);
                adpt.Dispose();

                if (dt.Rows.Count > 0)
                {
                    cRetorno.empresa = CodEmpresa.ToString();
                    cRetorno.registro = CodMatricula.ToString();
                    cRetorno.periodo = dt.Rows[0]["periodo"].ToString();
                    cRetorno.data_inicio = dt.Rows[0]["datincio"].ToString();
                    cRetorno.data_fim = dt.Rows[0]["datfim"].ToString();
                    cRetorno.valor_total = dt.Rows[0]["valortotal"].ToString();
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

        public DataTable Listar(int CodEmpresa, int CodMatricula, int NumIdntfRptant, string NumSubMatric, Int16 Semestre, int NumAno)
        {

            ExtratoUtilizacao cRetorno = new ExtratoUtilizacao();
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("anCodEmprs", CodEmpresa);
                objConexao.AdicionarParametro("anNumRgtroEmprg", CodMatricula);
                objConexao.AdicionarParametro("anNumIdntfRptant", NumIdntfRptant);
                objConexao.AdicionarParametro("asSubMatric", NumSubMatric);
                objConexao.AdicionarParametro("adSemestre", Semestre);
                objConexao.AdicionarParametro("adNumAno", NumAno);
                objConexao.AdicionarParametro("asQuadro", 1);
                objConexao.AdicionarParametroCursor("srcReturn");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OWN_FUNCESP.SAU_PRC_COMP_EXTRATO_UTIL");

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

        public Decimal ListarTotal(int CodEmpresa, int CodMatricula, int NumIdntfRptant, string NumSubMatric, Int16 Semestre, int NumAno)
        {
            Decimal TOTALGERAL = 0;
            ExtratoUtilizacao cRetorno = new ExtratoUtilizacao();
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("anCodEmprs", CodEmpresa);
                objConexao.AdicionarParametro("anNumRgtroEmprg", CodMatricula);
                objConexao.AdicionarParametro("anNumIdntfRptant", NumIdntfRptant);
                objConexao.AdicionarParametro("asSubMatric", NumSubMatric);
                objConexao.AdicionarParametro("adSemestre", Semestre);
                objConexao.AdicionarParametro("adNumAno", NumAno);
                objConexao.AdicionarParametro("asQuadro", 11);
                objConexao.AdicionarParametroCursor("srcReturn");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OWN_FUNCESP.SAU_PRC_COMP_EXTRATO_UTIL");

                adpt.Fill(dt);
                adpt.Dispose();

                if (dt.Rows.Count>0)
                {
                    Decimal.TryParse((dt.Rows[0][0] ?? "").ToString(), out TOTALGERAL);
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
            return TOTALGERAL;
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

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OWN_FUNCESP.SAU_PRC_COMP_EXTRATO_UTIL");

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

     }
}