using IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{
    public partial class CadVerba : BasePage
    {
        #region Atributos
        AcaoJudicialBLL obj = new AcaoJudicialBLL();
        FichaFinanceiraBLL ficha = new FichaFinanceiraBLL();
        string mensagem = "";
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }

        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (!txtCodMatricula.Equals("") && !txtCodEmpresa.Equals(""))
            {

                obj.cod_emprs = int.Parse(txtCodEmpresa.Text);
                obj.matricula = int.Parse(txtCodMatricula.Text);

                DataTable dt = obj.CarregaParticipante();

                lblNome.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    lblNome.Text = "Nome do Participante: " + dt.Rows[0]["nom_emprg"].ToString();
                    hdNumPartif.Value = dt.Rows[0]["NUM_MATR_PARTF"].ToString();
                    tabIni.Visible = true;
                    hdMatricula.Value = txtCodMatricula.Text;
                    HdEmpresa.Value = txtCodEmpresa.Text;
                }
                else
                {
                    tabIni.Visible = false;
                    lblNome.Text = "Nenhum registro encontrado ";
                    hdNumPartif.Value = "0";
                }

            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {

            txtCodMatricula.Text = "";
            txtCodEmpresa.Text = "";

        }

        protected void btnTrocarVerba_Click(object sender, EventArgs e)
        {
            ficha.num_matr_partf = int.Parse(hdNumPartif.Value);
  
            bool ret = ficha.AlteraVerba(out mensagem);


            MostraMensagemTelaUpdatePanel(upVerba, mensagem);

        }


        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            ficha.matricula = int.Parse(hdMatricula.Value);
            ficha.cod_emprs = int.Parse(HdEmpresa.Value);
            DataTable dt = ficha.CarregaVerbasIncorporacao();

            if (dt.Rows.Count > 0)
            {


                Dictionary<string, DataTable> dtRelatorio = new Dictionary<string, DataTable>();
                var nomeArquivo = Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + "_Verbar_de_Incorporacao" + ".xls";
                dtRelatorio.Add(nomeArquivo, dt);
                Session["DtRelatorio"] = dtRelatorio;
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'WebFile.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

            }
            else
                MostraMensagemTelaUpdatePanel(upVerba, "Participante não possuí verbas de Incorpração.");
        }

        #endregion

        #region Métodos

        private void CarregaGrid(string nameView, DataTable dt, GridView grid)
        {

            ViewState[nameView] = dt;
            grid.DataSource = ViewState[nameView];
            grid.DataBind();
        }

        #endregion

 


    }
}