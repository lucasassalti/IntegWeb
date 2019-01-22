using IntegWeb.Administracao.Aplicacao;
using IntegWeb.Administracao.Aplicacao.BLL;
using IntegWeb.Entidades;
using IntegWeb.Entidades.Administracao;
using IntegWeb.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IntegWeb.Administracao.Web
{
    public partial class GrupoAdm : System.Web.UI.Page
    {
        #region Atributos
        GrupoAcessosBLL objBll = new GrupoAcessosBLL();
        GrupoAcessos objM = new GrupoAcessos();
        GrupoUsuario objU = new GrupoUsuario();
        BasePage objB = new BasePage();
        GrupoMenu objMn = new GrupoMenu();
        ArrayList arrayUsuAcessos = new ArrayList();
        ArrayList arrayUsuGrupos = new ArrayList();
        ArrayList arrayPagAcessos = new ArrayList();
        ArrayList arrayPagGrupos = new ArrayList();
        MovimentacaoUsuario ObjMov = new MovimentacaoUsuario();
        MovimentacaoUsuarioBLL ObjBllMov = new MovimentacaoUsuarioBLL();

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaCamposTela();
            }

        }

        #region ABA Grupo
        protected void grdGrupo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdGrupo"] != null)
            {
                grdGrupo.PageIndex = e.NewPageIndex;
                grdGrupo.DataSource = ViewState["grdGrupo"];
                grdGrupo.DataBind();
            }
        }

        protected void btnGrupo_Click(object sender, EventArgs e)
        {
            string msg = "";
            int number = 0;
            bool isErro = ValidaCamposTela(txtGrupo, drpGrupo, out msg);

            if (drpGrupo.SelectedValue == "3" && !int.TryParse(txtGrupo.Text, out number))
            {
                isErro = true;
                msg += "\\nDigite apenas Números!\\n0=INATIVO\\n1=ATIVO";
            }

            if (isErro)
            {
                objB.MostraMensagemTelaUpdatePanel(upGrupos, msg.ToString());
            }
            else
            {
                if (drpGrupo.SelectedValue == "1")
                    objM.nome = txtGrupo.Text;
                else if (drpGrupo.SelectedValue == "2")
                    objM.area = txtGrupo.Text;
                else
                    objM.id_status = int.Parse(txtGrupo.Text);

                CarregaGrid("grdUsuario", objBll.ListarGrupo(objM).ToList<Object>(), grdGrupo);
                AlteraLabelGrupo("");
            }
            DivAcao(DivSelectGrupo, DivActionGrupo);
        }

        protected void grdGrupo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] commandArgs;
            string id = "";
            string nome = "";

            switch (e.CommandName)
            {
                case "Selecionar":

                    commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    id = commandArgs[0];
                    nome = commandArgs[1];

                    AlteraLabelGrupo(nome);
                    hdfGrupo.Value = id;
                    objM.id_grupo_acessos = int.Parse(id);
                    objM = objBll.ListarMenuUsuario(objM);

                    CarregaGrid("grdUsuario", objM.usuarios.ToList<Object>(), grdUsuario);
                    CarregaGrid("grdPagina", objM.menus.ToList<Object>(), grdPagina);
                    DivAcao(DivSelectGrupo, DivActionGrupo);
                    DivAcao(DivSelectUsuario, DivActionUsuario);
                    DivAcao(DivSelectPagina, DivActionPagina);

                    break;

                case "Editar":
                    commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    id = commandArgs[0];

                    objM.id_grupo_acessos = int.Parse(id);
                    List<GrupoAcessos> list = objBll.ListarGrupo(objM);

                    if (list.Count > 0)
                    {
                        txtArea.Text = list[0].area;
                        txtDescricao.Text = list[0].descricao;
                        txtNome.Text = list[0].nome;
                        Session["Editar"] = id;
                    }
                    DivAcao(DivActionGrupo, DivSelectGrupo);

                    break;
                case "Status":
                    commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    id = commandArgs[0];

                    objM.id_grupo_acessos = int.Parse(id);
                    objM.id_status = int.Parse(commandArgs[2]) == 0 ? 1 : 0;
                    string msg = "";
                    objBll.AlterarStatus(objM, out msg);
                    CarregaGrid("grdGrupo", objBll.ListarGrupo(new GrupoAcessos()).ToList<Object>(), grdGrupo);
                    break;
                default:
                    break;
            }


        }

        protected void btnInserirGrupo_Click(object sender, EventArgs e)
        {
            string msg;
            bool isValid;
            objM.descricao = txtDescricao.Text;
            objM.area = txtArea.Text;
            objM.nome = txtNome.Text;

            if (Session["Editar"] == null)
            {

                isValid = objBll.InserirGrupo(objM, out msg);
                objB.MostraMensagemTelaUpdatePanel(upGrupos, msg);
            }
            else
            {
                objM.id_grupo_acessos = int.Parse(Session["Editar"].ToString());
                isValid = objBll.AtualizarGrupo(objM, out msg);
                objB.MostraMensagemTelaUpdatePanel(upGrupos, msg);
                Session.Remove("Editar");

            }

            if (isValid)
            {
                CarregaGrid("grdGrupo", objBll.ListarGrupo(new GrupoAcessos()).ToList<Object>(), grdGrupo);
                DivAcao(DivSelectGrupo, DivActionGrupo);
                LimpCamposTela();
            }

        }

        protected void btnVoltarGrupo_Click(object sender, EventArgs e)
        {
            if (Session["Editar"] != null)
                Session.Remove("Editar");
            DivAcao(DivSelectGrupo, DivActionGrupo);
        }

        protected void lnkInserirGrupo_Click(object sender, EventArgs e)
        {
            LimpCamposTela();
            DivAcao(DivActionGrupo, DivSelectGrupo);
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            drpGrupo.SelectedValue = "0";
            txtGrupo.Text = "";
            CarregaGrid("grdGrupo", objBll.ListarGrupo(objM).ToList<Object>(), grdGrupo);
        }
        #endregion

        #region ABA Usuario

        protected void grdUsuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdUsuario"] != null)
            {
                grdUsuario.PageIndex = e.NewPageIndex;
                grdUsuario.DataSource = ViewState["grdUsuario"];
                grdUsuario.DataBind();
            }
        }

        protected void lnkInserirUsuario_Click(object sender, EventArgs e)
        {
            LimpCamposTela();
            DivAcao(DivActionUsuario, DivSelectUsuario);
        }

        protected void btnVoltarUsuario_Click(object sender, EventArgs e)
        {
            DivAcao(DivSelectUsuario, DivActionUsuario);
        }

        protected void BtnSalvarUsuario_Click(object sender, EventArgs e)
        {
            string msg;
            bool isValid;

            objM.id_grupo_acessos = int.Parse(drpGrupoUsuario.SelectedValue);

            if (lstUsuarioAcessos.Items.Count == 0)
                objU.listid = "0";
            else if (lstUsuarioAcessos.Items.Count == 1)
                objU.listid = lstUsuarioAcessos.Items[0].Value;
            else
            {
                objU.listid = String.Join(",", lstUsuarioAcessos.Items
                        .Cast<ListItem>()
                                .Select(li => li.Value)
                                .ToArray());
            }

            objM.usuarios.Add(objU);

            if (Session["objUser"] != null)
            {
                var user = (ConectaAD)Session["objUser"];
                isValid = objBll.InserirUsuarioGrupo(objM, user, out msg);
                objB.MostraMensagemTelaUpdatePanel(upGrupos, msg);

                if (isValid)
                {
                    LimpCamposTela();
                }
            }
            else
                objB.MostraMensagemTelaUpdatePanel(upGrupos, "Problemas contate o administrador de sistemas!!!");


        }

        protected void grdUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Status":
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    bool isValid = InativaUsuario(int.Parse(commandArgs[0]), int.Parse(commandArgs[1]), 0);

                    if (isValid)
                    {
                        objM = objBll.ListarMenuUsuario(objM);
                        CarregaGrid("grdUsuario", objM.usuarios.ToList<Object>(), grdUsuario);
                        CarregaGrid("grdPagina", objM.menus.ToList<Object>(), grdPagina);
                    }
                    else
                        objB.MostraMensagemTelaUpdatePanel(upGrupos, "Problemas contate o administrador de sistemas!!!" + " " + commandArgs[0] + " " + commandArgs[1]);
                    break;
                default:
                    break;
            }
        }

        protected void btnEnvia_Click(object sender, EventArgs e)
        {
            lbltxt.Visible = false;
            if (lstGrupoUsuario.SelectedIndex >= 0)
            {
                for (int i = 0; i < lstGrupoUsuario.Items.Count; i++)
                {
                    if (lstGrupoUsuario.Items[i].Selected)
                    {
                        if (!arrayUsuAcessos.Contains(lstGrupoUsuario.Items[i]))
                        {
                            arrayUsuAcessos.Add(lstGrupoUsuario.Items[i]);

                        }
                    }
                }
                for (int i = 0; i < arrayUsuAcessos.Count; i++)
                {
                    if (!lstUsuarioAcessos.Items.Contains(((ListItem)arrayUsuAcessos[i])))
                    {
                        lstUsuarioAcessos.Items.Add(((ListItem)arrayUsuAcessos[i]));
                    }
                    lstGrupoUsuario.Items.Remove(((ListItem)arrayUsuAcessos[i]));
                }
                lstUsuarioAcessos.SelectedIndex = -1;
            }
            else
            {
                lbltxt.Visible = true;
                lbltxt.Text = "Selecione uma opção da Lista de Usuários para mover";
            }
        }

        protected void btnEnviaTodos_Click(object sender, EventArgs e)
        {

            var teste = String.Join(",", lstGrupoUsuario.Items
                    .Cast<ListItem>()
                            .Where(li => li.Selected)
                            .Select(li => li.Value)
                            .ToArray());

            lbltxt.Visible = false;
            while (lstGrupoUsuario.Items.Count != 0)
            {
                for (int i = 0; i < lstGrupoUsuario.Items.Count; i++)
                {
                    lstUsuarioAcessos.Items.Add(lstGrupoUsuario.Items[i]);
                    lstGrupoUsuario.Items.Remove(lstGrupoUsuario.Items[i]);
                }
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            lbltxt.Visible = false;
            if (lstUsuarioAcessos.SelectedIndex >= 0)
            {
                for (int i = 0; i < lstUsuarioAcessos.Items.Count; i++)
                {
                    if (lstUsuarioAcessos.Items[i].Selected)
                    {
                        if (!arrayUsuGrupos.Contains(lstUsuarioAcessos.Items[i]))
                        {
                            arrayUsuGrupos.Add(lstUsuarioAcessos.Items[i]);
                        }
                    }
                }
                for (int i = 0; i < arrayUsuGrupos.Count; i++)
                {
                    if (!lstGrupoUsuario.Items.Contains(((ListItem)arrayUsuGrupos[i])))
                    {
                        lstGrupoUsuario.Items.Add(((ListItem)arrayUsuGrupos[i]));
                    }
                    lstUsuarioAcessos.Items.Remove(((ListItem)arrayUsuGrupos[i]));
                }
                lstGrupoUsuario.SelectedIndex = -1;
                OrderByListBox(lstGrupoUsuario);
            }
            else
            {
                lbltxt.Visible = true;
                lbltxt.Text = "Selecione uma opção da lista Usuários no Grupo para mover";
            }
        }

        protected void btnRemoveTodos_Click(object sender, EventArgs e)
        {
            RemoveTodosListaUsuario();
        }

        protected void drpGrupoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            listVinPagina.Items.Clear();
            objB.CarregaListBox(objBll.ListarUsuario(int.Parse(drpGrupoUsuario.SelectedValue.ToString())), lstUsuario);
        }
        #endregion

        #region ABA Pagina

        protected void grPagina_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdPagina"] != null)
            {
                grdPagina.PageIndex = e.NewPageIndex;
                grdPagina.DataSource = ViewState["grdPagina"];
                grdPagina.DataBind();
            }
        }

        protected void lnkInserirPagina_Click(object sender, EventArgs e)
        {
            LimpCamposTela();
            DivAcao(DivActionPagina, DivSelectPagina);
        }

        protected void grdPagina_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Status":
                    bool isValid;
                    string[] arguments = e.CommandArgument.ToString().Split(new char[] { ',' });

                    isValid = InativaPagina(int.Parse(arguments[0]), int.Parse(arguments[1]), 0);

                    if (isValid)
                    {
                        objM = objBll.ListarMenuUsuario(objM);
                        CarregaGrid("grdUsuario", objM.usuarios.ToList<Object>(), grdUsuario);
                        CarregaGrid("grdPagina", objM.menus.ToList<Object>(), grdPagina);
                    }
                    break;
                default:
                    break;
            }
        }

        protected void btnVoltarPagina_Click(object sender, EventArgs e)
        {
            DivAcao(DivSelectPagina, DivActionPagina);
        }

        protected void btnSalvarPagina_Click(object sender, EventArgs e)
        {
            string msg;
            bool isValid;
            objM.menus = new List<GrupoMenu>();

            objM.id_grupo_acessos = int.Parse(drpGrupoPagina.SelectedValue);

            if (lstPaginaAcesso.Items.Count == 0)
                objMn.listids = "0";
            else if (lstPaginaAcesso.Items.Count == 1)
                objMn.listids = lstPaginaAcesso.Items[0].Value;
            else
            {
                objMn.listids = String.Join(",", lstPaginaAcesso.Items
                        .Cast<ListItem>()
                                .Select(li => li.Value)
                                .ToArray());
            }


            objM.menus.Add(objMn);
            isValid = objBll.InserirMenuGrupo(objM, out msg);
            objB.MostraMensagemTelaUpdatePanel(upGrupos, msg);

            if (isValid)
            {
                LimpCamposTela();
            }
        }

        protected void btnEnvio_Click(object sender, EventArgs e)
        {
            lblPagina.Visible = false;
            if (lstPagina.SelectedIndex >= 0)
            {
                for (int i = 0; i < lstPagina.Items.Count; i++)
                {
                    if (lstPagina.Items[i].Selected)
                    {
                        if (!arrayPagAcessos.Contains(lstPagina.Items[i]))
                        {
                            arrayPagAcessos.Add(lstPagina.Items[i]);

                        }
                    }
                }
                for (int i = 0; i < arrayPagAcessos.Count; i++)
                {
                    if (!lstPaginaAcesso.Items.Contains(((ListItem)arrayPagAcessos[i])))
                    {
                        lstPaginaAcesso.Items.Add(((ListItem)arrayPagAcessos[i]));
                    }
                    lstPagina.Items.Remove(((ListItem)arrayPagAcessos[i]));
                }
                lstPaginaAcesso.SelectedIndex = -1;
            }
            else
            {
                lblPagina.Visible = true;
                lblPagina.Text = "Selecione uma opção da Lista de Páginas para mover";
            }
        }

        protected void btnEnvioTodos_Click(object sender, EventArgs e)
        {
            lblPagina.Visible = false;
            while (lstPagina.Items.Count != 0)
            {
                for (int i = 0; i < lstPagina.Items.Count; i++)
                {
                    lstPaginaAcesso.Items.Add(lstPagina.Items[i]);
                    lstPagina.Items.Remove(lstPagina.Items[i]);
                }
            }
        }

        protected void btnRemovs_Click(object sender, EventArgs e)
        {
            lblPagina.Visible = false;
            if (lstPaginaAcesso.SelectedIndex >= 0)
            {
                for (int i = 0; i < lstPaginaAcesso.Items.Count; i++)
                {
                    if (lstPaginaAcesso.Items[i].Selected)
                    {
                        if (!arrayPagGrupos.Contains(lstPaginaAcesso.Items[i]))
                        {
                            arrayPagGrupos.Add(lstPaginaAcesso.Items[i]);
                        }
                    }
                }
                for (int i = 0; i < arrayPagGrupos.Count; i++)
                {
                    if (!lstPagina.Items.Contains(((ListItem)arrayPagGrupos[i])))
                    {
                        lstPagina.Items.Add(((ListItem)arrayPagGrupos[i]));
                    }
                    lstPaginaAcesso.Items.Remove(((ListItem)arrayPagGrupos[i]));
                }
                lstPagina.SelectedIndex = -1;
            }
            else
            {
                lblPagina.Visible = true;
                lblPagina.Text = "Selecione uma opção da lista Páginas no Grupo para mover";
            }
        }

        protected void btnRemovsTodos_Click(object sender, EventArgs e)
        {
            RemoveTodosListaPagina();
        }

        protected void drpSistema_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstPagina.Items.Clear();
            objB.CarregaListBox(objBll.ListarMenuSistema(int.Parse(drpSistema.SelectedValue.ToString())), lstPagina);

        }

        protected void drpGrupoPagina_SelectedIndexChanged(object sender, EventArgs e)
        {
            listVinPagina.Items.Clear();
            objB.CarregaListBox(objBll.ListarPagina(int.Parse(drpGrupoPagina.SelectedValue.ToString())), listVinPagina);
        }

    

        #endregion

        #region Menu Consultar Grupo

        protected void grdPesquisa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Status":
                    if (drpConsulta.SelectedIndex > 0)
                    {
                        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                        var id_drop = drpConsulta.SelectedValue;
                        bool isValid;
                        List<Object> list;

                        if (drpConsultaPesquisa.SelectedValue == "1")
                        {
                            isValid = InativaUsuario(int.Parse(id_drop), int.Parse(commandArgs[0]), int.Parse(commandArgs[1]));
                            objM.usuarios[0].matricula = int.Parse(id_drop);
                            list=objBll.ConsultarGrupoUsuario(objM).ToList<Object>();

                        }
                        else {

                            isValid = InativaPagina(int.Parse(id_drop), int.Parse(commandArgs[0]), int.Parse(commandArgs[1]));
                            objM.menus[0].id_menu = int.Parse(id_drop);
                            list = objBll.ConsultarPaginaUsuario(objM).ToList<Object>();
                        }


                        if (isValid)
                        {
                            CarregaGrid("grdPesquisa",list , grdPesquisa);
                        }
                        else
                            objB.MostraMensagemTelaUpdatePanel(upGrupos, "Problemas contate o administrador de sistemas!!!");

                    }


                    break;
                default:
                    break;
            }
        }

        protected void drpConsultaPesquisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdPesquisa.Visible = false;

            if (drpConsultaPesquisa.SelectedIndex > 0)
            {
                if (drpConsultaPesquisa.SelectedValue == "1")
                {
                    if (ViewState["USUARIO"] != null)
                    {
                        objB.CarregaDropDowDT((DataTable)ViewState["USUARIO"], drpConsulta);

                    }

                }
                else
                {

                    if (ViewState["PAGINA"] != null)
                    {
                        objB.CarregaDropDowDT((DataTable)ViewState["PAGINA"], drpConsulta);

                    }
                }
                drpConsulta.Enabled = true;
            }
            else
            {
                drpConsulta.Items.Clear();
                drpConsulta.Items.Add(new ListItem("Selecione", "0"));
                drpConsulta.SelectedValue = "0";
                drpConsulta.Enabled = false;

            }
        }

        private bool ValidaTelaRastrear(DropDownList drpPesquisa, DropDownList drpConsulta, out int value)
        {
            value = 0;
            bool ret = false;

            if (drpPesquisa.SelectedIndex > 0)
            {

                if (drpConsulta.SelectedIndex > 0)
                {
                    value = int.Parse(drpConsulta.SelectedValue);
                    ret = true;
                }
                else
                    objB.MostraMensagemTelaUpdatePanel(upGrupos, "Selecione uma opção de" + (drpPesquisa.SelectedValue.Equals("1") ? " Usuário" : " Página"));
            }
            else
                objB.MostraMensagemTelaUpdatePanel(upGrupos, "Selecione uma opção de pesquisa");

            return ret;
        }

        protected void btnConsulta_Click(object sender, EventArgs e)
        {
            int value = 0;
            bool isValid = ValidaTelaRastrear(drpConsultaPesquisa, drpConsulta, out value);

            if (isValid)
            {

                if (drpConsultaPesquisa.SelectedValue.Equals("1"))
                {
                    objU.matricula = value;
                    objM.usuarios.Add(objU);
                    CarregaGrid("grdPesquisa", objBll.ConsultarGrupoUsuario(objM).ToList<Object>(), grdPesquisa);
                }
                else
                {
                    objMn.id_menu = value;
                    objM.menus.Add(objMn);
                    CarregaGrid("grdPesquisa", objBll.ConsultarPaginaUsuario(objM).ToList<Object>(), grdPesquisa);
                }
                grdPesquisa.Visible = true;

            }
        }

        protected void btnConsultaVoltar_Click(object sender, EventArgs e)
        {
            DivAcao(Administracao, ConsultarGrupo);
        }

        protected void btnRastrear_Click(object sender, EventArgs e)
        {
            DivAcao(ConsultarGrupo, Administracao);
            DivAcao(ConsultarGrupo, Movimentacao);
            drpConsultaPesquisa.SelectedValue = "0";
            drpConsulta.Items.Clear();
            drpConsulta.Items.Add(new ListItem("Selecione", "0"));
            drpConsulta.SelectedValue = "0";
            drpConsulta.Enabled = false;
            grdPesquisa.Visible = false;
        }

        protected void drpConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpConsulta.SelectedIndex > 0)
            {
                grdPesquisa.Visible = false;
            }
        }

        #endregion

        #region Menu Histórico de Movimentações

        protected void btnMovimentacao_Click(object sender, EventArgs e)
        {
            if (ViewState["USUARIO"] != null)
            {
                objB.CarregaDropDowDT((DataTable)ViewState["USUARIO"], drpMovimentacaoUsuario);
                grdMovimentacao.Visible = false;
                DivAcao(Movimentacao, Administracao);
                DivAcao(Movimentacao, ConsultarGrupo);
            }

        }

        protected void btnMovimentarSalvar_Click(object sender, EventArgs e)
        {
            if (drpMovimentacaoUsuario.SelectedIndex > 0)
            {
                ObjMov.id_usuario = int.Parse(drpMovimentacaoUsuario.SelectedValue);
                grdMovimentacao.Visible = true;
                CarregaGrid("Movimentacao", ObjBllMov.Consultar(ObjMov).ToList<Object>(), grdMovimentacao);
                btnMovimentarExcel.Visible = grdMovimentacao.Rows.Count > 0;
            }
            else
                objB.MostraMensagemTelaUpdatePanel(upGrupos, "Selecione uma opção de Usuário");
        }

        protected void btnMovimentarVoltar_Click(object sender, EventArgs e)
        {
            DivAcao(Administracao, Movimentacao);
        }

        protected void grdMovimentacao_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["Movimentacao"] != null)
            {
                grdMovimentacao.PageIndex = e.NewPageIndex;
                grdMovimentacao.DataSource = ViewState["Movimentacao"];
                grdMovimentacao.DataBind();
            }
        }

        protected void btnMovimentarExcel_Click(object sender, EventArgs e)
        {
            if (grdMovimentacao.Rows.Count > 0)
            {
                var nomeArquivo = Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + "_Historico_Movimentacoes" + ".xls";
                objB.ExportarExcel(nomeArquivo, grdMovimentacao);
            }
            else
                objB.MostraMensagemTela(this, "Problema encontrado ao tentar exportar a Planilha.");

        }

        protected void drpMovimentacaoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdMovimentacao.Visible = false;
            btnMovimentarExcel.Visible = false;
        }
        #endregion

        #endregion

        #region Métodos
        private void CarregaGrid(string nameView, List<Object> list, GridView grid)
        {

            ViewState[nameView] = list;
            grid.DataSource = ViewState[nameView];
            grid.DataBind();
        }

        private bool ValidaCamposTela(TextBox text, DropDownList drp, out string mensagem)
        {

            StringBuilder msg = new StringBuilder();
            msg.Append("ERRO!\\n");
            bool isErro = false;
            if (drp.SelectedIndex < 1)
            {
                msg.Append("1 Selecione uma opção de busca.\\n");
                isErro = true;
            }

            if (text.Text == "")
            {
                msg.Append("2 Digite no campo de busca! ");
                isErro = true;
            }
            mensagem = msg.ToString();
            return isErro;
        }

        private void AlteraLabelGrupo(string texto)
        {

            if (!string.IsNullOrEmpty(texto))
            {
                lblGrupo.ForeColor = System.Drawing.Color.Black;
                lblGrupo.Text = "Grupo Selecionado: " + texto;
            }
            else
            {
                lblGrupo.ForeColor = System.Drawing.Color.Red;
                lblGrupo.Text = "Nenhum Grupo Selecionado";
            }
        }

        private void LimpCamposTela()
        {

            txtArea.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtNome.Text = string.Empty;
            drpGrupoPagina.SelectedIndex = 0;
            drpGrupoUsuario.SelectedIndex = 0;
            RemoveTodosListaUsuario();
            RemoveTodosListaPagina();
            drpSistema.SelectedIndex = 0;
            listVinPagina.Items.Clear();
            lstUsuario.Items.Clear();
            lstPagina.Items.Clear();
            objB.CarregaListBox(objBll.ListarMenuSistema(0), lstPagina);

        }

        private void CarregaCamposTela()
        {

            CarregaGrid("grdGrupo", objBll.ListarGrupo(objM).ToList<Object>(), grdGrupo);

            DataSet ds = objBll.ListarDrop();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    objB.CarregaListBox(ds.Tables[0], lstPagina);
                    ViewState["PAGINA"] = ds.Tables[0];
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    objB.CarregaListBox(ds.Tables[1], lstGrupoUsuario);
                    ViewState["USUARIO"] = ds.Tables[1];
                }
                if (ds.Tables[2].Rows.Count > 0)
                {

                    objB.CarregaDropDowDT(ds.Tables[2], drpGrupoUsuario);
                    objB.CarregaDropDowDT(ds.Tables[2], drpGrupoPagina);

                }
            }

            objB.CarregaDropDowList(drpSistema, new SistemaBLL().Listar().ToList<object>(), "Nome", "codigo");

            DivAcao(Administracao, ConsultarGrupo);
            DivAcao(Administracao, Movimentacao);
            DivAcao(DivSelectGrupo, DivActionGrupo);
            DivAcao(DivSelectUsuario, DivActionUsuario);
            DivAcao(DivSelectPagina, DivActionPagina);
        }

        private void DivAcao(HtmlGenericControl divExibir, HtmlGenericControl divOcultar)
        {

            divExibir.Visible = true;
            divOcultar.Visible = false;

        }

        private void OrderByListBox(ListBox list)
        {

            Dictionary<string, string> slBrowser = new Dictionary<string, string>();

            foreach (ListItem lstItem in list.Items)
            {
                slBrowser.Add(lstItem.Value, lstItem.Text);
            }

            var sortedDic = (from dic in slBrowser orderby dic.Value ascending select dic);

            list.DataTextField = "Value";
            list.DataValueField = "Key";
            list.DataSource = sortedDic;
            list.DataBind();

        }

        private void RemoveTodosListaUsuario()
        {

            lbltxt.Visible = false;
            while (lstUsuarioAcessos.Items.Count != 0)
            {
                for (int i = 0; i < lstUsuarioAcessos.Items.Count; i++)
                {
                    lstGrupoUsuario.Items.Add(lstUsuarioAcessos.Items[i]);
                    lstUsuarioAcessos.Items.Remove(lstUsuarioAcessos.Items[i]);
                }
            }

            OrderByListBox(lstGrupoUsuario);

        }

        private void RemoveTodosListaPagina()
        {

            lbltxt.Visible = false;
            while (lstPaginaAcesso.Items.Count != 0)
            {
                for (int i = 0; i < lstPaginaAcesso.Items.Count; i++)
                {
                    lstPagina.Items.Add(lstPaginaAcesso.Items[i]);
                    lstPaginaAcesso.Items.Remove(lstPaginaAcesso.Items[i]);
                }
            }

            OrderByListBox(lstGrupoUsuario);

        }

        private bool InativaUsuario(int matricula, int status, int grupo_grid)
        {
            bool isValid;
            int grupo_selecionado = int.Parse(hdfGrupo.Value == "" ? "0" : hdfGrupo.Value);
            objM.id_grupo_acessos = grupo_grid == 0 ? grupo_selecionado : grupo_grid;
            objU.matricula = matricula;
            objU.id_status = status == 0 ? 1 : 0;
            objM.usuarios.Add(objU);

            if (Session["objUser"] != null)
            {
                var user = (ConectaAD)Session["objUser"];
                isValid = objBll.InativarUsuarioGrupo(objM, user);
            }
            else
                isValid = false;

            return isValid;

        }

        private bool InativaPagina(int id_menu, int status, int grupo_grid)
        {

            bool isValid = false;
            int grupo_selecionado = int.Parse(hdfGrupo.Value == "" ? "0" : hdfGrupo.Value);
            objM.id_grupo_acessos = grupo_grid == 0 ? grupo_selecionado : grupo_grid;
            objMn.id_menu = id_menu;
            objMn.id_status = status == 0 ? 1 : 0;
            objM.menus.Add(objMn);

            isValid = objBll.InativarPaginaGrupo(objM);

            return isValid;

        }
        #endregion

    }
}