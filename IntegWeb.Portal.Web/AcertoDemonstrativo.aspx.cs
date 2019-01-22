using IntegWeb.Entidades;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Previdencia.Aplicacao.BLL;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{

    public partial class AcertoDemonstrativo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            init_SSO_Session();

            TbUpload_Mensagem.Visible = false;

            if (!IsPostBack)
            {
                if (Request.Form.AllKeys.Length > 0)
                {
                    string[] Params = ((HiddenField)PreviousPage.FindControl("ctl00$ContentPlaceHolder1$hidParamAcertos")).Value.Split(',');
                    txtMesRef.Text = Params[0];
                    txtAnoRef.Text = Params[1];
                    txtDtRepasse.Text = Params[2];
                    txtDtCredito.Text = Params[3];

                    hGRUPO_PORTAL.Value = ((DropDownList)PreviousPage.FindControl("ctl00$ContentPlaceHolder1$ddlGrupoNovo")).SelectedValue;
                    hCOD_ARQ_PATS.Value = ((HiddenField)PreviousPage.FindControl("ctl00$ContentPlaceHolder1$hidCodArquivos")).Value;
                    hCOD_EMPRS.Value = ((HiddenField)PreviousPage.FindControl("ctl00$ContentPlaceHolder1$hidCodEmprs")).Value;
                    hCOD_STATUS.Value = ((HiddenField)PreviousPage.FindControl("ctl00$ContentPlaceHolder1$hidCodStatus")).Value;
       
                    string[] aStatus =  hCOD_STATUS.Value.Split(',');

                    if (aStatus.Contains("3") ||
                        aStatus.Contains("4") ||
                        aStatus.Contains("6") ||
                        aStatus.Contains("7") ||
                        aStatus.Contains("8") ||
                        aStatus.Contains("9")) {
                        btnNovoLancamento.Enabled = false;
                        modalPopUpNovoLancamentoErro.Show();
                    }                 
                }
                grdReembolsoAjustes.Sort("COD_VERBA", SortDirection.Ascending);
            }

            //txtDtRepasse.Text = txtDtRepasse.Text.Split(',')[0];
            //txtDtCredito.Text = txtDtCredito.Text.Split(',')[0];
        }

        protected void btnImprimirReembolso_Ajuste_Click(object sender, EventArgs e)
        {
            //String sCOD_ARQ_PATS = hCOD_ARQ_PATS.Value;
            //String sCOD_EMPRS = hCOD_EMPRS.Value;
            short AnoRef = short.Parse(txtAnoRef.Text);
            short MesRef = short.Parse(txtMesRef.Text);
            DownloadDemonstrativo(AnoRef, MesRef, DateTime.Parse(txtDtRepasse.Text), DateTime.Parse(txtDtCredito.Text), hCOD_EMPRS.Value, hCOD_ARQ_PATS.Value, hGRUPO_PORTAL.Value);
            grdReembolsoAjustes.DataBind();
        }

        protected void btnNovoLancamento_Click(object sender, EventArgs e)
        {
            txtCodEmpresa.Text = "";
            txtNumMatricula.Text = "";
            hTipoVerba.Value = "";
            lblTipo.Text = "";
            txtCodVerba.Text = "";
            txtCodVerbaPatrocina.Text = "";
            txtVlrLancamento.Text = "";
            //txtVlrAcerto.Text = "";            
            pnlNovoLancamento.Visible = true;
        }

        protected void btOk_Click(object sender, ImageClickEventArgs e)
        {
            Resultado res = new Resultado();
            ArqPatrocinaDemonstrativoBLL Demons = new ArqPatrocinaDemonstrativoBLL();
            PRE_TBL_ARQ_PAT_DEMONSTRA newDemonstrativo = new PRE_TBL_ARQ_PAT_DEMONSTRA();
            PRE_TBL_ARQ_PAT_DEMONSTRA_DET newLancamento = new PRE_TBL_ARQ_PAT_DEMONSTRA_DET();

            if (ValidarNovoLancamento()) 
            {

                UsuarioPortal user = new UserEngineBLL()
                            .GetCurrentUser((ConectaAD)Session["objUser"],
                                            (Singlesignon)Session["SingleSignOn"]);

                newDemonstrativo.GRUPO_PORTAL = hGRUPO_PORTAL.Value;
                newDemonstrativo.ANO_REF = Util.String2Short(txtAnoRef.Text);
                newDemonstrativo.MES_REF = Util.String2Short(txtMesRef.Text);
                newDemonstrativo.DAT_REPASSE = Util.String2Date(txtDtRepasse.Text);
                newDemonstrativo.DAT_CREDITO = Util.String2Date(txtDtCredito.Text);
                newDemonstrativo.DTH_INCLUSAO = DateTime.Now;
                newDemonstrativo.LOG_INCLUSAO = user.login;

                newLancamento.TIP_LANCAMENTO = "M";
                newLancamento.TIP_CRED_DEB = hTipoVerba.Value; //lblTipo.Text;
                newLancamento.COD_EMPRS = Util.String2Short(txtCodEmpresa.Text);
                newLancamento.NUM_RGTRO_EMPRG = Util.String2Int64(txtNumMatricula.Text);
                newLancamento.COD_VERBA = Util.String2Int32(txtCodVerba.Text);
                newLancamento.COD_VERBA_PATROCINA = txtCodVerbaPatrocina.Text;
                newLancamento.DCR_LANCAMENTO = "";
                newLancamento.VLR_LANCAMENTO = Util.String2Decimal(txtVlrLancamento.Text) ?? 0;
                newLancamento.COD_ARQ_PAT_LINHA = null;
                newLancamento.DTH_INCLUSAO = DateTime.Now;
                newLancamento.LOG_INCLUSAO = user.login;

                //res = Demons.SaveData(newLancamento, Util.String2Decimal(txtVlrAcerto.Text) ?? 0);
                res = Demons.SaveData(newDemonstrativo);
                if (res.Ok)
                {
                    newLancamento.COD_ARQ_PAT_DEMO = Convert.ToInt32(res.CodigoCriado);
                    res = Demons.SaveData(newLancamento);
                }

                if (res.Ok)
                {
                    pnlNovoLancamento.Visible = false;
                    grdReembolsoAjustes.DataBind();
                } else {
                    MostraMensagem(TbUpload_Mensagem, res.Mensagem);
                }
            
            }

        }

        protected void btCancel_Click(object sender, ImageClickEventArgs e)
        {
            pnlNovoLancamento.Visible = false;
        }

        protected void btCarregaVerbaPatrocina_Click(object sender, ImageClickEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCodVerbaPatrocina.Text))
            {
                ArqPatrocinaDemonstrativoBLL Demons = new ArqPatrocinaDemonstrativoBLL();
                PRE_TBL_ARQ_PAT_VERBA VerbaPatrocinadora = Demons.GetVerbaPatrocinadora(Util.String2Short(txtCodEmpresa.Text), null, txtCodVerbaPatrocina.Text);
                hTipoVerba.Value = "";
                lblTipo.Text = "";
                if (VerbaPatrocinadora == null)
                {
                    txtCodVerba.Text = txtCodVerbaPatrocina.Text;
                    btCarregaVerba_Click(sender, e);
                    //MostraMensagem(TbUpload_Mensagem, "Verba não localizada");
                }
                else
                {
                    TB_SCR_SUBGRUPO_FINANC_VERBA SubGrupoVerba = Demons.GetTipoLancamento(VerbaPatrocinadora.COD_VERBA);
                    if (SubGrupoVerba == null)
                    {
                        MostraMensagem(TbUpload_Mensagem, "Verba não localizada");
                    }
                    else
                    {
                        txtCodVerba.Text = VerbaPatrocinadora.COD_VERBA.ToString();
                        //lblTipo.Text = SubGrupoVerba.CRED_DEB;
                        hTipoVerba.Value = SubGrupoVerba.CRED_DEB;
                        switch (SubGrupoVerba.CRED_DEB)
                        {
                            case "C":
                                lblTipo.Text = "Crédito";
                                break;
                            case "D":
                                lblTipo.Text = "Débito";
                                break;
                            default:
                                lblTipo.Text = "";
                                break;
                        }
                    }
                }
            }
            txtVlrLancamento.Focus();
        }

        protected void btCarregaVerba_Click(object sender, ImageClickEventArgs e)
        {
            if (Util.String2Int32(txtCodVerba.Text) > 0)
            {
                ArqPatrocinaDemonstrativoBLL Demons = new ArqPatrocinaDemonstrativoBLL();
                TB_SCR_SUBGRUPO_FINANC_VERBA SubGrupoVerba = Demons.GetTipoLancamento(Util.String2Int32(txtCodVerba.Text));
                lblTipo.Text = "";
                hTipoVerba.Value = "";
                if (SubGrupoVerba == null)
                {
                    MostraMensagem(TbUpload_Mensagem, "Verba não localizada");
                }
                else
                {
                    PRE_TBL_ARQ_PAT_VERBA VerbaFuncesp = Demons.GetVerbaPatrocinadora(Util.String2Short(txtCodEmpresa.Text), Util.String2Int32(txtCodVerba.Text), null);
                    if (VerbaFuncesp != null)
                    {
                        txtCodVerbaPatrocina.Text = VerbaFuncesp.COD_VERBA_PATROCINA;
                    }

                    //lblTipo.Text = SubGrupoVerba.CRED_DEB;
                    hTipoVerba.Value = SubGrupoVerba.CRED_DEB;

                    switch (SubGrupoVerba.CRED_DEB)
                    {
                        case "C":
                            lblTipo.Text = "Crédito";
                            break;
                        case "D":
                            lblTipo.Text = "Débito";
                            break;
                        default:
                            lblTipo.Text = "";
                            break;
                    }
                    
                }
            }
            txtVlrLancamento.Focus();
        }

        private bool ValidarNovoLancamento()
        {
            decimal dNum;
            int pNUM_RGTRO_EMPRG;            
            short pCOD_EMPRS;
            int pCOD_VERBA;

            if (String.IsNullOrEmpty(txtCodEmpresa.Text))
            {
                MostraMensagem(TbUpload_Mensagem, "Campo Empresa é obrigatorio.");
                return false;
            }
            else if (!short.TryParse(txtCodEmpresa.Text, out pCOD_EMPRS))
            {
                MostraMensagem(TbUpload_Mensagem, "Campo Empresa inválido.");
                return false;
            }

            if (String.IsNullOrEmpty(txtNumMatricula.Text))
            {
                MostraMensagem(TbUpload_Mensagem, "Campo Matrícula é obrigatorio.");
                return false;
            }
            else if (!int.TryParse(txtNumMatricula.Text, out pNUM_RGTRO_EMPRG))
            {
                MostraMensagem(TbUpload_Mensagem, "Campo Matrícula inválido.");
                return false;
            }

            if (String.IsNullOrEmpty(txtCodVerba.Text))
            {
                MostraMensagem(TbUpload_Mensagem, "Campo Verba é obrigatorio.");
                return false;
            }
            //else if (!int.TryParse(txtCodVerba.Text, out iNum))
            //{
            //    MostraMensagem(TbUpload_Mensagem, "Campo Verba inválido.");
            //    return false;
            //}

            //if (String.IsNullOrEmpty(txtCodVerbaPatrocina.Text))
            //{
            //    MostraMensagem(TbUpload_Mensagem, "Campo Verba Funcesp é obrigatorio.");
            //    return false;
            //}
            //else if (!int.TryParse(txtCodVerbaPatrocina.Text, out iNum))
            //{
            //    MostraMensagem(TbUpload_Mensagem, "Campo Verba Funcesp inválido.");
            //    return false;
            //}

            if (String.IsNullOrEmpty(lblTipo.Text))
            {
                MostraMensagem(TbUpload_Mensagem, "Tipo da verba é obrigatorio.");
                return false;
            }

            if (String.IsNullOrEmpty(txtVlrLancamento.Text))
            {
                MostraMensagem(TbUpload_Mensagem, "Campo Valor é obrigatorio.");
                return false;
            }
            else if (!decimal.TryParse(txtVlrLancamento.Text, out dNum))
            {
                MostraMensagem(TbUpload_Mensagem, "Campo Valor inválido.");
                return false;
            }
            else if (dNum < 0)
            {
                MostraMensagem(TbUpload_Mensagem, "Valor inválido. O lançamento deve ser positivo.");
                return false;
            }

            int.TryParse(txtCodVerba.Text, out pCOD_VERBA);

            ArqPatrocinaDemonstrativoBLL DemonsBLL = new ArqPatrocinaDemonstrativoBLL();
            PRE_VW_ARQ_PAT_DEMONSTRATIVO Demons_det = DemonsBLL.GetLancamento(hGRUPO_PORTAL.Value, short.Parse(txtMesRef.Text), short.Parse(txtAnoRef.Text), pCOD_EMPRS, pNUM_RGTRO_EMPRG, pCOD_VERBA, "M");

            if (Demons_det != null)
            {
                if (Demons_det.TIP_LANCAMENTO == "P")
                {
                    MostraMensagem(TbUpload_Mensagem, "Já existe um <b>lançamento</b> para este ano/mês ref.");
                }
                else
                {
                    MostraMensagem(TbUpload_Mensagem, "Já existe um <b>acerto</b> para este ano/mês ref.");
                }
                return false;
            }

            return true;
        }

        private bool ValidarLancamento(long COD_DEMO_DET, string COD_EMPRS, string NUM_RGTRO_EMPRG, string VLR_LANCAMENTO, string COD_VERBA)
        {
            decimal dNum;
            int pNUM_RGTRO_EMPRG;
            short pCOD_EMPRS;
            int pCOD_VERBA;

            if (String.IsNullOrEmpty(COD_EMPRS))
            {
                MostraMensagem(TbUpload_Mensagem, "Campo Empresa é obrigatorio.");
                return false;
            }
            else if (!short.TryParse(COD_EMPRS, out pCOD_EMPRS))
            {
                MostraMensagem(TbUpload_Mensagem, "Campo Empresa inválido.");
                return false;
            }

            if (String.IsNullOrEmpty(NUM_RGTRO_EMPRG))
            {
                MostraMensagem(TbUpload_Mensagem, "Campo Matrícula é obrigatorio.");
                return false;
            }
            else if (!int.TryParse(NUM_RGTRO_EMPRG, out pNUM_RGTRO_EMPRG))
            {
                MostraMensagem(TbUpload_Mensagem, "Campo Matrícula inválido.");
                return false;
            }

            if (String.IsNullOrEmpty(VLR_LANCAMENTO))
            {
                MostraMensagem(TbUpload_Mensagem, "Campo Valor é obrigatorio.");
                return false;
            }
            else if (!decimal.TryParse(VLR_LANCAMENTO, out dNum))
            {
                MostraMensagem(TbUpload_Mensagem, "Campo Valor inválido.");
                return false;
            }
            else if (dNum < 0)
            {
                MostraMensagem(TbUpload_Mensagem, "Valor inválido. O lançamento deve ser positivo.");
                return false;
            }

            int.TryParse(COD_VERBA, out pCOD_VERBA);

            ArqPatrocinaDemonstrativoBLL DemonsBLL = new ArqPatrocinaDemonstrativoBLL();
            PRE_VW_ARQ_PAT_DEMONSTRATIVO Demons_det = DemonsBLL.GetLancamento(hGRUPO_PORTAL.Value, short.Parse(txtMesRef.Text), short.Parse(txtAnoRef.Text), pCOD_EMPRS, pNUM_RGTRO_EMPRG, pCOD_VERBA, null);

            if (Demons_det != null && Demons_det.COD_DEMO_DET != COD_DEMO_DET)
            {
                if (Demons_det.TIP_LANCAMENTO == "P")
                {
                    MostraMensagem(TbUpload_Mensagem, "Já existe um <b>lançamento</b> para este ano/mês ref.");
                }
                else
                {
                    MostraMensagem(TbUpload_Mensagem, "Já existe um <b>acerto</b> para este ano/mês ref.");
                }
                return false;
            }

            return true;
        }

        private void DownloadDemonstrativo(short? ANO_REF, 
                                           short? MES_REF, 
                                           DateTime DAT_REPASSE, 
                                           DateTime DAT_CREDITO, 
                                           String COD_EMPRS = "",
                                           String COD_ARQ_PATS = "", 
                                           String GRUPO_PORTAL = "")
        {
            Relatorio rel = new Relatorio();
            RelatorioBLL relBLL = new RelatorioBLL();
            String relatorio_nome = "Rel_Demo_Repasse.rpt";
            rel = relBLL.Listar(relatorio_nome);

            if (String.IsNullOrEmpty(COD_EMPRS))
            {
                COD_EMPRS = "0";
            }

            rel.get_parametro("COD_EMPRS").valor = COD_EMPRS;
            rel.get_parametro("COD_ARQ_PATS").valor = COD_ARQ_PATS;
            rel.get_parametro("GRUPO_PORTAL").valor = GRUPO_PORTAL;
            rel.get_parametro("ANO_REF").valor = ANO_REF.ToString();
            rel.get_parametro("MES_REF").valor = MES_REF.ToString();

            Session[rel.relatorio] = rel;
            AbrirNovaAba(upArqPatrocinadoras, "RelatorioWeb.aspx?Relatorio_nome=" + rel.relatorio + "&PromptParam=false&Popup=true&Alert=false", rel.relatorio);

            //if (ReportCrystal.Visible) ReportCrystal.VisualizaRelatorio();
        }

        protected void grdReembolsoAjustes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdReembolsoAjustes.EditIndex = e.NewEditIndex;
        }

        protected void grdReembolsoAjustes_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            long PK = 0;
            long.TryParse(e.CommandArgument.ToString(), out PK);

            ArqPatrocinaDemonstrativoBLL Demons = new ArqPatrocinaDemonstrativoBLL();

            switch (e.CommandName)
            {
                case "Gravar":

                    Resultado res = new Resultado();
                    PRE_TBL_ARQ_PAT_DEMONSTRA_DET updLancamento = new PRE_TBL_ARQ_PAT_DEMONSTRA_DET();

                    UsuarioPortal user = new UserEngineBLL()
                                .GetCurrentUser((ConectaAD)Session["objUser"],
                                                (Singlesignon)Session["SingleSignOn"]);                    

                    string COD_EMPRS = ((TextBox)grdReembolsoAjustes.Rows[grdReembolsoAjustes.EditIndex].FindControl("txtCodEmpresa")).Text;
                    string NUM_RGTRO_EMPRG = ((TextBox)grdReembolsoAjustes.Rows[grdReembolsoAjustes.EditIndex].FindControl("txtNumMatricula")).Text;
                    string VLR_LANCAMENTO = ((TextBox)grdReembolsoAjustes.Rows[grdReembolsoAjustes.EditIndex].FindControl("txtVlrLancamento")).Text;
                    string COD_VERBA = ((Label)grdReembolsoAjustes.Rows[grdReembolsoAjustes.EditIndex].FindControl("lblCodVerba")).Text;
                    //string VLR_ACERTO = ((TextBox)grdReembolsoAjustes.Rows[grdReembolsoAjustes.EditIndex].FindControl("txtVlrAcerto")).Text;

                    if (ValidarLancamento(PK, COD_EMPRS, NUM_RGTRO_EMPRG, VLR_LANCAMENTO, COD_VERBA))
                    {

                        decimal dVLR_LANCAMENTO;
                        decimal.TryParse(VLR_LANCAMENTO, out dVLR_LANCAMENTO);

                        //decimal dVLR_ACERTO;
                        //decimal.TryParse(VLR_ACERTO, out dVLR_ACERTO);

                        updLancamento = Demons.GetLancamento(PK, "M");

                        updLancamento.COD_EMPRS = short.Parse(COD_EMPRS);
                        updLancamento.NUM_RGTRO_EMPRG = long.Parse(NUM_RGTRO_EMPRG);
                        updLancamento.VLR_LANCAMENTO = dVLR_LANCAMENTO;
                        updLancamento.DTH_INCLUSAO = DateTime.Now;
                        updLancamento.LOG_INCLUSAO = user.login;
                        //updLancamento.GRUPO_PORTAL = hGRUPO_PORTAL.Value;

                        //res = Demons.SaveData(updLancamento, dVLR_ACERTO);
                        res = Demons.SaveData(updLancamento);

                        if (res.Ok)
                        {
                            pnlNovoLancamento.Visible = false;
                            grdReembolsoAjustes.EditIndex = -1;
                            grdReembolsoAjustes.DataBind();
                        }
                        else
                        {
                            MostraMensagem(TbUpload_Mensagem, res.Mensagem);
                        }
                    }

                    break;

                case "Excluir":

                    res = Demons.DeleteData(PK);
                    if (res.Ok)
                    {
                        pnlNovoLancamento.Visible = false;
                        grdReembolsoAjustes.DataBind();
                    } else {
                        MostraMensagem(TbUpload_Mensagem, res.Mensagem);
                    }
                    break;

                default:
                    break;
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            //Server.Transfer("ArquivoPatrocinadora.aspx");
            Response.Redirect("ArquivoPatrocinadora.aspx");
        }

    }
}