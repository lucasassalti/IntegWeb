using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Entidades.Previdencia.Concessao;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace IntegWeb.Previdencia.Aplicacao.DAL
{
    public class extratoPrevidenciarioDAL
    {

        public ExtratoPrevidenciario Consultar(int CodEmpresa, int txtCodMatricula)
        {

            ExtratoPrevidenciario cRetorno = new ExtratoPrevidenciario();
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("anCodEmprs", CodEmpresa);
                objConexao.AdicionarParametro("anNumRgtroEmprg", txtCodMatricula);
                objConexao.AdicionarParametro("asquadro", 1);
                objConexao.AdicionarParametroCursor("srcreturn");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OWN_PORTAL.PRE_SP_DADOS_EXTRA_CONTR_PI");

                adpt.Fill(dt);
                adpt.Dispose();

                if (dt.Rows.Count > 0)
                {
                    cRetorno.titulo = dt.Rows[0]["titulo"].ToString();
                    cRetorno.empresa = dt.Rows[0]["empresa"].ToString();
                    cRetorno.nome_emprs = dt.Rows[0]["nome_emprs"].ToString();
                    cRetorno.registro = dt.Rows[0]["registro"].ToString();
                    cRetorno.nome = dt.Rows[0]["nome"].ToString();
                    cRetorno.plano = dt.Rows[0]["plano"].ToString();
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

        public DataTable ListaPeriodos(int CodEmpresa, int txtCodMatricula)
        {

            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("anCodEmprs", CodEmpresa);
                objConexao.AdicionarParametro("anNumRgtroEmprg", txtCodMatricula);
                objConexao.AdicionarParametro("asquadro", 16);
                objConexao.AdicionarParametroCursor("srcreturn");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OWN_PORTAL.PRE_SP_DADOS_EXTRA_CONTR_PI");

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

        //public DadosPrevidenciarios ConsultarDadosPrevidenciarios(int CodEmprs, int NumRgtroEmprg)
        //{

        //    DadosPrevidenciarios cRetorno = new DadosPrevidenciarios();
        //    DataTable dt = new DataTable();
        //    ConexaoOracle objConexao = new ConexaoOracle();
        //    try
        //    {
        //        objConexao.AdicionarParametro("P_COD_EMPRS", CodEmprs);
        //        objConexao.AdicionarParametro("P_NUM_RGTRO_EMPRG", NumRgtroEmprg);
        //        objConexao.AdicionarParametroCursor("DADOS");

        //        OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_EXTRATO_PREV.CONSULTA_DADOS_PREV");

        //        adpt.Fill(dt);
        //        adpt.Dispose();

        //        if (dt.Rows.Count > 0)
        //        {
        //            cRetorno.cod_emprs = int.Parse(dt.Rows[0]["cod_emprs"].ToString());
        //            cRetorno.num_rgtro_emprg = int.Parse(dt.Rows[0]["num_rgtro_emprg"].ToString());
        //            cRetorno.num_digvr_emprg = int.Parse(dt.Rows[0]["num_digvr_emprg"].ToString());
        //            cRetorno.nom_emprg = dt.Rows[0]["nom_emprg"].ToString();
        //            cRetorno.nom_patroc = dt.Rows[0]["nom_patroc"].ToString();
        //            cRetorno.dat_admss_emprg = DateTime.Parse(dt.Rows[0]["dat_admss_emprg"].ToString());
        //            cRetorno.Dcr_Tppcp = dt.Rows[0]["Dcr_Tppcp"].ToString();
        //            cRetorno.Dcr_Sitpar = dt.Rows[0]["Dcr_Sitpar"].ToString();
        //            cRetorno.adesao = DateTime.Parse(dt.Rows[0]["adesao"].ToString());
        //            cRetorno.tip_opctribir_adplpr = int.Parse(dt.Rows[0]["tip_opctribir_adplpr"].ToString());
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
        //    }
        //    finally
        //    {
        //        objConexao.Dispose();
        //    }
        //    return cRetorno;
        //}

     }
}