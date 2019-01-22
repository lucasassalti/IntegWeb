using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.BLL.Cadastro;
using IntegWeb.Previdencia.Aplicacao.ENUM;
using System;

namespace IntegWeb.Portal.Web
{
    public partial class AtualizacaoCadastral : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                long cpf = long.Parse(Request.QueryString["cpf"]);
                Guid guid = Guid.Parse(Request.QueryString["cod"]);
                int tipo = int.Parse(Request.QueryString["tipo"]);
                string linkConfirmacao = string.Format("http://{0}/AtualizacaoCadastralConfirmacao.aspx?cpf={1}&cod={2}&tipo={3}", Util.GetUrlPortal(), cpf, guid, tipo);
                string nome = new EmpregadoAtualizacaoBLL().GetNome(cpf, (EmpregadoTipo)tipo);

                if (string.IsNullOrEmpty(nome))
                {
                    throw new Exception("Email não encontrado na atualização cadastral");
                }

                hNome.InnerText = string.Format("Olá, {0}!", nome);
                hyperlinkConfirmacao.NavigateUrl = linkConfirmacao;
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