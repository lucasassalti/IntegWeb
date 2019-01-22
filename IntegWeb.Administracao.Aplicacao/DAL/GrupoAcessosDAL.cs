using IntegWeb.Entidades;
using IntegWeb.Entidades.Administracao;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Administracao.Aplicacao
{
    internal class GrupoAcessosDAL
    {

        public List<GrupoAcessos> ConsultarGrupo(GrupoAcessos grupo)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            List<GrupoAcessos> list = new List<GrupoAcessos>();
            try
            {
                objConexao.AdicionarParametro("p_id_grupo_acessos", grupo.id_grupo_acessos);
                objConexao.AdicionarParametro("p_area", grupo.area == null ? grupo.area : grupo.area.ToUpper());
                objConexao.AdicionarParametro("p_nome", grupo.nome == null ? grupo.nome : grupo.nome.ToUpper());
                objConexao.AdicionarParametro("p_status", grupo.id_status == null ? -1 : grupo.id_status);
                objConexao.AdicionarParametro("P_ID_USUARIO", grupo.usuarios.Count > 0 ? grupo.usuarios[0].matricula : null);
                objConexao.AdicionarParametro("P_ID_MENU", grupo.menus.Count > 0 ? grupo.menus[0].id_menu : null);
                objConexao.AdicionarParametroCursor("dados");

                OracleDataReader leitor = objConexao.ObterLeitor("FUN_PKG_GRUPO.consultar_grupos");

                while (leitor.Read())
                {
                    grupo = new GrupoAcessos();
                    grupo.id_grupo_acessos = int.Parse(leitor["ID_GRUPO_ACESSOS"].ToString());
                    grupo.area = leitor["AREA"].ToString();
                    grupo.nome = leitor["NOME"].ToString();
                    grupo.descricao = leitor["DESCRICAO"].ToString();
                    grupo.descricao_status = int.Parse(leitor["STATUS"].ToString())==0?"INATIVO":"ATIVO";
                    grupo.id_status = int.Parse(leitor["STATUS"].ToString());
                    list.Add(grupo);
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

        public List<GrupoUsuario> ConsultarUsuario(GrupoAcessos grupo)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            List<GrupoUsuario> list = new List<GrupoUsuario>();
            GrupoUsuario objU;
            try
            {
                objConexao.AdicionarParametro("P_ID_USUARIO", grupo.usuarios[0].matricula);
                objConexao.AdicionarParametro("P_NOME", grupo.usuarios[0].nome == null ? grupo.usuarios[0].nome : grupo.usuarios[0].nome.ToUpper());
                objConexao.AdicionarParametroCursor("dados");

                OracleDataReader leitor = objConexao.ObterLeitor("FUN_PKG_GRUPO.CONSULTAR_USUARIO");

                while (leitor.Read())
                {
                    objU = new GrupoUsuario();
                    objU.matricula = int.Parse(leitor["MATRICULA"].ToString());
                    objU.nome = leitor["IDENTIFICACAO"].ToString();
                    list.Add(objU);
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

        public List<GrupoMenu> ConsultarMenu(GrupoAcessos grupo)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            List<GrupoMenu> list = new List<GrupoMenu>();
            GrupoMenu objM;
            try
            {
                objConexao.AdicionarParametro("P_MENU_PAI", grupo.menus[0].menu_pai);
                objConexao.AdicionarParametro("P_MENU", grupo.menus[0].menu);
                objConexao.AdicionarParametro("P_AREA", grupo.menus[0].area);
                objConexao.AdicionarParametro("P_SISTEMA", grupo.menus[0].sistema);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataReader leitor = objConexao.ObterLeitor("FUN_PKG_GRUPO.CONSUTAR_MENU");

                while (leitor.Read())
                {
                    objM = new GrupoMenu();
                    objM.menu_pai = leitor["MENU_PAI"].ToString();
                    objM.menu = leitor["MENU"].ToString();
                    objM.area = leitor["AREA"].ToString();
                    objM.sistema = leitor["SISTEMA"].ToString();
                    objM.id_menu = int.Parse(leitor["ID_MENU"].ToString());
                    list.Add(objM);
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

        public DataTable ConsultarMenuSistema(int id_sistema)
        {

            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {


                objConexao.AdicionarParametro("P_ID_SISTEMA", id_sistema);
                objConexao.AdicionarParametroCursor("DADOS");


                OracleDataAdapter adpt = objConexao.ExecutarAdapter("FUN_PKG_GRUPO.CONSULTA_PAGINA_SISTEMA");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: \\n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }

        public DataTable ConsultarPagina(int id_grupo)
        {

            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {


                objConexao.AdicionarParametro("P_ID_GRUPOS_ACESSOS", id_grupo);
                objConexao.AdicionarParametroCursor("DADOS");


                OracleDataAdapter adpt = objConexao.ExecutarAdapter("FUN_PKG_GRUPO.CONSUTAR_PAGINA");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: \\n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }

        public DataTable ConsultarUsuario(int id_grupo)
        {

            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {


                objConexao.AdicionarParametro("P_ID_GRUPOS_ACESSOS", id_grupo);
                objConexao.AdicionarParametroCursor("DADOS");


                OracleDataAdapter adpt = objConexao.ExecutarAdapter("FUN_PKG_GRUPO.CONSUTAR_USARIO");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: \\n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }

        public GrupoAcessos ConsultarUsuarioMenu(GrupoAcessos grupo)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            GrupoAcessos grupos = new GrupoAcessos();
            GrupoMenu objM;
            GrupoUsuario objU;
            try
            {
                objConexao.AdicionarParametro("P_ID_GRUPOS_ACESSOS", grupo.id_grupo_acessos);
                objConexao.AdicionarParametroCursor("dados");
                objConexao.AdicionarParametroCursor("dados1");

                OracleDataReader leitor = objConexao.ObterLeitor("FUN_PKG_GRUPO.CONSUTAR_MENU_GRUPO_USUARIO");

                while (leitor.Read())
                {
                    objM = new GrupoMenu();
                    objM.menu_pai = leitor["MENU_PAI"].ToString();
                    objM.menu = leitor["MENU"].ToString();
                    objM.area = leitor["AREA"].ToString();
                    objM.sistema = leitor["SISTEMA"].ToString();
                    objM.id_menu = int.Parse(leitor["ID_MENU"].ToString());
                    objM.id_status = int.Parse(leitor["STATUS"].ToString());
                    objM.descricao_status = int.Parse(leitor["STATUS"].ToString()) > 0 ? "ATIVO" : "INATIVO";
                    grupos.menus.Add(objM);
                }

                leitor.NextResult();

                while (leitor.Read())
                {
                    objU = new GrupoUsuario();
                    objU.matricula = int.Parse(leitor["MATRICULA"].ToString());
                    objU.nome = leitor["IDENTIFICACAO"].ToString();
                    objU.id_status = int.Parse(leitor["STATUS"].ToString());
                    objU.descricao_status = int.Parse(leitor["STATUS"].ToString())>0?"ATIVO":"INATIVO";
                    grupos.usuarios.Add(objU);
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
            return grupos;
        }

        public DataSet PopularDropDow()
        {

            DataSet dt = new DataSet();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");
                objConexao.AdicionarParametroCursor("DADOS1");
                objConexao.AdicionarParametroCursor("DADOS2");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("FUN_PKG_GRUPO.CONSUTAR_DROP");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: \\n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;


        }

        public bool InsereGrupo(GrupoAcessos grupo)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_AREA", grupo.area.ToUpper());
                objConexao.AdicionarParametro("P_NOME", grupo.nome.ToUpper());
                objConexao.AdicionarParametro("P_DESCRICAO", grupo.descricao.ToUpper());

                return objConexao.ExecutarNonQuery("FUN_PKG_GRUPO.INSERIR_GRUPO");

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

        public bool AtualizaGrupo(GrupoAcessos grupo)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_GRUPO_ACESSOS", grupo.id_grupo_acessos);
                objConexao.AdicionarParametro("P_AREA", grupo.area.ToUpper());
                objConexao.AdicionarParametro("P_NOME", grupo.nome.ToUpper());
                objConexao.AdicionarParametro("P_DESCRICAO", grupo.descricao.ToUpper());

                return objConexao.ExecutarNonQuery("FUN_PKG_GRUPO.ATUALIZAR_GRUPO");

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

        public bool AlterarStatusGrupo(GrupoAcessos grupo) {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_GRUPO_ACESSOS", grupo.id_grupo_acessos);
                objConexao.AdicionarParametro("P_STATUS", grupo.id_status);

                return objConexao.ExecutarNonQuery("FUN_PKG_GRUPO.INATIVAR_ATIVAR_GRUPO");

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

        public bool InsereUsuarioGrupo(GrupoAcessos grupo, UsuarioSistema user)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                Util.Log("CALL PROC: FUN_PKG_GRUPO.INSERIR_GRUPO_USUARIO",
                    "P_ID_USUARIO: " + ((grupo.usuarios.Count > 0) ? grupo.usuarios[0].listid : "") +
                    " P_ID_GRUPO_ACESSOS: " + grupo.id_grupo_acessos +
                    " P_ID_USUARIO_APLICACAO: " + user.login);

                objConexao.AdicionarParametro("P_ID_USUARIO",grupo.usuarios.Count>0?grupo.usuarios[0].listid:null);
                objConexao.AdicionarParametro("P_ID_GRUPO_ACESSOS", grupo.id_grupo_acessos);
                objConexao.AdicionarParametro("P_ID_USUARIO_APLICACAO", user.login);
                objConexao.AdicionarParametroOut("P_RETORNO");
                objConexao.ExecutarNonQuery("FUN_PKG_GRUPO.INSERIR_GRUPO_USUARIO");

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

        public bool InativarUsuarioGrupo(GrupoAcessos grupo, UsuarioSistema user) {


            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_USUARIO", grupo.usuarios.Count>0? grupo.usuarios[0].matricula:null);
                objConexao.AdicionarParametro("P_ID_GRUPO_ACESSOS", grupo.id_grupo_acessos);
                objConexao.AdicionarParametro("P_STATUS", grupo.usuarios.Count > 0 ? grupo.usuarios[0].id_status : null);
                objConexao.AdicionarParametro("P_ID_USUARIO_APLICACAO",user.login);

                return objConexao.ExecutarNonQuery("FUN_PKG_GRUPO.INATIVAR_ATIVAR_USUARIO");

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: \\n" + ex.Message + " " + grupo.usuarios[0].matricula + "  " + grupo.id_grupo_acessos + "  " + grupo.usuarios[0].id_status+"  "+user.login);
            }
            finally
            {
                objConexao.Dispose();
            }
        
        
        }

        public bool InserePaginaGrupo(GrupoAcessos grupo)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_MENU",grupo.menus.Count>0? grupo.menus[0].listids:null);
                objConexao.AdicionarParametro("P_ID_GRUPO_ACESSOS", grupo.id_grupo_acessos);
                objConexao.AdicionarParametroOut("P_RETORNO");
                objConexao.ExecutarNonQuery("FUN_PKG_GRUPO.INSERIR_GRUPO_MENU");

                return int.Parse(objConexao.ReturnParemeterOut().Value.ToString())>0;

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

        public bool InativarPaginaGrupo(GrupoAcessos grupo)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_GRUPOS_ACESSOS", grupo.id_grupo_acessos);
                objConexao.AdicionarParametro("P_ID_MENU", grupo.menus.Count>0?grupo.menus[0].id_menu:null);
                objConexao.AdicionarParametro("P_STATUS", grupo.menus.Count > 0 ? grupo.menus[0].id_status : null);

                return objConexao.ExecutarNonQuery("FUN_PKG_GRUPO.INATIVAR_ATIVAR_MENU");

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

        public List<GrupoAcessos> ConsultarUsuarioGrupo(GrupoAcessos grupo)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            List<GrupoAcessos> list = new List<GrupoAcessos>();
            try
            {

                objConexao.AdicionarParametro("P_ID_USUARIO", grupo.usuarios.Count > 0 ? grupo.usuarios[0].matricula : null);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataReader leitor = objConexao.ObterLeitor("FUN_PKG_GRUPO.CONSULTA_USUARIO_GRUPO");

                while (leitor.Read())
                {
                    grupo = new GrupoAcessos();
                    grupo.id_grupo_acessos = int.Parse(leitor["ID_GRUPO_ACESSOS"].ToString());
                    grupo.area = leitor["AREA"].ToString();
                    grupo.nome = leitor["NOME"].ToString();
                    grupo.descricao = leitor["DESCRICAO"].ToString();
                    grupo.descricao_status = int.Parse(leitor["STATUS"].ToString()) == 0 ? "INATIVO" : "ATIVO";
                    grupo.id_status = int.Parse(leitor["STATUS"].ToString());
                    list.Add(grupo);
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

        public List<GrupoAcessos> ConsultarPaginaGrupo(GrupoAcessos grupo)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            List<GrupoAcessos> list = new List<GrupoAcessos>();
            try
            {

                objConexao.AdicionarParametro("P_ID_MENU", grupo.menus.Count > 0 ? grupo.menus[0].id_menu : null);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataReader leitor = objConexao.ObterLeitor("FUN_PKG_GRUPO.CONSULTA_PAGINA_GRUPO");

                while (leitor.Read())
                {
                    grupo = new GrupoAcessos();
                    grupo.id_grupo_acessos = int.Parse(leitor["ID_GRUPO_ACESSOS"].ToString());
                    grupo.area = leitor["AREA"].ToString();
                    grupo.nome = leitor["NOME"].ToString();
                    grupo.descricao = leitor["DESCRICAO"].ToString();
                    grupo.descricao_status = int.Parse(leitor["STATUS"].ToString()) == 0 ? "INATIVO" : "ATIVO";
                    grupo.id_status = int.Parse(leitor["STATUS"].ToString());
                    list.Add(grupo);
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
