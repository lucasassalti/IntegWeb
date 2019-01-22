using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Entidades.Framework;
using IntegWeb.Previdencia.Aplicacao.BLL.CargaProtheus;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System.Data;

namespace IntegWeb.Previdencia.Web
{
    public partial class ControleMedctr : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = (ConectaAD)Session["objUser"];

            if (!IsPostBack)
            {
                ControleMedctrBLL objBLL = new ControleMedctrBLL();

                ddlTipoMed.DataSource = objBLL.GetTipoMedDdl();
                ddlTipoMed.DataTextField = "dcr_carga_tipo";
                ddlTipoMed.DataValueField = "cod_carga_tipo";
                ddlTipoMed.DataBind();
                ddlTipoMed.Items.Insert(0, new ListItem("Todos", ""));

                CarregaGridPrincipal();

            }

        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {

            ControleMedctrBLL objBLL = new ControleMedctrBLL();
            string dtProt = "";

            if (!String.IsNullOrEmpty(txtDtPagamento.Text))
            {

                dtProt = NormalizaDataMedctr(Convert.ToDateTime(txtDtPagamento.Text));
            }


            DataTable dtPesq = objBLL.GetPesqLotes(Util.String2Int32(txtNumeroDoLote.Text), dtProt, Util.String2Int32(ddlTipoMed.SelectedValue));

            int i = 0;

            foreach (DataRow r in dtPesq.Rows)
            {
                dtPesq.Rows[i][3] = DateTime.ParseExact(r[3].ToString(),
                                    "yyyyMMdd",
                                     System.Globalization.CultureInfo.InvariantCulture);

                i++;
            }

            grdPrincipal.DataSource = dtPesq;

            grdPrincipal.DataBind();



        }

