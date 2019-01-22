using IntegWeb.Entidades;
using IntegWeb.Previdencia.Aplicacao.BLL.Cadastro;
using IntegWeb.Previdencia.Aplicacao.ENUM;
using System;

namespace IntegWeb.Portal.Web
{
    public partial class AtualizacaoCadastralConfirmacao : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                long cpf = long.Parse(Request.QueryString["cpf"]);
                Guid guid = Guid.Parse(Request.QueryString["cod"]);
                int tipo = int.Parse(Request.QueryString["tipo"]);
                EmpregadoAtualizacaoBLL empregadoAtualizacaoBLL = new EmpregadoAtualizacaoBLL();
                Resultado res = empregadoAtualizacaoBLL.ValidaEmail(cpf, guid);
                
                if (res.Mensagem.Equals("Email não encontrado na atualização cadastral"))
                {
                    throw new Exception(res.Mensagem);
                }

                string nome = empregadoAtualizacaoBLL.GetNome(cpf, (EmpregadoTipo)tipo);
                hNome.InnerText = string.Format("Olá, {0}!", nome);
            }
            catch (ArgumentNullException)
            {
                string msg = "Email não encontrado na atualização cadastral";
                Response.Redirect(string.Format("AtualizacaoCadastralErro.aspx?msg={0}", msg));
            }
            catch (FormatException)
            {
                string msg = "Email não encontrado na atualização cadastral";
                Response.Redirect(string.Format("AtualizacaoCadastralErro.aspx?msg={0}", msg));
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Response.Redirect(string.Format("AtualizacaoCadastralErro.aspx?msg={0}", msg));
            }
        }
    }
}