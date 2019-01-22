using IntegWeb.Saude.Aplicacao.BLL;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IntegWeb.Saude.Web
{
    public partial class CargaUtilizacoes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtVerifica.Visible = false;
            }
        }

        protected void btngerar_Click(object sender, EventArgs e)
        {            
            try
            {
                Obter_dataMax();
            }
            catch (Exception ex)
            {
                txtVerifica.Visible = true;
                txtVerifica.Text = "<div class='n_error'><p>" + ex.Message.Replace("//n","<br>") + "</p></div>";
            }
        }

        public void Obter_dataMax()
        {
            ExtratoUtilizacaoBLL ExtUtilizacao = new ExtratoUtilizacaoBLL();

            DateTime DatMovimento;
            DateTime.TryParse(hiddataini1.Value, out DatMovimento);

            if (DatMovimento.Day != 1)
            {
                txtVerifica.Visible = true;
                txtVerifica.Text = "<div class='n_warning'><p><strong>Data inválida!</strong> <br>Data de movimento deve ser sempre no 1º dia do mês.</p></div> ";
                return;
            }

            int resultado = ExtUtilizacao.ConsultarQtdRegistrosCarga(DatMovimento);

            if (resultado == 0)
            {

                txtVerifica.Visible = true;
                txtVerifica.Text = "<h2>Processando. Aguarde...</h2>";

                ExtUtilizacao.ExecutarCargaExtratoUtilizacao(DatMovimento);

                resultado = ExtUtilizacao.ConsultarQtdRegistrosCarga(DatMovimento);

                if (resultado == 0)
                {

                    txtVerifica.Visible = true;
                    txtVerifica.Text = "<div class='n_error'><p>A carga não está disponível na data de movineto informada!</p></div>";
                
                }
                else
                {
                    txtVerifica.Visible = true;
                    txtVerifica.Text = "<div class='n_ok'><p><strong>Carga efetuada com sucesso!</strong> <br> Foram inseridos: " + resultado + " registros na data informada!</p></div> ";
                }

            }
            else
            {
                txtVerifica.Visible = true;
                txtVerifica.Text = "<div class='n_warning'><p><strong>A data " + hiddataini1.Value + " já foi processada anteriormente! Inserir uma data que ainda não tenha sido processada.</strong> <br> Foram encontrados " + resultado + " registros.</p></div> ";
            }


        }

    }
}