
using  IntegWeb.Entidades;
using  IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Periodico.Aplicacao.DAL
{
    internal class EditoraDAL :Editora
    {

        public bool Insert(out string mensagem, Editora objM, out int id)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_NOME", objM.nome_editora);
                objConexao.AdicionarParametro("P_CGC_CPF", objM.cgc_cpf_editora);
                objConexao.AdicionarParametro("P_RUA", objM.endereco_editora);
                objConexao.AdicionarParametro("P_CIDADE", objM.cidade_editora);
                objConexao.AdicionarParametro("P_BAIRRO", objM.bairro_editora);
                objConexao.AdicionarParametro("P_UF", objM.uf_editora);
                objConexao.AdicionarParametro("P_CEP", objM.cep_editora);
                objConexao.AdicionarParametro("P_COMPLEMENTO", objM.complemento_editora);
                objConexao.AdicionarParametro("P_NUMERO", objM.numero_editora);

                objConexao.AdicionarParametro("P_FAX", objM.fax_editora);
                objConexao.AdicionarParametro("P_FONE", objM.fone_editora);
                objConexao.AdicionarParametro("P_EMAIL", objM.email_editora);
                objConexao.AdicionarParametro("P_CONTATO", objM.contato);
                objConexao.AdicionarParametro("P_SITE", objM.site_editora);
                objConexao.AdicionarParametroOut("P_RETORNO");
                mensagem = "Registro inserido com sucesso";
                objConexao.ExecutarNonQuery("SAU_PKG_EDITORA.INSERIR");
                id=int.Parse(objConexao.ReturnParemeterOut().Value.ToString());
               return id>0;

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }



        }

        public DataTable SelectAll(Editora objM)
        {


            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_NOME", objM.nome_editora);
                objConexao.AdicionarParametro("P_CGC_CPF", objM.cgc_cpf_editora);
                objConexao.AdicionarParametro("P_ID_EDITORA", objM.id_editora);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_EDITORA.LISTAR");

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

        public bool Update(out string mensagem, Editora objM)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_EDITORA", objM.id_editora);
                objConexao.AdicionarParametro("P_NOME", objM.nome_editora);
                objConexao.AdicionarParametro("P_CGC_CPF", objM.cgc_cpf_editora);
                objConexao.AdicionarParametro("P_RUA", objM.endereco_editora);
                objConexao.AdicionarParametro("P_CIDADE", objM.cidade_editora);
                objConexao.AdicionarParametro("P_BAIRRO", objM.bairro_editora);
                objConexao.AdicionarParametro("P_UF", objM.uf_editora);
                objConexao.AdicionarParametro("P_CEP", objM.cep_editora);
                objConexao.AdicionarParametro("P_COMPLEMENTO", objM.complemento_editora);
                objConexao.AdicionarParametro("P_NUMERO", objM.numero_editora);

                objConexao.AdicionarParametro("P_FAX", objM.fax_editora);
                objConexao.AdicionarParametro("P_FONE", objM.fone_editora);
                objConexao.AdicionarParametro("P_EMAIL", objM.email_editora);
                objConexao.AdicionarParametro("P_CONTATO", objM.contato);
                objConexao.AdicionarParametro("P_SITE", objM.site_editora);

                mensagem = "Registro Atualizado com Sucesso";
                return objConexao.ExecutarNonQuery("SAU_PKG_EDITORA.ALTERAR");

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
           

        }

        public bool Delete(out string mensagem, Editora obj)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_EDITORA", obj.id_editora);
                objConexao.AdicionarParametroOut("P_RETURN");

                objConexao.ExecutarNonQuery("SAU_PKG_EDITORA.DELETAR");
                bool ret = int.Parse(objConexao.ReturnParemeterOut().Value.ToString()) > 0;
                if (ret)
                    mensagem = "Registro deletado com sucesso";
                else
                    mensagem = "Não é possível deletar um registro que possui vínculo.";
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            

        }
    }
}
