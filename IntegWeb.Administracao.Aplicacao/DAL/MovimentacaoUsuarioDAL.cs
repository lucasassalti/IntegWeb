using IntegWeb.Entidades.Administracao;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Administracao.Aplicacao.DAL
{
    internal class MovimentacaoUsuarioDAL
    {

        public List<MovimentacaoUsuario> ConsultarMovimentacao(MovimentacaoUsuario mov)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            List<MovimentacaoUsuario> list = new List<MovimentacaoUsuario>();
            try
            {
                objConexao.AdicionarParametro("P_ID_USUARIO", mov.id_usuario);
                objConexao.AdicionarParametroCursor("dados");

                OracleDataReader leitor = objConexao.ObterLeitor("FUN_PKG_GRUPO.CONSULTA_MOVIMENTACAO_USUARIO");

                while (leitor.Read())
                {
                    mov = new MovimentacaoUsuario();
                    mov.id_grupos_acesso = int.Parse(leitor["ID_GRUPOS_ACESSOS"].ToString());
                    mov.id_movimentacao_usuario = int.Parse(leitor["ID_MOVIMENTACAO_USUARIO"].ToString());
                    mov.id_usuario_aplicacao = int.Parse(leitor["ID_USUARIO_APLICACAO"].ToString());
                    mov.status = int.Parse(leitor["STATUS"].ToString());
                    mov.dt_movimentacao = DateTime.Parse(leitor["DT_MOVIMENTACAO"].ToString());
                    mov.descricao_movimentacao=mov.status==0?"Saída":"Entrada";
                    mov.descricao_usuario = leitor["USARIO"].ToString();
                    mov.descricao_grupo = leitor["GRUPO"].ToString();
                    mov.area = leitor["AREA"].ToString();
              
                    list.Add(mov);
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