        protected void grdPrincipal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPrincipal.PageIndex = e.NewPageIndex;
            CarregaGridPrincipal();

        }

        public void CarregaGridPrincipal()
        {
            ControleMedctrBLL objBLL = new ControleMedctrBLL();


            DataTable dt = objBLL.GetGridGeral();

            int i = 0;

            foreach (DataRow r in dt.Rows)
            {
                DateTime date;
                bool result = DateTime.TryParseExact(r[3].ToString(),
                                                    "yyyyMMdd",
                                                     System.Globalization.CultureInfo.InvariantCulture,
                                                        System.Globalization.DateTimeStyles.None,
                                                        out date);



                if (result)
                {
                    //dt.Rows[i][3] = DateTime.ParseExact(r[3].ToString(),
                    //                               "yyyyMMdd",
                    //                                System.Globalization.CultureInfo.InvariantCulture);

                    dt.Rows[i][3] = date;
                }

               



                i++;

            }

            grdPrincipal.DataSource = dt;

            grdPrincipal.DataBind();
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            CarregaGridPrincipal();
            txtDtPagamento.Text = "";
            txtNumeroDoLote.Text = "";

            ControleMedctrBLL objBLL = new ControleMedctrBLL();

            ddlTipoMed.DataSource = objBLL.GetTipoMedDdl();
            ddlTipoMed.DataTextField = "dcr_carga_tipo";
            ddlTipoMed.DataValueField = "cod_carga_tipo";
            ddlTipoMed.DataBind();
            ddlTipoMed.Items.Insert(0, new ListItem("Todos", ""));

        }

        protected void grdPrincipal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var user = (ConectaAD)Session["objUser"];

            if (e.CommandName != "Page")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int indexLinha = gvr.RowIndex;
                string numeroLote = ((Label)grdPrincipal.Rows[indexLinha].FindControl("lblNumeroLote")).Text;


                if (e.CommandName == "Gravar")
                {
                    lblPopNumLote.Text = numeroLote;
                    txtPopStatus.Text = ((Label)grdPrincipal.Rows[indexLinha].FindControl("lblStatus")).Text;
                    txtDtPagtoNova.Text = Convert.ToDateTime(((Label)grdPrincipal.Rows[indexLinha].FindControl("lblDtPagto")).Text).ToShortDateString();
                    lblStatusAnt.Text = txtPopStatus.Text;
                    lblDtAnt.Text = txtDtPagtoNova.Text;
                    lblLog.Text = user.login;
                    mpe.Show();
                }
                else if (e.CommandName == "Excluir")
                {
                    ControleMedctrBLL objBLL = new ControleMedctrBLL();
                    ControleMedctrBLL obj = new ControleMedctrBLL();

                    bool ret = objBLL.ExcluiLote(Convert.ToInt32(numeroLote));

                    if (ret)
                    {
                        DateTime dat = Convert.ToDateTime(((Label)grdPrincipal.Rows[indexLinha].FindControl("lblDtPagto")).Text);

                        if (obj.insereHistorico(Convert.ToInt32(numeroLote), "Excluir", Convert.ToInt32(((Label)grdPrincipal.Rows[indexLinha].FindControl("lblStatus")).Text), Convert.ToInt32(((Label)grdPrincipal.Rows[indexLinha].FindControl("lblStatus")).Text), dat, dat, user.login.ToString()))
                        {
                            MostraMensagemTelaUpdatePanel(upUpdatePanel, "Lote excluido com sucesso");
                        }
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro ao excluir o lote, favor entrar em contato com o administrador do sistema.");
                    }

                    CarregaGridPrincipal();
                }
                else if (e.CommandName == "Duplicar")
                {
                    ControleMedctrBLL objBLL = new ControleMedctrBLL();
                    ControleMedctrBLL obj = new ControleMedctrBLL();

                    int lote_novo = objBLL.retornaMaxLote();

                    bool ret = objBLL.DuplicaLote(Convert.ToInt32(numeroLote), NormalizaDataMedctr(Convert.ToDateTime(((Label)grdPrincipal.Rows[indexLinha].FindControl("lblDtPagto")).Text)), lote_novo);
                    if (ret)
                    {
                        DataTable dt = objBLL.GetInfoLotePreTbl(Convert.ToInt32(numeroLote));


                        PRE_TBL_CARGA_PROTHEUS newobj = new PRE_TBL_CARGA_PROTHEUS();
                        newobj.COD_CARGA_PROTHEUS = Convert.ToInt32(dt.Rows[0][1].ToString());
                        newobj.DTH_PAGAMENTO = Convert.ToDateTime(dt.Rows[0][2].ToString());
                        newobj.COD_CARGA_TIPO = Convert.ToInt32(dt.Rows[0][1].ToString());
                        newobj.COD_CARGA_STATUS = 1;
                        newobj.DTH_EXECUCAO = null;
                        newobj.IND_EXEC_IMEDIATA = "N";
                        newobj.DCR_PARAMETROS = dt.Rows[0][3].ToString();
                        newobj.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";
                        newobj.DTH_INCLUSAO = DateTime.Now;
                        newobj.DTH_INCIO_PROCESSO = null;
                        newobj.DTH_FIM_PROCESSO = null;
                        newobj.DTH_DOCUMENTO_INICIAL = null;
                        newobj.DTH_DOCUMENTO_FINAL = null;
                        newobj.DTH_COMPLEMENTAR = null;
                        newobj.DTH_SUPLEMENTAR = null;
                        newobj.COD_ASSOCIADO = null;
                        newobj.COD_LOTE = null;
                        newobj.NUM_LOTE = lote_novo;

                        objBLL.SaveFila(newobj);

                        if (obj.insereHistorico(lote_novo, "Duplicar", Convert.ToInt32(((Label)grdPrincipal.Rows[indexLinha].FindControl("lblStatus")).Text), Convert.ToInt32(((Label)grdPrincipal.Rows[indexLinha].FindControl("lblStatus")).Text), Convert.ToDateTime(dt.Rows[0][2].ToString()), Convert.ToDateTime(dt.Rows[0][2].ToString()), user.login.ToString()))
                        {
                            MostraMensagemTelaUpdatePanel(upUpdatePanel, "Lote duplicado com sucesso");
                        }



                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro ao duplicar o lote, favor entrar em contato com o administrador do sistema.");
                    }

                    CarregaGridPrincipal();
                }
            }
        }


        protected void btnConfirmaAlterData_Click(object sender, EventArgs e)
        {
            ControleMedctrBLL objBLL = new ControleMedctrBLL();

            if (!String.IsNullOrEmpty(txtPopStatus.Text) && !String.IsNullOrEmpty(txtDtPagtoNova.Text))
            {
                objBLL.AlteraLoteMedctr(NormalizaDataMedctr(Convert.ToDateTime(txtDtPagtoNova.Text)), txtPopStatus.Text, Convert.ToInt32(lblPopNumLote.Text), lblStatusAnt.Text, NormalizaDataMedctr(Convert.ToDateTime(lblDtAnt.Text)));
                //   objBLL.AlteraLoteHistorico(Convert.ToDateTime(txtDtPagtoNova.Text).ToShortDateString(), Convert.ToInt32(lblPopNumLote.Text));
                objBLL.AlteraLoteMedctrLiq(NormalizaDataMedctr(Convert.ToDateTime(txtDtPagtoNova.Text)), Convert.ToInt32(lblPopNumLote.Text), NormalizaDataMedctr(Convert.ToDateTime(lblDtAnt.Text)));
                objBLL.AlteraLoteTmp(NormalizaDataMedctr(Convert.ToDateTime(txtDtPagtoNova.Text)), Convert.ToInt32(lblPopNumLote.Text), NormalizaDataMedctr(Convert.ToDateTime(lblDtAnt.Text)));
                objBLL.AlteraLoteDet(NormalizaDataMedctr(Convert.ToDateTime(txtDtPagtoNova.Text)), Convert.ToInt32(lblPopNumLote.Text), NormalizaDataMedctr(Convert.ToDateTime(lblDtAnt.Text)));

            }


            if (objBLL.insereHistorico(Convert.ToInt32(lblPopNumLote.Text), "Alteração Data/Status", Convert.ToInt32(lblStatusAnt.Text), Convert.ToInt32(txtPopStatus.Text), Convert.ToDateTime(lblDtAnt.Text), Convert.ToDateTime(txtDtPagtoNova.Text), lblLog.Text))
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Lote alterado com sucesso");
            }



            CarregaGridPrincipal();

        }

        protected string NormalizaDataMedctr(DateTime data)
        {
            string dtProt = "";
            string mes = "";
            string dia = "";

            if (data.Month.ToString().Length < 2)
            {
                mes = "0" + data.Month.ToString();
            }
            else
            {
                mes = data.Month.ToString();
            }

            if (data.Day.ToString().Length < 2)
            {
                dia = "0" + data.Day.ToString();
            }
            else
            {
                dia = data.Day.ToString();
            }

            dtProt = data.Year.ToString() + mes + dia;

            return dtProt;
        }

        protected DateTime NormalizaData(string dt_med)
        {
            DateTime data = new DateTime();

            int ano = Convert.ToInt32(dt_med.Substring(0, 4));

            data.AddYears(Convert.ToInt32(dt_med.Substring(0, 4)));

            data.AddMonths(Convert.ToInt32(dt_med.Substring(4, 2)));

            data.AddDays(Convert.ToInt32(dt_med.Substring(6, 2)));

            return data;

        }


    }
}