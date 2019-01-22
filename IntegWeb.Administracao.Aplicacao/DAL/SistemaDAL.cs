using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntegWeb.Framework;
using IntegWeb.Entidades;
using System.Data.OracleClient;

namespace IntegWeb.Administracao.Aplicacao.DAL
{
    internal class SistemaDAL
    {
        public Sistema Consultar(byte codigo)
        {
            Sistema objItem = new Sistema();

            //Instanciado a classe padrão de conexão Oracle
            using (ConexaoOracle objConexao = new ConexaoOracle())
            {
                //Adicionado todos os parâmetros da package/procedure
                objConexao.AdicionarParametro("P_ID_SISTEMA", codigo);
                objConexao.AdicionarParametroCursor("DADOS");

                //Executa a procedure e retorna CURSOR dentro de LEITOR
                using (OracleDataReader leitor = objConexao.ObterLeitor("FUN_PKG_SISTEMA.CONSULTAR"))
                {
                    if (leitor.Read())
                    {
                        //Mapeamento Objeto-Relacional (ORM)
                        objItem.Codigo = Convert.ToByte(leitor["ID_SISTEMA"]);
                        objItem.Nome = leitor["DS_SISTEMA"].ToString();
                        objItem.status = int.Parse(leitor["STATUS"].ToString());
                        objItem.descricao_status = objItem.status == 0 ? "INATIVO" : "ATIVO";
                    }
                }
            }

            return objItem;
        }

        public List<Sistema> Listar()
        {
            List<Sistema> lista = new List<Sistema>();
            Sistema objItem;

            using (ConexaoOracle db = new ConexaoOracle())
            {
                db.AdicionarParametroCursor("DADOS");

                using (OracleDataReader leitor = db.ObterLeitor("FUN_PKG_SISTEMA.LISTAR"))
                {
                    while (leitor.Read())
                    {
                        objItem = new Sistema();

                        objItem.Codigo = Convert.ToByte(leitor["ID_SISTEMA"]);
                        objItem.Nome = leitor["NM_SISTEMA"].ToString();
                        objItem.status = int.Parse(leitor["STATUS"].ToString());
                        objItem.descricao_status = objItem.status == 0 ? "INATIVO" : "ATIVO";

                        lista.Add(objItem);
                    }
                }
            }
            return lista;
        }

        public Resultado AlterarStatus(byte codigo, int status)
        {
            Resultado retorno = new Resultado();

            using (ConexaoOracle db = new ConexaoOracle())
            {
                db.AdicionarParametro("P_ID_SISTEMA", codigo);
                db.AdicionarParametro("P_STATUS", status==0?1:0);

                try
                {
                    db.ExecutarDML("FUN_PKG_SISTEMA.ALTERARSTATUS");

                    retorno.Sucesso("Sistema excluído com sucesso!");
                }
                catch (OracleException erroOracle)
                {
                    retorno.Erro("Erro de Banco de Dados: " + erroOracle.Message);
                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao excluir Sistema: " + erro.Message);
                }
            }

            return retorno;
        }

        public Resultado Incluir(Sistema objSistema)
        {
            Resultado retorno = new Resultado();

            using (ConexaoOracle db = new ConexaoOracle())
            {
                db.AdicionarParametro("P_NM_SISTEMA", objSistema.Nome);

                try
                {

                    db.ExecutarDML("FUN_PKG_SISTEMA.INSERIR");

                    retorno.Sucesso("Sistema incluido com sucesso!");
                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao incluir Sistema: " + erro.Message);
                }
            }

            return retorno;
        }

        public Resultado Alterar(Sistema objSistema)
        {
            Resultado retorno = new Resultado();

            using (ConexaoOracle db = new ConexaoOracle())
            {
                db.AdicionarParametro("P_ID_SISTEMA", objSistema.Codigo);
                db.AdicionarParametro("P_NM_SISTEMA", objSistema.Nome);

                try
                {
                    db.ExecutarDML("FUN_PKG_SISTEMA.ALTERAR");

                    retorno.Sucesso("Sistema alterado com sucesso!");
                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao alterar Sistema: " + erro.Message);
                }
            }

            return retorno;
        }
    }
}
