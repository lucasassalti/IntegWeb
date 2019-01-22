using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntegWeb.Framework;
using IntegWeb.Entidades;
using System.Data.OracleClient;
using System.Text.RegularExpressions;
using System.Data;

namespace IntegWeb.Administracao.Aplicacao.DAL
{
    internal class MenuDAL
    {
        private void MapearMenu(OracleDataReader leitor, ref Menu obj)
        {
            obj.Codigo = Convert.ToInt32(leitor["ID_MENU"]);
            obj.Nome = leitor["NM_MENU"].ToString();
            obj.Status = int.Parse(leitor["STATUS"].ToString());
            obj.DescricaoStatus = obj.Status == 0 ? "INATIVO" : "ATIVO";

            obj.Link = leitor["DS_LINK"] != DBNull.Value
                                       ? leitor["DS_LINK"].ToString()
                                       : string.Empty;

            obj.Sistema.Codigo = Convert.ToByte(leitor["ID_SISTEMA"]);
            obj.Sistema.Nome = leitor["NM_SISTEMA"].ToString();

            obj.Nivel = Convert.ToInt16(leitor["CD_NIVEL"]);

            if (leitor["ID_MENU_PAI"] != DBNull.Value)
            {
                obj.MenuPai = new Menu();
                obj.MenuPai.Codigo = Convert.ToInt32(leitor["ID_MENU_PAI"]);
                obj.MenuPai.Nome = leitor["MENU_PAI"].ToString();
       
               
            }

        }

        public DataTable Consultar(Menu objM)
        {
            DataTable dt = new DataTable();
            objM.MenuPai = new Menu();
            //Instanciado a classe padrão de conexão Oracle
            using (ConexaoOracle objConexao = new ConexaoOracle())
            {
      
                //Adicionado todos os parâmetros da package/procedure
                objConexao.AdicionarParametro("P_ID_MENU", objM.Codigo==int.MinValue ? 0 : objM.Codigo);
                objConexao.AdicionarParametro("P_NM_MENU", objM.Nome.Equals("") ? null : objM.Nome);
                objConexao.AdicionarParametro("P_DS_LINK", objM.Link.Equals("") ? null : objM.Link);
                objConexao.AdicionarParametro("P_NM_SISTEMA", objM.Sistema.Nome.Equals("")? null : objM.Sistema.Nome);
                objConexao.AdicionarParametro("P_CD_NIVEL", objM.Nivel== short.MinValue ? 0 : objM.Nivel);
                objConexao.AdicionarParametro("P_MENU_PAI", objM.MenuPai.Nome.Equals("") ? null : objM.MenuPai.Nome);
                objConexao.AdicionarParametro("P_STATUS", objM.Status == int.MinValue ? -1 : objM.Status);
                objConexao.AdicionarParametroCursor("DADOS");

                //Executa a procedure e retorna CURSOR dentro de LEITOR

                using (OracleDataAdapter adp = objConexao.ExecutarAdapter("FUN_PKG_MENU.CONSULTAR"))
                {

                    adp.Fill(dt);

                    
                }
            }

            return dt;
        }

        public List<Menu> Listar()
        {
            List<Menu> lista = new List<Menu>();
            Menu objItem;

            using (ConexaoOracle db = new ConexaoOracle())
            {
                db.AdicionarParametroCursor("DADOS");

                using (OracleDataReader leitor = db.ObterLeitor("FUN_PKG_MENU.LISTAR"))
                {
                    while (leitor.Read())
                    {
                        objItem = new Menu();

                        MapearMenu(leitor, ref objItem);
                        
                        lista.Add(objItem);
                    }
                }
            }
            return lista;
        }



        public List<Menu> ListarPorSistema(byte codigoSistema)
        {
            List<Menu> lista = new List<Menu>();
            Menu objItem;

            using (ConexaoOracle db = new ConexaoOracle())
            {
                db.AdicionarParametro("P_ID_SISTEMA", codigoSistema);
                db.AdicionarParametroCursor("DADOS");

                using (OracleDataReader leitor = db.ObterLeitor("FUN_PKG_MENU.LISTAR_POR_SISTEMA"))
                {
                    while (leitor.Read())
                    {
                        objItem = new Menu();

                        MapearMenu(leitor, ref objItem);

                        lista.Add(objItem);
                    }
                }
            }
            return lista;
        }



        public List<Menu> ListarPorNivel(byte codigoSistema, short nivel, int? codigoMenuPai)
        {
            List<Menu> lista = new List<Menu>();
            Menu objItem;

            using (ConexaoOracle db = new ConexaoOracle())
            {
                db.AdicionarParametro("P_ID_SISTEMA", codigoSistema);
                db.AdicionarParametro("P_CD_NIVEL", nivel);
                db.AdicionarParametro("P_ID_MENU_PAI", codigoMenuPai == null ? DBNull.Value : (object)codigoMenuPai);
                db.AdicionarParametroCursor("DADOS");

                using (OracleDataReader leitor = db.ObterLeitor("FUN_PKG_MENU.LISTAR_POR_NIVEL"))
                {
                    while (leitor.Read())
                    {
                        objItem = new Menu();

                        MapearMenu(leitor, ref objItem);

                        lista.Add(objItem);
                    }
                }
            }
            return lista;
        }



