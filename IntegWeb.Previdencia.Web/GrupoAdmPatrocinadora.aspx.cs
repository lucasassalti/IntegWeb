using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{
    public partial class GrupoAdmPatrocinadora : BasePage
    {
        #region .: Eventos Página Principal :.

        protected void Page_Load(object sender, EventArgs e)
        {
            GrupoAdmPatrocinadoraBLL bll = new GrupoAdmPatrocinadoraBLL();

            if (!IsPostBack)
            {
                CarregarTelaPrincipal();
            }

        }

        protected void btnInserirGrupo_Click(object sender, EventArgs e)
        {
            divLista.Visible = false;
            divInsere.Visible = true;
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            GrupoAdmPatrocinadoraBLL bll = new GrupoAdmPatrocinadoraBLL();
            lstGrupo.DataSource = bll.Search(txtNomGrupo.Text, Util.String2Short(txtEmpresa.Text));
            lstGrupo.DataTextField = "GRUPO";
            lstGrupo.DataValueField = "GRUPO";
            lstGrupo.DataBind();
            lstGrupo_SelectedIndexChanged(null, null);
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {

            CarregarTelaPrincipal();
            lstGrupo_SelectedIndexChanged(null, null);
        }

        protected void lstGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            GrupoAdmPatrocinadoraBLL bll = new GrupoAdmPatrocinadoraBLL();
            string emp = lstGrupo.SelectedValue.ToString();

            lstEmpresa.DataSource = bll.GetEmpresa(emp);
            lstEmpresa.DataTextField = "EMPRESA";
            lstEmpresa.DataValueField = "EMPRESA";
            lstEmpresa.DataBind();
            btnAdd.Visible = true;
            btnDel.Visible = true;
            ddlEmpresas.Visible = true;
            lblAddEmpresa.Visible = true;

        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            GrupoAdmPatrocinadoraBLL bll = new GrupoAdmPatrocinadoraBLL();
            Resultado res = new Resultado();
            string grupo = lstGrupo.SelectedValue.ToString();
            string emp = lstEmpresa.SelectedValue.ToString();

            if (bll.ValidaEmpresa(Convert.ToInt16(emp == "" ? 0 : Convert.ToInt16(emp))) == false)
            {
                MostraMensagemTelaUpdatePanel(upGrupoAdm, "Favor Escolher uma Empresa ! ");
                return;
            }

            res = bll.Delete(grupo, Convert.ToInt16(emp == "" ? 0 : Convert.ToInt16(emp)));

            if (res.Ok)
            {
                MostraMensagemTelaUpdatePanel(upGrupoAdm, "Empresa Excluída com Sucesso !");
                CarregarTelaPrincipal();
                lstGrupo_SelectedIndexChanged(null, null);
            }
            
            else
            {
                MostraMensagemTelaUpdatePanel(upGrupoAdm, "Problemas ao Excluir: " + res.Mensagem);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            GrupoAdmPatrocinadoraBLL bll = new GrupoAdmPatrocinadoraBLL();
            Resultado res = new Resultado();
            string grupo = lstGrupo.SelectedValue.ToString();
            short emp = Convert.ToInt16(ddlEmpresas.SelectedValue.ToString());

            // Validação
            if (bll.ValidaEmpresa(emp) == false)
            {
                MostraMensagemTelaUpdatePanel(upGrupoAdm, "Favor Escolher uma Empresa ! ");
                return;
            }
            if (bll.ValidaGrupoEmp(grupo, emp) == false)
            {
                MostraMensagemTelaUpdatePanel(upGrupoAdm, "Essa Empresa já está Cadastrada nesse Grupo ! ");
                return;
            }

            res = bll.Insert(grupo, emp);

            if (res.Ok)
            {
                MostraMensagemTelaUpdatePanel(upGrupoAdm, "Empresa adicionado com sucesso !");
                CarregarTelaPrincipal();
                lstGrupo_SelectedIndexChanged(null, null);
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upGrupoAdm, "Problemas ao Inserir a Empresa: " + res.Mensagem);
            }

        }

        #endregion

        #region .: Eventos Página de Inserção de Grupo :.

        protected void btnAddGrupo_Click(object sender, EventArgs e)
        {
            GrupoAdmPatrocinadoraBLL bll = new GrupoAdmPatrocinadoraBLL();
            Resultado res = new Resultado();

            if (bll.ValidaEmpresa(Convert.ToInt16(txtCodEmpresa.Text)) == false)
            {
                MostraMensagemTelaUpdatePanel(upGrupoAdm, "Favor Escolher uma Empresa ! ");
                return;
            }

            if (bll.ValidaGrupoEmp(txtNomeGrupo.Text, Convert.ToInt16(txtCodEmpresa.Text)) == false)
            {
                MostraMensagemTelaUpdatePanel(upGrupoAdm, "Esse Grupo já está Cadastrado ! ");
                return;
            }
            else
            {
                res = bll.Insert(txtNomeGrupo.Text, Convert.ToInt16(txtCodEmpresa.Text));

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upGrupoAdm, "Grupo adicionado com Sucesso ! ");
                    CarregarTelaPrincipal();

                }
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            divLista.Visible = true;
            divInsere.Visible = false;
            CarregarTelaPrincipal();
        }

        #endregion

        #region .: Métodos :.

        public void CarregarTelaPrincipal()
        {
            GrupoAdmPatrocinadoraBLL bll = new GrupoAdmPatrocinadoraBLL();

            lstGrupo.DataSource = bll.GetGrupo();
            lstGrupo.DataTextField = "GRUPO";
            lstGrupo.DataValueField = "GRUPO";
            lstGrupo.DataBind();
            ddlEmpresas.DataSource = bll.GetEmpresaGeral();
            ddlEmpresas.DataValueField = "COD_EMPRS";
            ddlEmpresas.DataTextField = "COD_EMPRS";
            ddlEmpresas.DataBind();
            ddlEmpresas.Items.Insert(0, new ListItem("---Selecione---", "0"));
            btnAdd.Visible = false;
            btnDel.Visible = false;
            ddlEmpresas.Visible = false;
            lblAddEmpresa.Visible = false;
            divInsere.Visible = false;
            divLista.Visible = true;
            txtCodEmpresa.Text = "";
            txtNomeGrupo.Text = "";
            txtNomGrupo.Text = "";
            txtEmpresa.Text = "";
        }

        #endregion



    }
}