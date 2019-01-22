using IntegWeb.Entidades.Saude.Cobranca;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL.Cobranca
{
    public class CisaoFusaoDAL 
    {
        public SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();

        public class EMPREGADO_VIEW 
        {
            public string NOM_EMPRG { get; set; }
        }

        public bool Inserir (out string mensagem, CisaoFusao obj)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_COD_EMPRS_ANT", obj.Cod_Emprs_Ant);
                objConexao.AdicionarParametro("P_COD_EMPRS_ATU", obj.Cod_Emprs_Atu);
                objConexao.AdicionarParametro("P_DAT_ATUALIZACAO", obj.Dat_Atualizacao);
                objConexao.AdicionarParametro("P_DAT_BASE_CISAO", obj.Dat_Base_Cisao);
                objConexao.AdicionarParametro("P_NUM_DIGVER_ATU", obj.Num_Digver_Atu);
                objConexao.AdicionarParametro("P_NUM_RGTRO_EMPRG_ANT", obj.Num_Rgtro_Emprg_Ant);
                objConexao.AdicionarParametro("P_NUM_RGTRO_EMPRG_ATU", obj.Num_Rgtro_Emprg_Atu);
                objConexao.AdicionarParametro("P_MATRICULA", obj.matricula);
                mensagem = "Registro Inserido com Sucesso";
                return objConexao.ExecutarNonQuery("SAU_PKG_CISAOFUSAO.INSERIR");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

        }

        public bool Atualizar(out string  mensagem, CisaoFusao obj)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_COD_EMPRS_ANT", obj.Cod_Emprs_Ant);
                objConexao.AdicionarParametro("P_COD_EMPRS_ATU", obj.Cod_Emprs_Atu);
                objConexao.AdicionarParametro("P_DAT_ATUALIZACAO", obj.Dat_Atualizacao);
                objConexao.AdicionarParametro("P_DAT_BASE_CISAO", obj.Dat_Base_Cisao);
                objConexao.AdicionarParametro("P_NUM_DIGVER_ATU", obj.Num_Digver_Atu);
                objConexao.AdicionarParametro("P_NUM_RGTRO_EMPRG_ANT", obj.Num_Rgtro_Emprg_Ant);
                objConexao.AdicionarParametro("P_NUM_RGTRO_EMPRG_ATU", obj.Num_Rgtro_Emprg_Atu);
                objConexao.AdicionarParametro("P_MATRICULA", obj.matricula);
                mensagem = "Registro Atualizado com Sucesso";
                return objConexao.ExecutarNonQuery("SAU_PKG_CISAOFUSAO.ATUALIZAR");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

        }

        public bool Deletar(out string mensagem, CisaoFusao obj)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_COD_EMPRS_ANT", obj.Cod_Emprs_Ant);
                objConexao.AdicionarParametro("P_NUM_RGTRO_EMPRG_ANT", obj.Num_Rgtro_Emprg_Ant);
                objConexao.AdicionarParametro("P_MATRICULA", obj.matricula);
                mensagem = "Registro Deletado com Sucesso";
                return objConexao.ExecutarNonQuery("SAU_PKG_CISAOFUSAO.Deletar");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

        }

        public bool Processar( string matricula)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_MATRICULA", matricula);
               
                return objConexao.ExecutarNonQuery("SAU_PKG_CISAOFUSAO.EXECUTAR_CISAO_FUSAO");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

        }

        public DataTable ListarEmpresa(int cod_empresa)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");
                objConexao.AdicionarParametro("P_COD_EMPRS", cod_empresa );
                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_CISAOFUSAO.LISTAR_EMPRESA");


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

        public String getEmpresaMatricula(decimal cod_empresa, decimal matricula)
        {
            string nome = "";

            try
            {
            
            IQueryable<EMPREGADO_VIEW> query;

            query = from em in m_DbContext.EMPREGADO
                    where em.COD_EMPRS == cod_empresa
                    && em.NUM_RGTRO_EMPRG == matricula
                    select new EMPREGADO_VIEW
                    {
                        NOM_EMPRG = em.NOM_EMPRG
                    };

             
                nome = query.FirstOrDefault().NOM_EMPRG;
            }
            catch(NullReferenceException)
            {
                return nome;
            }

            return nome;
            
           



        }
        
        public DataTable ListarDigito(int emprs,int matricula)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");
                objConexao.AdicionarParametro("P_MATRICULA", matricula);
                objConexao.AdicionarParametro("P_EMPRS", emprs);
                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_CISAOFUSAO.LISTAR_DIGITO");


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

        public DataTable ListarCisao(CisaoFusao obj)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");
                objConexao.AdicionarParametro("P_NUM_RGTRO_EMPRG_ANT", obj.Num_Rgtro_Emprg_Ant);
                objConexao.AdicionarParametro("P_NUM_RGTRO_EMPRG_ATU", obj.Num_Rgtro_Emprg_Atu);
                objConexao.AdicionarParametro("P_MES", obj.mes);
                objConexao.AdicionarParametro("P_ANO", obj.ano);
                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_CISAOFUSAO.LISTAR_CISAO");


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



        public DataTable ListarCisaoCadastro(CisaoFusao obj)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");
                objConexao.AdicionarParametro("P_NUM_RGTRO_EMPRG_ANT", obj.Num_Rgtro_Emprg_Ant);
                objConexao.AdicionarParametro("P_NUM_RGTRO_EMPRG_ATU", obj.Num_Rgtro_Emprg_Atu);
                objConexao.AdicionarParametro("P_MES", obj.mes);
                objConexao.AdicionarParametro("P_ANO", obj.ano);
                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_CISAOFUSAO.LISTAR_CISAO_CADASTRO");


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

        public DataTable ListarCisaoLog()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_CISAOFUSAO.LISTAR_CISAOLOG");


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


    }
}
