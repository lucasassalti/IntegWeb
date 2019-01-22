using IntegWeb.Entidades;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Data;

namespace IntegWeb.Financeira.Aplicacao.DAL.Tesouraria
{
    public class ControleFlagsDAL
    {
        public DataTable consultaGrid(int codEmpr)
        {
            ConexaoOracle obj = new ConexaoOracle();
            DataTable dt = new DataTable();

            try
            {
                OracleDataAdapter adpt = obj.ExecutarQueryAdapter("select s.cod_emprs, s.num_rgtro_emprg, s.num_idntf_rptant, s.nom_emprg_repres, s.dt_inclusao,s.dt_exclusao, s.nom_solic_inclusao,s.flag_judicial,s.flag_insucesso  from opportunity.tb_nao_gerar_cobranca_saude s where s.cod_emprs= " + codEmpr + "");
                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas com a conexão com o banco de dados, favor entrar em contato com Administrador do sistema " + ex.Message);
            }
            return dt;
        }

        public DataTable consultaGridMatr(int codMatr)
        {
            ConexaoOracle obj = new ConexaoOracle();
            DataTable dt = new DataTable();

            try
            {
                OracleDataAdapter adpt = obj.ExecutarQueryAdapter("select s.cod_emprs, s.num_rgtro_emprg, s.num_idntf_rptant, s.nom_emprg_repres, s.dt_inclusao,s.dt_exclusao, s.nom_solic_inclusao,s.flag_judicial,s.flag_insucesso  from opportunity.tb_nao_gerar_cobranca_saude s where s.num_rgtro_emprg= " + codMatr + "");
                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas com a conexão com o banco de dados, favor entrar em contato com Administrador do sistema " + ex.Message);
            }
            return dt;
        }
        
        
        public DataTable mostrarGridDal(String nome)
        {
            ConexaoOracle obj = new ConexaoOracle();
            DataTable dt = new DataTable();
            String upString = nome.ToUpper();

            try
            {
                OracleDataAdapter adpt = obj.ExecutarQueryAdapter("select s.cod_emprs, s.num_rgtro_emprg, s.num_idntf_rptant,s.nom_emprg_repres,s.dt_inclusao,s.dt_exclusao,s.nom_solic_inclusao,s.flag_judicial,s.flag_insucesso  from opportunity.tb_nao_gerar_cobranca_saude s where upper(s.nom_emprg_repres) like '" + upString + "%'");
                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas com a conexão com o banco de dados, favor entrar em contato com Administrador do sistema " + ex.Message);
            }

            return dt;
        }

        public DataTable geraGridGeral()

        {
            ConexaoOracle obj = new ConexaoOracle();
            DataTable dt = new DataTable();

            try
            {

                OracleDataAdapter adpt = obj.ExecutarQueryAdapter("SELECT n.cod_emprs, n.num_rgtro_emprg, n.num_idntf_rptant, n.nom_emprg_repres, n.dt_inclusao,n.dt_exclusao ,n.nom_solic_inclusao,n.flag_judicial,n.flag_insucesso from opportunity.tb_nao_gerar_cobranca_saude n order by n.dt_inclusao");
                adpt.Fill(dt);
                adpt.Dispose();
            }   //where n.dt_exclusao is null
            catch(Exception ex)
            {
                throw new Exception("Problemas com a conexão com o banco de dados, favor entrar em contato com Administrador do sistema " + ex.Message);
            }
            return dt;
        }

        public void deletaLinha(int num_rgtro, int cod_emprs, string exclusao)
        {
            ConexaoOracle obj = new ConexaoOracle();
            DataTable dt = new DataTable();

            try
            {
                obj.AdicionarParametro("reg_rgtro_emprg", num_rgtro);
                obj.AdicionarParametro("reg_cod_emprs", cod_emprs);
                obj.AdicionarParametro("v_log_exclusao", exclusao);
                
                bool retorno = obj.ExecutarNonQuery("own_funcesp.PKG_CONTROLE_FLAG.prc_del_matr_emp");

            }
            catch (Exception ex)
            {

                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);

            }
        }

        public void InsereLinha(int cod_emprs, int num_rgtro, int num_idntf_rptant, string nom_emprg_repres, string log_inclusao, string flag_judicial,string flag_insucesso)

            {

            ConexaoOracle obj = new ConexaoOracle();
            DataTable dt = new DataTable();

            try

                
            {
                obj.AdicionarParametro("v_cod_emprs", cod_emprs);
                obj.AdicionarParametro("v_rgtro_emprg", num_rgtro);
                obj.AdicionarParametro("v_num_idntf_rptant", num_idntf_rptant);
                obj.AdicionarParametro("v_nom_emprg_repres", nom_emprg_repres);
                obj.AdicionarParametro("v_log_inclusao", log_inclusao);
                obj.AdicionarParametro("v_flag_judicial", flag_judicial);
                obj.AdicionarParametro("v_flag_insucesso", flag_insucesso);
              

                bool retorno = obj.ExecutarNonQuery("own_funcesp.PKG_CONTROLE_FLAG.prc_inserir");

            }

            catch (Exception ex)

            {


                throw new Exception ("Já existem registros com essas informações." + ex.Message);
         

                
            }
        }
    }   
}
