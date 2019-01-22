using IntegWeb.Framework;
using IntegWeb.Intranet.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Intranet.Aplicacao.DAL
{
    public class RetiraFolhaFestaAposentadoDAL
    {
        INTRA_Entity_Conn m_DbContext = new INTRA_Entity_Conn();

        ConexaoOracle objConexao = new ConexaoOracle();
        protected DataTable selecionaUsuarioMatricula(string matricula)
        {
            DataTable dt = new DataTable();
            try
            {
                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter("SELECT * FROM OWN_FUNCESP.FUN_TBL_ACESSO_PAGFOLHA WHERE NUM_RGTRO_EMPRG = '" + matricula + "'");
                adpt.Fill(dt);
                adpt.Dispose();

            }catch(Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }

            return dt;
        }

        protected DataTable selecionaUsuarioNome(string nome)
        {
            DataTable dt = new DataTable();
            try
            {
                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter("SELECT * FROM OWN_FUNCESP.FUN_TBL_ACESSO_PAGFOLHA WHERE NOM_EMPRG LIKE '" + nome.ToUpper() + "%'");
                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            return dt;
        }

        protected void insereUsuario(string matricula, string nome)
        {
            try
            {
                 objConexao.ExecutarNonQuery("INSERT INTO OWN_FUNCESP.FUN_TBL_ACESSO_PAGFOLHA(COD_EMPRS,NUM_RGTRO_EMPRG,ID,NOM_EMPRG) VALUES (4,'"+matricula+"',(SELECT NVL(MAX(ID),0)+1 FROM OWN_FUNCESP.FUN_TBL_ACESSO_PAGFOLHA),'"+ nome +"')");
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }

        }

        protected DataTable consultaNome()
        {
            DataTable dt = new DataTable();
            try
            {
                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter("SELECT * FROM OWN_FUNCESP.FUN_TBL_ACESSO_PAGFOLHA");
                
                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }

            return dt;
        }

        protected void excluiUsuario(string matricula)
        {
            try
            {
                objConexao.ExecutarNonQuery("DELETE OWN_FUNCESP.FUN_TBL_ACESSO_PAGFOLHA WHERE NUM_RGTRO_EMPRG = '" + matricula + "'");
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
        }

    }
}
