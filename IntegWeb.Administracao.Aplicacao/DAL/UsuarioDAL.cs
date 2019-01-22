using IntegWeb.Entidades;
using IntegWeb.Framework;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using IntegWeb.Framework;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Entidades.Framework;

namespace IntegWeb.Administracao.Aplicacao.DAL
{
    internal class UsuarioDAL
    {

        public List<UsuarioSistema> ConsultarUsuario(UsuarioSistema user)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            List<UsuarioSistema> list = new List<UsuarioSistema>();
            UsuarioSistema usu;
            try
            {
                objConexao.AdicionarParametro("P_LOGIN", user.login);
                objConexao.AdicionarParametro("P_NOME", user.nome);
                objConexao.AdicionarParametro("P_DEPARTAMENTO", user.departamento);
                objConexao.AdicionarParametro("P_EMAIL", user.email);

                objConexao.AdicionarParametroCursor("dados");

                System.Data.OracleClient.OracleDataReader leitor = objConexao.ObterLeitor("FUN_PKG_USUARIO.LISTAR_USUARIOS_IMPORTADOS");

                while (leitor.Read())
                {
                    usu = new UsuarioSistema();
                    usu.departamento = leitor["DEPARTAMENTO"].ToString();
                    usu.email = leitor["EMAIL"].ToString();
                    usu.login = leitor["LOGIN"].ToString();
                    usu.nome = leitor["NOME"].ToString();
                    usu.dt_inclusao = DateTime.Parse(leitor["DT_INCLUSAO"].ToString());
                    usu.id_usuario = int.Parse(leitor["ID_USUARIO"].ToString());
                    usu.id_status = int.Parse(leitor["status"].ToString());
                    usu.descricao_status = usu.id_status == 0 ? "INATIVO" : "ATIVO";

                    list.Add(usu);
                }

                leitor.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: \\n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return list;
        }

        public bool ImportaDados(DataTable dt)
        {
            bool ret = false;

            using (OracleBulkCopy bulkCopy = new OracleBulkCopy(ConfigAplication.GetConnectString().Replace(";Unicode=True", "")))
            {
                bulkCopy.DestinationTableName = "OWN_FUNCESP.FUN_TBL_AD_USUARIO";
                bulkCopy.ColumnMappings.Add("Nome", "nome");
                bulkCopy.ColumnMappings.Add("Usuario", "login");
                bulkCopy.ColumnMappings.Add("Email", "email");
                bulkCopy.ColumnMappings.Add("Departamento", "departamento");
                bulkCopy.ColumnMappings.Add("Data", "dt_inclusao");

                try
                {
                    bulkCopy.WriteToServer(dt);
                    ret= true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Problemas contate o administrador do sistema: \\n" + ex.Message);

                }
                finally
                {
                    bulkCopy.Close();

                }
                return ret;
            }
        }

        public bool InsereUsuarioAD()
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
      
                objConexao.AdicionarParametroOut("P_RETORNO");
                objConexao.ExecutarNonQuery("FUN_PKG_USUARIO.IMPORTAR_USUARIO_AD");

                return int.Parse(objConexao.ReturnParemeterOut().Value.ToString()) > 0;

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: \\n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
        }

        public bool InativarUsuario(UsuarioHistorico user)
        {


            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_LOGIN", user.login);
                objConexao.AdicionarParametro("P_STATUS", user.id_status);
                objConexao.AdicionarParametro("P_DS_JUSTIFICATIVA", user.ds_justitificativa);
                objConexao.AdicionarParametro("P_LOGIN_APLICACAO",user.login_aplicacao);

                return objConexao.ExecutarNonQuery("FUN_PKG_USUARIO.INATIVAR_ATIVAR_USUARIO");

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: \\n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }


        }

        public List<UsuarioHistorico> ConsultarMovimentacao(UsuarioSistema user)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            List<UsuarioHistorico> list = new List<UsuarioHistorico>();
            UsuarioHistorico usu;
            try
            {
                objConexao.AdicionarParametro("P_LOGIN", user.login);

                objConexao.AdicionarParametroCursor("dados");

                System.Data.OracleClient.OracleDataReader leitor = objConexao.ObterLeitor("FUN_PKG_USUARIO.LISTAR_MOVIMENTACAO_USUARIO");

                while (leitor.Read())
                {
                    usu = new UsuarioHistorico();   
                    usu.nome = leitor["nome"].ToString();
                    usu.login = leitor["LOGIN"].ToString();
                    usu.dt_inclusao = DateTime.Parse(leitor["DT_INCLUSAO"].ToString());
                    usu.ds_justitificativa = leitor["ds_justificativa"].ToString();
                    usu.id_status = int.Parse(leitor["status"].ToString());
                    usu.descricao_status = usu.id_status == 0 ? "INATIVO" : "ATIVO";

                    list.Add(usu);
                }

                leitor.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: \\n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return list;
        }
    }
}
