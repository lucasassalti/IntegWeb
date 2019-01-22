using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL.Cadastro
{
    public class ExtratorCiDAL
    {
         public SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();

        #region .: Aba 1 :.

        public Resultado ProcessaCI()
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            Resultado res = new Resultado();
            try
            {

                bool result = objConexao.ExecutarNonQuery("carga_nl.carga_carteirinha_carencia");

                if (result == true)
                {
                    res.Sucesso("Processamento Feito com Sucesso");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }
            finally
            {
                objConexao.Dispose();
            }

            return res;
        }

        public void Inserir(FUN_TBL_CONTROLE_PROCESSACI obj)
        {
            obj.ID_CONTROLE = GetMaxPk();
            obj.DAT_PROCESSAMENTO = DateTime.Now;
            m_DbContext.FUN_TBL_CONTROLE_PROCESSACI.Add(obj);
            m_DbContext.SaveChanges();               
        }

        public DateTime? GetDataProcessamento()
        {
            var query = from p in m_DbContext.FUN_TBL_CONTROLE_PROCESSACI
                        where (p.DAT_PROCESSAMENTO == (m_DbContext.FUN_TBL_CONTROLE_PROCESSACI.Where(p1 => p1.ID_CONTROLE == p.ID_CONTROLE).Max(p2 => p2.DAT_PROCESSAMENTO)))
                        select p.DAT_PROCESSAMENTO;

            return query.Max();
        }
        
        public decimal GetMaxPk()
        {
            decimal maxPK = 0;
            maxPK = m_DbContext.FUN_TBL_CONTROLE_PROCESSACI.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_CONTROLE) + 1;
            return maxPK;
        }

        #endregion

        #region .: Aba 2 :.

        /* ARQUIVOS PES - CI VERMELHO */

        public DataTable ArqAdesaoPesCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_ADESAO_PES_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoPesEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_ADESAO_PES_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaPesCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_SEGVIA_PES_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaPesEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_SEGVIA_PES_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        /* ARQUIVOS PES/ AMH - CI AZUL */

        public DataTable ArqAdesaoPesAzulCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_ADESAO_PESAZUL_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoPesAzulEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_ADESAO_PESAZUL_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoAmhEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_ADESAO_AMH_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaPesAzulCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_SEGVIA_PESAZUL_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaPesAzulEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_SEGVIA_PESAZUL_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaAmhEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_SEGVIA_AMH_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        /* ARQUIVOS CI PRATA */

        public DataTable ArqCiPrataCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_CIPRATA_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        //public DataTable ArqCiPrataEmpresa(DateTime vData)
        //{
        //    ConexaoOracle objConexao = new ConexaoOracle();

        //    DataTable dt = new DataTable();

        //    try
        //    {
        //        objConexao.AdicionarParametro("vData", vData);
        //        objConexao.AdicionarParametroCursor("DADOS");
        //        OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_CIPRATA_EMPRESA");

        //        leitor.Fill(dt);
        //        leitor.Dispose();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
        //    }
        //    finally
        //    {
        //        objConexao.Dispose();
        //    }
        //    return dt;
        //}

        /* ARQUIVOS EXTENSIVE */

        public DataTable ArqAdesaoExtensCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_ADESAO_EXTENS_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaExtensCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_SEGVIA_EXTENS_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        /* ARQUIVOS DIGNA OURO/CRISTAL/BRONZE/CESP */

        public DataTable ArqAdesaoDigOuroCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_ADESAO_DIGOURO_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoDigOuroEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_ADESAO_DIGOURO_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaDigOuroCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_SEGVIA_DIGOURO_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaDigOuroEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_SEGVIA_DIGOURO_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoDigCrstCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_ADESAO_DIGCRST_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoDigCrstEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_ADESAO_DIGCRST_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaDigCrstCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_SEGVIA_DIGCRST_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaDigCrstEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_SEGVIA_DIGCRST_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoDigBrnzCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_ADESAO_DIGBRNZ_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoDigBrnzEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_ADESAO_DIGBRNZ_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaDigBrnzCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_SEGVIA_DIGBRNZ_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaDigBrnzEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_SEGVIA_DIGBRNZ_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoDigCespCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_ADESAO_DIGCESP_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoDigCespEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_ADESAO_DIGCESP_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaDigCespCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_SEGVIA_DIGCESP_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaDigCespEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_SEGVIA_DIGCESP_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        /* ARQUIVOS TODOS OS PLANO NOSSO */

        public DataTable ArqAdesaoNossoAmpCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_ADESAO_NOSSOAMP_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoNossoAmpEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_ADESAO_NOSSOAMP_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaNossoAmpCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_SEGVIA_NOSSOAMP_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaNossoAmpEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTAR_SEGVIA_NOSSOAMP_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoNossoConcCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_ADESAO_NOSSOCONC_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoNossoConcEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_ADESAO_NOSSOCONC_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaNossoConcCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_SEGVIA_NOSSOCONC_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaNossoConcEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_SEGVIA_NOSSOCONC_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoNossoConfCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_ADESAO_NOSSOCONF_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoNossoConfEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_ADESAO_NOSSOCONF_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaNossoConfCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_SEGVIA_NOSSOCONF_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaNossoConfEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_SEGVIA_NOSSOCONF_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoNossoIntCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_ADESAO_NOSSOINT_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoNossoIntEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_ADESAO_NOSSOINT_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaNossoIntCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_SEGVIA_NOSSOINT_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaNossoIntEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_SEGVIA_NOSSOINT_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoNossoTotCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_ADESAO_NOSSOTOT_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoNossoTotEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_ADESAO_NOSSOTOT_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaNossoTotCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_SEGVIA_NOSSOTOT_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaNossoTotEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_SEGVIA_NOSSOTOT_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        /* ARQUIVOS NOSSO REGIONAL ENFERMARIA */

        public DataTable ArqAdesaoNossoRegeCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_ADESAO_NOSSOREGE_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoNossoRegeEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_ADESAO_NOSSOREGE_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaNossoRegeCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_SEGVIA_NOSSOREGE_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaNossoRegeEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_SEGVIA_NOSSOREGE_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        /* ARQUIVOS NOSSO REGIONAL APARTAMENTO */

        public DataTable ArqAdesaoNossoRegaCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_ADESAO_NOSSOREGA_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoNossoRegaEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_ADESAO_NOSSOREGA_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaNossoRegaCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_SEGVIA_NOSSOREGA_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaNossoRegaEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_SEGVIA_NOSSOREGA_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

      /* ARQUIVOS NOSSO PERFIL ENFERMARIA */

        public DataTable ArqAdesaoNossoPerfeCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LST_ADESAO_NOSSOPERFE_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoNossoPerfeEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LST_ADESAO_NOSSOPERFE_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaNossoPerfeCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LST_SEGVIA_NOSSOPERFE_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaNossoPerfeEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LST_SEGVIA_NOSSOPERFE_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        /* ARQUIVOS NOSSO PERFIL APARTAMENTO */

        public DataTable ArqAdesaoNossoPerfaCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LST_ADESAO_NOSSOPERFA_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqAdesaoNossoPerfaEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LST_ADESAO_NOSSOPERFA_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaNossoPerfaCorreio(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LST_SEGVIA_NOSSOPERFA_CORREIO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ArqSegViaNossoPerfaEmpresa(DateTime vData)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("vData", vData);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_EXTRATORCI.LISTA_SEGVIA_NOSSOREGA_EMPRESA");

                leitor.Fill(dt);
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
            return dt;
        }
        
        #endregion

        #region .: Aba 3 :.

        #endregion

    }
}
