using IntegWeb.Entidades.Saude.Financeiro;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.BLL.Financeiro;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Saude.Web
{
    public partial class ProcessaBoleto : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            dtProc.CssClass = "date";
            dtProc.Attributes.Add("onkeypress", "javascript:return mascara(this, data);");
            dtProc.Attributes.Add("MaxLength", "10");
            
        }

        protected void btnEnvia_Click(object sender, EventArgs e)
        {

                Boleto dt = new Boleto();
                dt.DataVencimento = DateTime.Parse(dtProc.Text);
                //bool prc = new BoletoBLL().ProcessaBoleto(dt);
                //dtProc.Text = "";
                BoletoBLL objBLL = new BoletoBLL();

            if (Session["objUser"] != null)
                {

                    ConectaAD objad = (ConectaAD)Session["objUser"];


                    string valida = ValidaDate(dtProc.Text);
                    int ret = 0;
                    //Se tem Relatório sendo gerado
                    if (string.IsNullOrEmpty(valida))
                    {
                        ret = objBLL.ProcessaBoleto(dt);
                            
                           // (Convert.ToDateTime(dtProc.Text).ToString("dd/MM/yyyy"));                            
                    }
                    else
                        MostraMensagemTelaUpdatePanel(upHistBoleto, valida);
                    
                    if (ret < 1)
                            {
                                MostraMensagemTelaUpdatePanel(upHistBoleto, "Já existe uma execução em andamento. Por favor, aguarde.");
                            }
                            
                }

        }

 

        private string ValidaDate(string dt)
        {

            if (!string.IsNullOrEmpty(dt))

                return "";
            else
                return "Digite a data de movimentação";

        }

     
    }
}