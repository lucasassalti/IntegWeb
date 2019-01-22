using IntegWeb.Entidades.Saude.Cobranca;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.BLL.Cobranca;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Saude.Web
{
    public partial class CadastroCisao : BasePage
    {
        #region Atributos
        CisaoFusaoBLL objBLL = new CisaoFusaoBLL();
        CisaoFusao obj = new CisaoFusao();
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (txtCodEmpAtu.Text == "" && txtMatriculaAtu.Text == "")
            {
                ckConsulta.Enabled = false;
                txtDigito.Enabled = false;
            }
            else
            {
                ckConsulta.Enabled = true;
                txtDigito.Enabled = true;
            }
        }

        #region .:ABA 1:.

        protected void btnConsultar_Click(object sender, EventArgs e)
        {

            try
            {
                string mesangem = ValidaTela();
                if (mesangem.Equals(""))
                {
                    //int? mes = int.Parse(drpMes.SelectedValue);
                    //int? ano = 0;
                    //int? matricula = 0;

                    obj.mes = int.Parse(drpMes.SelectedValue);
                    obj.ano = 0;
                    obj.Num_Rgtro_Emprg_Ant = 0;
                    obj.Num_Rgtro_Emprg_Atu = 0;

                    if (txtMatricula.Text != "")
                    {
                        obj.Num_Rgtro_Emprg_Ant = int.Parse(txtMatricula.Text);
                        obj.Num_Rgtro_Emprg_Atu = int.Parse(txtMatricula.Text);
                    }
                    else
                    {
                        obj.Num_Rgtro_Emprg_Ant = null;
                        obj.Num_Rgtro_Emprg_Atu = null;
                    }

                    if (txtAno.Text != "" && drpMes.SelectedValue != "0")
                    {
                        obj.ano = int.Parse(txtAno.Text);
                        obj.mes = int.Parse(drpMes.SelectedValue);
                    }
                    else
                    {
                        obj.ano = null;
                        obj.mes = null;
                    }

                    Session["obj"] = obj;
                    DataTable dt = objBLL.BuscaCisaoCadastro(obj);
                    CarregaGrid("grdFusao", dt, grdFusao);

                    if (grdFusao.Visible == false)
                        grdFusao.Visible = true;
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "Atenção\\n\\n" + mesangem);
                }
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upCisao, "Atenção \\n\\nRegistro não inserido.\\nMotivo:\\n" + ex.Message);
            }


        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtAno.Text = "";
            txtMatricula.Text = "";
            drpMes.SelectedIndex = 0;
            grdFusao.DataBind();
            grdFusao.Visible = false;

        }

        protected void grdFusao_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdFusao.EditIndex = -1;
            ListaGrid();
        }

        protected void grdFusao_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (Session["objUser"] != null)
                {
                    var user = (ConectaAD)Session["objUser"];
                    string msg = "";
                    obj.Cod_Emprs_Ant = int.Parse(grdFusao.DataKeys[e.RowIndex].Values["COD_EMPRS_ANT"].ToString());
                    obj.Num_Rgtro_Emprg_Ant = int.Parse(grdFusao.DataKeys[e.RowIndex].Values["NUM_RGTRO_EMPRG_ANT"].ToString());
                    obj.matricula = user.login;
                    bool ret = objBLL.Deletar(out msg, obj);

                    MostraMensagemTelaUpdatePanel(upCisao, msg);

                    if (ret)
                    {
                        DataTable dt = objBLL.BuscaCisao(obj);
                        CarregaGrid("grdFusao", dt, grdFusao);
                    }
                    else
                        MostraMensagemTelaUpdatePanel(upCisao, "Problemas contate o administrador do sistema!");
                }
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upCisao, "Atenção \\n\\nRegistro não atualizado.\\nMotivo:\\n" + ex.Message);
            }



        }

        protected void grdFusao_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdFusao.EditIndex = e.NewEditIndex;
            ListaGrid();
        }

        protected void grdFusao_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                if (Session["objUser"] != null && Session["obj"] != null)
                {
                    var user = (ConectaAD)Session["objUser"];

                    int index = e.RowIndex;
                    obj.Cod_Emprs_Ant = int.Parse(grdFusao.DataKeys[e.RowIndex].Values["COD_EMPRS_ANT"].ToString());
                    obj.Num_Rgtro_Emprg_Ant = int.Parse(grdFusao.DataKeys[e.RowIndex].Values["NUM_RGTRO_EMPRG_ANT"].ToString());
                    DateTime dtBase = DateTime.MinValue;
                    DateTime dtAtu = DateTime.MinValue;
                    Int32 empATu = 0;
                    Int32 maATu = 0;
                    Int32 digito = 0;
                    string msg = "";

                    TextBox txtCdEmpAtu = grdFusao.Rows[index].FindControl("txtCdEmpAtu") as TextBox;
                    TextBox txtCdMatAtu = grdFusao.Rows[index].FindControl("txtCdMatAtu") as TextBox;
                    TextBox txtDig = grdFusao.Rows[index].FindControl("txtDig") as TextBox;
                    TextBox txtDtBase = grdFusao.Rows[index].FindControl("txtDtBase") as TextBox;
                    TextBox txtdtAtu = grdFusao.Rows[index].FindControl("txtdtAtu") as TextBox;

                    if (DateTime.TryParse(txtDtBase.Text, out dtBase))
                    {
                        obj.Dat_Base_Cisao = dtBase;
                    }

                    if (DateTime.TryParse(txtdtAtu.Text, out dtAtu))
                    {
                        obj.Dat_Atualizacao = dtAtu;
                    }


                    if (Int32.TryParse(txtCdEmpAtu.Text, out empATu))
                    {
                        obj.Cod_Emprs_Atu = empATu;
                    }


                    if (Int32.TryParse(txtCdMatAtu.Text, out maATu))
                    {
                        obj.Num_Rgtro_Emprg_Atu = maATu;
                    }

                    if (Int32.TryParse(txtDig.Text, out digito))
                    {
                        obj.Num_Digver_Atu = digito;
                    }
                    obj.matricula = user.login;

                    bool ret = objBLL.ValidaCampos(out msg, obj, true);

                    MostraMensagemTelaUpdatePanel(upCisao, msg);
                    if (ret)
                    {
                        grdFusao.EditIndex = -1;
                        CisaoFusao obs = (CisaoFusao)Session["obj"];
                        DataTable dt = objBLL.BuscaCisao(obs);
                        CarregaGrid("grdFusao", dt, grdFusao);
                    }
                    else
                        MostraMensagemTelaUpdatePanel(upCisao, "Problemas contate o administrador do sistema!");
                }
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upCisao, "Atenção \\n\\nRegistro não atualizado.\\nMotivo:\\n" + ex.Message);
            }
        }

        protected void grdFusao_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdFusao"] != null)
            {
                grdFusao.PageIndex = e.NewPageIndex;
                grdFusao.DataSource = ViewState["grdFusao"];
                grdFusao.DataBind();
            }
        }
       
        #endregion

        #region .:ABA 2:.
       
        protected void txtCodEmpAnt_TextChanged(object sender, EventArgs e)
        {
            BuscaEmpresa(txtCodEmpAnt, lblEmpAnt);
        }

        protected void txtCodEmpAtu_TextChanged(object sender, EventArgs e)
        {
            BuscaEmpresa(txtCodEmpAtu, lblEmpAtu);
        }

        protected void txtMatriculaAnt_TextChanged(object sender, EventArgs e)
        {
            BuscaEmpresaMatriculaAnt(lblMatAnt);
        }

        protected void txtMatriculaAtu_TextChanged(object sender, EventArgs e)
        {
            BuscaEmpresaMatriculaAtu(lblMatAtu);
        }

        //protected void btnPesquisa_Click(object sender, EventArgs e)
        //{

        //}

        protected void ckConsulta_CheckedChanged(object sender, EventArgs e)
        {
            divMatricula.Visible = !divMatricula.Visible;
            //txtDigito.Text = "";
            grdPesquisa.Visible = false;

            if (txtCodEmpAtu.Text != "" && txtMatriculaAtu.Text != "")
            {
                DataTable dt = objBLL.BuscaDigito(int.Parse(txtCodEmpAtu.Text), int.Parse(txtMatriculaAtu.Text));

                grdPesquisa.Visible = true;
                CarregaGrid("grdPesquisa", dt, grdPesquisa);

                //txtDigito.Text = grdPesquisa.Rows[0].Cells[3].Text;

            }
            else
                MostraMensagemTelaUpdatePanel(upCisao, "Digite a matrícula!");
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["objUser"] != null)
                {
                    var user = (ConectaAD)Session["objUser"];
                    DateTime dtBase = DateTime.MinValue;
                    DateTime dtAtu = DateTime.MinValue;
                    Int32 empAnt = 0;
                    Int32 empATu = 0;
                    Int32 maAnt = 0;
                    Int32 maATu = 0;
                    Int32 digito = 0;
                    string msg = "";

                    if (DateTime.TryParse(txtBaseCisao.Text, out dtBase))
                    {
                        obj.Dat_Base_Cisao = dtBase;
                    }

                    if (DateTime.TryParse(txtBaseAtu.Text, out dtAtu))
                    {
                        obj.Dat_Atualizacao = dtAtu;
                    }

                    if (Int32.TryParse(txtCodEmpAnt.Text, out empAnt))
                    {
                        obj.Cod_Emprs_Ant = empAnt;
                    }

                    if (Int32.TryParse(txtCodEmpAtu.Text, out empATu))
                    {
                        obj.Cod_Emprs_Atu = empATu;
                    }

                    if (Int32.TryParse(txtMatriculaAnt.Text, out maAnt))
                    {
                        obj.Num_Rgtro_Emprg_Ant = maAnt;
                    }

                    if (Int32.TryParse(txtMatriculaAtu.Text, out maATu))
                    {
                        obj.Num_Rgtro_Emprg_Atu = maATu;
                    }

                    if (Int32.TryParse(txtDigito.Text, out digito))
                    {
                        obj.Num_Digver_Atu = digito;
                    }
                    obj.matricula = user.login;

                    bool ret = objBLL.ValidaCampos(out msg, obj, false);

                    if (ret)
                        LimparCampos();

                    MostraMensagemTelaUpdatePanel(upCisao, msg);
                }
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upCisao, "Atenção \\n\\nRegistro não inserido.\\nMotivo:\\n" + ex.Message);
            }



        }

        protected void btnLimpar2_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }
       
        #endregion
        
        #endregion

        #region Métodos

        private void BuscaEmpresa(TextBox txtB, Label lbl)
        {

            if (!txtB.Text.Equals(""))
            {
                DataTable dt = objBLL.BuscaEmpresa(int.Parse(txtB.Text));
                lbl.Visible = true;
                if (dt.Rows.Count > 0)
                {

                    lbl.Text = dt.Rows[0]["NOME_EMPRESA"].ToString();
                }
                else
                    lbl.Text = "*EMPRESA NÃO CONSTA NO BANCO DE DADOS";
            }
            else
                lbl.Visible = false;

        }

        private void BuscaEmpresaMatriculaAnt(Label lbl)
        {
            CisaoFusaoBLL bll = new CisaoFusaoBLL();

            if (txtMatriculaAnt.Text != "")
            {
                string nome = bll.buscaEmpresaMatricula(Convert.ToDecimal(txtCodEmpAnt.Text), Convert.ToDecimal(txtMatriculaAnt.Text));

                if (nome != "")
                {

                    lbl.Text = nome;
                    lbl.Visible = true;
                }
                else
                {
                    lbl.Text = "*MATRICULA NÃO CONSTA NO BANCO DE DADOS";
                    lbl.Visible = true;
                }
            }
            else
            {
                lbl.Visible = false;

            }
        }

        private void BuscaEmpresaMatriculaAtu(Label lbl)
        {
            CisaoFusaoBLL bll = new CisaoFusaoBLL();

            if (txtMatriculaAtu.Text != "")
            {
                string nome = bll.buscaEmpresaMatricula(Convert.ToDecimal(txtCodEmpAtu.Text), Convert.ToDecimal(txtMatriculaAtu.Text));

                if (nome != "")
                {

                    lbl.Text = nome;
                    lbl.Visible = true;
                }
                else
                {
                    lbl.Text = "*MATRICULA NÃO CONSTA NO BANCO DE DADOS";
                    lbl.Visible = true;
                }
            }
            else
            {
                lbl.Visible = false;

            }
        }

        private void LimparCampos()
        {

            txtBaseAtu.Text = "";
            txtBaseCisao.Text = "";
            txtCodEmpAnt.Text = "";
            txtCodEmpAtu.Text = "";
            txtDigito.Text = "";
            txtMatriculaAnt.Text = "";
            txtMatriculaAtu.Text = "";

            grdPesquisa.Visible = false;
            //txtPesquisa.Text = "";

            lblEmpAnt.Visible = false;
            lblEmpAtu.Visible = false;
            lblMatAnt.Visible = false;
            lblMatAtu.Visible = false;

            divMatricula.Visible = false;
            ckConsulta.Checked = false;

        }

        private void ListaGrid()
        {

            if (ViewState["grdFusao"] != null)
            {
                DataTable dt = (DataTable)ViewState["grdFusao"];
                CarregaGrid("grdFusao", dt, grdFusao);
            }
        }

        private void CarregaGrid(string nameView, DataTable dt, GridView grid)
        {
            ViewState[nameView] = dt;
            grid.DataSource = ViewState[nameView];
            grid.DataBind();
        }

        private string ValidaTela()
        {

            StringBuilder str = new StringBuilder();
            int ano = 0;

            //if (drpMes.SelectedValue == "0")
            //{
            //    str.Append("Selecione um Mês.\\n");
            //}


            if (!txtAno.Text.Equals(""))
            {
                if (drpMes.SelectedValue != "0")
                {
                    if (int.TryParse(txtAno.Text, out ano))
                    {
                        if (ano < 1000)
                        {
                            txtAno.Text = "";
                            str.Append("Digite o ano no formato (YYYY).\\n");
                        }
                    }
                    else
                    {
                        txtAno.Text = "";
                        str.Append("Digite apenas números.\\n");
                    }
                }
                else
                {
                    str.Append("Selecione um Mês.\\n");
                }
            }
            //else
            //{

            //    str.Append("Digite o Ano.\\n");

            //}

            return str.ToString();

        }
        
        #endregion

       

      


    }
}