        public List<Menu> ListarPorUsuario(byte codigoSistema, short nivel, int? codigoMenuPai, string login)
        {
            List<Menu> lista = new List<Menu>();
            Menu objItem;

            using (ConexaoOracle db = new ConexaoOracle())
            {
                db.AdicionarParametro("P_ID_SISTEMA", codigoSistema);
                db.AdicionarParametro("P_CD_NIVEL", nivel);
                db.AdicionarParametro("P_ID_MENU_PAI", codigoMenuPai == null ? DBNull.Value : (object)codigoMenuPai);
                db.AdicionarParametro("P_DS_LOGIN", login);
                db.AdicionarParametroCursor("DADOS");

                using (OracleDataReader leitor = db.ObterLeitor("FUN_PKG_MENU.LISTAR_MENU_USUARIO"))
                {
                    while (leitor.Read())
                    {
                        objItem = new Menu();

                        MapearMenu(leitor, ref objItem);

                        lista.Add(objItem);
                    }
                }
            }
            return lista;
        }


        public Resultado AlterarStatus(int codigo, int status)
        {
            Resultado retorno = new Resultado();

            using (ConexaoOracle db = new ConexaoOracle())
            {
                db.AdicionarParametro("P_ID_MENU", codigo);
                db.AdicionarParametro("P_status", status==0?1:0);

                try
                {
                    db.ExecutarDML("FUN_PKG_MENU.ALTERARSTATUS");
                    retorno.Sucesso("status alterado com sucesso!");
                }
                catch (OracleException erroOracle)
                {
                    retorno.Erro("Erro de Banco de Dados: " + erroOracle.Message);
                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao alterar Menu: " + erro.Message);
                }
            }

            return retorno;
        }

        public Resultado Incluir(Menu objMenu)
        {
            Resultado retorno = new Resultado();

            using (ConexaoOracle db = new ConexaoOracle())
            {
                db.AdicionarParametroOut("P_ID_MENU");
                db.AdicionarParametro("P_NM_MENU", objMenu.Nome);
                db.AdicionarParametro("P_DS_LINK", objMenu.Link == string.Empty ? DBNull.Value : (object)objMenu.Link);
                db.AdicionarParametro("P_ID_SISTEMA", objMenu.Sistema.Codigo);
                db.AdicionarParametro("P_CD_NIVEL", objMenu.Nivel);
                db.AdicionarParametro("P_ID_MENU_PAI", objMenu.MenuPai.Codigo == int.MinValue ? DBNull.Value : (object)objMenu.MenuPai.Codigo);

                try
                {
                    //se FOR NECESSÁRIO capturar o código de saída
                    int codigoGerado = db.ExecutarDMLOutput("FUN_PKG_MENU.INSERIR", "P_ID_MENU");

                    retorno.Sucesso("Menu incluido com sucesso!", codigoGerado);
                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao incluir Menu: " + erro.Message);
                }
            }

            return retorno;
        }

        public Resultado Alterar(Menu objMenu)
        {
            Resultado retorno = new Resultado();

            using (ConexaoOracle db = new ConexaoOracle())
            {
                db.AdicionarParametro("P_ID_MENU", objMenu.Codigo);
                db.AdicionarParametro("P_NM_MENU", objMenu.Nome);
                db.AdicionarParametro("P_ID_SISTEMA", objMenu.Sistema.Codigo);
                db.AdicionarParametro("P_CD_NIVEL", objMenu.Nivel);
                db.AdicionarParametro("P_ID_MENU_PAI", objMenu.MenuPai.Codigo == int.MinValue ? DBNull.Value : (object)objMenu.MenuPai.Codigo);
                db.AdicionarParametro("P_DS_LINK", objMenu.Link == string.Empty ? DBNull.Value : (object)objMenu.Link);

                try
                {
                    db.ExecutarDML("FUN_PKG_MENU.ALTERAR");

                    retorno.Sucesso("Menu alterado com sucesso!");
                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao alterar Menu: " + erro.Message);
                }
            }

            return retorno;
        }

        public object login { get; set; }

        public List<string> ListarAcesso(byte codigoSistema, string login)
        {
            List<string> lista = new List<string>();
            Menu objItem;

            using (ConexaoOracle db = new ConexaoOracle())
            {
                db.AdicionarParametro("P_ID_SISTEMA", codigoSistema);
                db.AdicionarParametro("P_DS_LOGIN", login);
                db.AdicionarParametroCursor("DADOS");

                using (OracleDataReader leitor = db.ObterLeitor("FUN_PKG_MENU.LISTAR_MENU_ACESSO"))
                {
                    while (leitor.Read())
                    {
                        objItem = new Menu();

                        lista.Add(leitor["DS_LINK"].ToString());
                    }
                }
            }
            return lista;
        }

        public List<string> ListarAcessoPorGrupo(byte codigoSistema, string grupo)
        {
            List<string> lista = new List<string>();
            Menu objItem;

            using (ConexaoOracle db = new ConexaoOracle())
            {
                db.AdicionarParametro("P_ID_SISTEMA", codigoSistema);
                db.AdicionarParametro("P_GRUPO", grupo);
                db.AdicionarParametroCursor("DADOS");

                using (OracleDataReader leitor = db.ObterLeitor("FUN_PKG_MENU.LISTAR_MENU_GRUPO"))
                {
                    while (leitor.Read())
                    {
                        objItem = new Menu();

                        lista.Add(leitor["DS_LINK"].ToString());
                    }
                }
            }
            return lista;
        }
    }
}
