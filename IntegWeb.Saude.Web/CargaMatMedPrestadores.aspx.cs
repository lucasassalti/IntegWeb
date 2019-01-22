using System;
using System.Collections.Generic;
using IntegWeb.Saude.Aplicacao.BLL.Faturamento;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Framework;
using System.Web.UI.WebControls;
using System.Threading;
using System.ComponentModel;



namespace IntegWeb.Saude.Web
{
    public partial class CargaMatMedPrestadores : BasePage
    {

       CargaMatMedPrestadoresBLL bll = new CargaMatMedPrestadoresBLL();
        

        #region .:eventos:.

       protected void TabContainer_ActiveTabChanged(object sender, EventArgs e)
       {
           divGridCarga.Visible = false;
           ddlConvenenteAba2.SelectedIndex = 0;

           divResultado.Visible = false;
           divGrid.Visible = false;
           divGrid3.Visible = false;

           ddlConvenente.SelectedIndex = 0;
           ddlTbMatMed.Items.Clear();
           ddlTbMatMed.Items.Insert(0, new ListItem("Selecione a tabela Mat/Med", ""));
           txtDatVigencia.Text = "";
           lblTotalPrc.Visible = false;
       }
      
       protected void Page_Load(object sender, EventArgs e)
       {
           //enquanto nao
           if (!IsPostBack)
           {
               ddlConvenente.DataSource = bll.GetConvenente();
               ddlConvenente.DataValueField = "COD_CONVENENTE";
               ddlConvenente.DataTextField = "NOM_CONVENENTE";
               ddlConvenente.DataBind();
               ddlConvenente.Items.Insert(0, new ListItem("Selecione o prestador", ""));

               ddlConvenenteAba2.DataSource = bll.GetConvenente();
               ddlConvenenteAba2.DataValueField = "COD_CONVENENTE";
               ddlConvenenteAba2.DataTextField = "NOM_CONVENENTE";
               ddlConvenenteAba2.DataBind();
               ddlConvenenteAba2.Items.Insert(0, new ListItem("Selecione o prestador", ""));

               ddlTbMatMed.Items.Insert(0, new ListItem("Selecione a tabela Mat/Med", ""));
               ddlTipoCarga.Items.Insert(0, new ListItem("Selecione o tipo de carga", ""));

               lblTotalPrc.Visible = false;
               



           }

           

       }
       
       #endregion


        #region .:ABA1:.

        protected void ddlConvenente_SelectedIndexChanged(object sender, EventArgs e)
        {


            ddlTbMatMed.DataSource = bll.GetTabMatMed(Convert.ToDecimal(ddlConvenente.SelectedValue));
            ddlTbMatMed.DataTextField = "DES_TAB_RECURSO";
            ddlTbMatMed.DataValueField = "COD_TAB_MAT_MED";
            ddlTbMatMed.DataBind();
            ddlTbMatMed.Items.Insert(0, new ListItem("Selecione a tabela Mat/Med", ""));

        }
        
        protected void btnCarregar_Click(object sender, EventArgs e)
        {
             bll.DeleteMatMed();

            //verifica se foi feito upload do arquivo 
            if (fileExcel.HasFile)
            {
                //verifica o formato do arquivo excel
                if (fileExcel.PostedFile.ContentType.Equals("application/vnd.ms-excel") //formato superior ao 2003  ou
                 || fileExcel.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))//formato 2012
                {
                    string caminho = "";
                    string erroEtapa = ""; //caso de erro;
                    int totalCad = 0;
                    int totalAtu = 0;
                    int reCob = 0;
                    int cont = 0;
                    int qtdEsp = 0;

                    //1º
                    try
                    {
                       
                        erroEtapa = "Erro na Importação : ";

                        //traz o nome e a extensão do arquivo
                        string nomeArquivo = Path.GetFileName(fileExcel.FileName).ToString();

                        //separa o nome e o tipo do arquivo em um array
                        string[] arquivoTipo = nomeArquivo.Split('.');

                        //converte um caminho virtual em um caminho fisico - Server.MapPath só aceita caminho virtual
                        string caminhoFisico = Server.MapPath(@"UploadFile\");

                        // caminhoFisico + nome do arquivo + data hora atual + tipo do arquivo
                        caminho = caminhoFisico + arquivoTipo[0] + "-" + DateTime.Now.ToFileTime() + "." + arquivoTipo[1];

                        //salva o arquivo no caminho passado
                        fileExcel.SaveAs(caminho);
                       

                        // Lê e converte o excel para um datatable 
                        DataTable dt = ReadExcelFile(caminho, 1);


                        //salva os dados na tabela temporária
                        bll.ImportaDadosTemp(dt);


                        lblTotalPrc.Text = "Quantidade total de mat/med na planilha: " + dt.Rows.Count;


                        erroEtapa = "Erro na etapa - verificação de procedimentos cadastrados : ";

                        //conta a qtde de procedimentos que ja estão cadastrados na base
                        cont = verificarQtdeProcCadastrados();

                        //salvar procedimentos no datable para depois salvar no banco de dados

                        DataTable dtProcCad = ProcCadastrados();

                        erroEtapa = "Erro na etapa - cadastrar procedimentos  : ";

                        bll.CadastrarCarga(Convert.ToDecimal(ddlConvenente.SelectedValue), out totalCad, out totalAtu, out qtdEsp, out reCob, Convert.ToDecimal(ddlTbMatMed.SelectedValue.ToString()), DateTime.Parse(txtDatVigencia.Text.ToString()), Convert.ToDecimal(ddlTipoCarga.SelectedValue));


                        lblTotalCad.Text = Convert.ToString(totalCad);
                        lblTotalAtu.Text = Convert.ToString(totalAtu);
                        lblReCob.Text = Convert.ToString(reCob);
                        lblEsp.Text = Convert.ToString(qtdEsp);


                        erroEtapa = "Erro na etapa - registrar procedimentos que já foram cadastrados  : ";

                        //salva os procedimentos que já estão cadastrados com a vigência passada como parametro
                        SalvarProcCadastrados(Convert.ToDecimal(ddlConvenente.SelectedValue), Convert.ToDecimal(ddlTbMatMed.SelectedValue), Convert.ToDateTime(txtDatVigencia.Text), dtProcCad);


                        erroEtapa = "Erro na etapa - salvar carga realizada  : ";

                        //salva na tabela de log;
                        CargaRealizada(Convert.ToDecimal(ddlConvenente.SelectedValue), Convert.ToDecimal(ddlTbMatMed.SelectedValue), Convert.ToDateTime(txtDatVigencia.Text), cont);


                        bll.Savechanges();

                        gridViewProc.DataSource = bll.CarregarGridProc(bll.getMaxCountCarga());
                        gridViewProc.DataBind();


                        lblNAtu.Text = bll.GetProcNaoIncluido(bll.getMaxCountCarga()).ToString();
                        divResultado.Visible = true;
                        lblTotalPrc.Visible = true;
                        divGrid.Visible = true;
                      

                    }
                    catch (Exception ex)
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, erroEtapa + ex.Message);

                    }                  
                    finally
                    {
                        fileExcel.FileContent.Dispose();
                        fileExcel.FileContent.Flush();
                        fileExcel.PostedFile.InputStream.Flush();
                        fileExcel.PostedFile.InputStream.Close();
                        File.Delete(caminho);
                        bll.DeleteMatMed();

                    }
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Atenção! Carregue apenas arquivos com extensão .xlsx");
                }
            }

            else
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Atenção! Selecione um Arquivo para continuar");
            }
        }

        protected void gridViewProc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //carregar grid
            gridViewProc.DataSource = bll.CarregarGridProc(bll.getMaxCountCarga());
            gridViewProc.PageIndex = e.NewPageIndex;
            gridViewProc.DataBind();
        }

        #region .: Métodos:.
       
        public void CargaRealizada(decimal codConv, decimal codTabRec, DateTime datVig, int procCadastrados)
        {

            if (codConv == 399020)
            {

                SAU_TBL_CARGA_REALIZADA log = new SAU_TBL_CARGA_REALIZADA();
                var usuario = (ConectaAD)Session["objUser"];
                log.COD_CARGA = bll.getMaxCountCarga() + 1;
                log.COD_CONVENENTE = Convert.ToDecimal(ddlConvenente.SelectedValue);
                log.COD_TAB_MAT_MED = Convert.ToDecimal(ddlTbMatMed.SelectedValue);
                log.DAT_EXEC = DateTime.Now;
                log.DAT_VIG = Convert.ToDateTime(txtDatVigencia.Text);
                log.NM_USU = usuario.nome;
                log.HOST_USU = System.Environment.MachineName;
                log.NM_ARQ_IMPORT = fileExcel.FileName;
                log.TOTAL_INCLUIDO = Convert.ToInt32(lblTotalAtu.Text) + Convert.ToInt32(lblTotalCad.Text);
                log.TOTAL_NAO_INCLUIDO = bll.VerificaProcNaoCadastradosAlbert().Count() + procCadastrados;

                bll.InserirCargaRealizada(Convert.ToInt32(lblTotalAtu.Text) + Convert.ToInt32(lblTotalCad.Text), log);
            }
            else if (codConv == 135329)
            {
                SAU_TBL_CARGA_REALIZADA log = new SAU_TBL_CARGA_REALIZADA();
                var usuario = (ConectaAD)Session["objUser"];
                log.COD_CARGA = bll.getMaxCountCarga() + 1;
                log.COD_CONVENENTE = Convert.ToDecimal(ddlConvenente.SelectedValue);
                log.COD_TAB_MAT_MED = Convert.ToDecimal(ddlTbMatMed.SelectedValue);
                log.DAT_EXEC = DateTime.Now;
                log.DAT_VIG = Convert.ToDateTime(txtDatVigencia.Text);
                log.NM_USU = usuario.nome;
                log.HOST_USU = System.Environment.MachineName;
                log.NM_ARQ_IMPORT = fileExcel.FileName;
                log.TOTAL_INCLUIDO = Convert.ToInt32(lblTotalAtu.Text) + Convert.ToInt32(lblTotalCad.Text);

                int resul = bll.VerificaProcNaoCadastradosIncor().Rows.Count;
                int result = bll.VerificaProcCadastradosIncor(Convert.ToInt16(codTabRec), datVig).Rows.Count;

                log.TOTAL_NAO_INCLUIDO = bll.VerificaProcNaoCadastradosIncor().Rows.Count + procCadastrados;

                bll.InserirCargaRealizada(Convert.ToInt32(lblTotalAtu.Text) + Convert.ToInt32(lblTotalCad.Text), log);
            }


        }

        public void SalvarProcCadastrados(decimal codConv, decimal codTabRec, DateTime datVig, DataTable dt)
        {
            DataTable dataProc = new DataTable();
            dataProc.Columns.Add("cod_carga",typeof(decimal));
            dataProc.Columns.Add("cod_procedimento",typeof(string));
            dataProc.Columns.Add("descricao",typeof(string));
            dataProc.Columns.Add("preco",typeof(decimal));
            dataProc.Columns.Add("obs_erro",typeof(string));
           
            try
            {

                if (codConv == 399020)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow dr2 = dataProc.NewRow();

                        dr2["cod_carga"] = bll.getMaxCountCarga() + 1;
                        dr2["cod_procedimento"] = dr["COD_PROCEDIMENTO"].ToString();
                        dr2["descricao"]  = dr["DESCRICAO"].ToString();
                        dr2["preco"] = Convert.ToDecimal(dr["PRECO"]);
                        dr2["obs_erro"] = " Mat/Med Já consta cadastrado na vigência " + datVig.ToString("dd/MM/yyyy");

                        //bll.InserirCargaProcedimento(tab);
                        dataProc.Rows.Add(dr2);

                    }
                }
                else if (codConv == 135329)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow dr2 = dataProc.NewRow();

                        dr2["cod_carga"] = bll.getMaxCountCarga() + 1;
                        dr2["cod_procedimento"] = dr["COD_PROCEDIMENTO"].ToString();
                        dr2["descricao"] = dr["DESCRICAO"].ToString();
                        dr2["preco"] = Convert.ToDecimal(dr["PRECO"]);
                        dr2["obs_erro"] = " Mat/Med Já consta cadastrado na vigência " + datVig.ToString("dd/MM/yyyy");

                        
                        dataProc.Rows.Add(dr2);
                    }

                }

                bll.InserirCargaProcedimento(dataProc);

            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel,  ex.Message);
            }

        }

        public int verificarQtdeProcCadastrados()
        {
            if (Convert.ToDecimal(ddlConvenente.SelectedValue) == 399020)
            {
                return bll.VerificaProcCadastradosAlbert(Convert.ToDecimal(ddlTbMatMed.SelectedValue), Convert.ToDateTime(txtDatVigencia.Text)).Count();
            }

            else if (Convert.ToDecimal(ddlConvenente.SelectedValue) == 135329)
            {
                return bll.VerificaProcCadastradosIncor(Convert.ToDecimal(ddlTbMatMed.SelectedValue), Convert.ToDateTime(txtDatVigencia.Text)).Rows.Count;
            }

            return 0;
        }

        public DataTable ConverterListParaDataTable<T>(IList<T> lista)
        {
            PropertyDescriptorCollection propriedades = TypeDescriptor.GetProperties(typeof(T));

            DataTable dt = new DataTable();

            foreach (PropertyDescriptor prop in propriedades)
            {
                dt.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in lista)
            {
                DataRow linha = dt.NewRow();
                foreach (PropertyDescriptor prop in propriedades)
                {
                    linha[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    
                }

                dt.Rows.Add(linha);
            }

            return dt;
        }

        public DataTable ProcCadastrados() 
        {
            DataTable dtProcCadastrados = new DataTable(); ;

            if (Convert.ToDecimal(ddlConvenente.SelectedValue) == 399020)
            {
                 dtProcCadastrados = ConverterListParaDataTable(bll.VerificaProcCadastradosAlbert(Convert.ToDecimal(ddlTbMatMed.SelectedValue), Convert.ToDateTime(txtDatVigencia.Text)));
            }
            else if (Convert.ToDecimal(ddlConvenente.SelectedValue) == 135329)
            {
                 dtProcCadastrados = bll.VerificaProcCadastradosIncor(Convert.ToDecimal(ddlTbMatMed.SelectedValue), Convert.ToDateTime(txtDatVigencia.Text));
            }

            return dtProcCadastrados;
        }

        public DataTable ProcNaoCadastrados()
        {
            DataTable dtProcNaoCadastrados = new DataTable(); 

            if (Convert.ToDecimal(ddlConvenente.SelectedValue) == 399020)
            {
                dtProcNaoCadastrados = ConverterListParaDataTable(bll.VerificaProcNaoCadastradosAlbert());
            }
            else if (Convert.ToDecimal(ddlConvenente.SelectedValue) == 135329)
            {
                dtProcNaoCadastrados = bll.VerificaProcNaoCadastradosIncor();
            }

            return dtProcNaoCadastrados;
        }

        #endregion

        #endregion


        #region .:ABA2:.
        protected void ddlConvenenteAba2_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridCarga.DataSource = bll.CarregarGridCarga(Convert.ToDecimal(ddlConvenenteAba2.SelectedValue));
            gridCarga.DataBind();

            divGridCarga.Visible = true;
            divGrid3.Visible = false;
        }

        protected void gridViewProcAba2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            decimal codCarga = Convert.ToDecimal(gridCarga.Rows[gridCarga.SelectedIndex].Cells[0].Text);

            gridViewProcAba2.PageIndex = e.NewPageIndex;
            gridViewProcAba2.DataSource = bll.CarregarGridProc(codCarga);
            gridViewProcAba2.DataBind();
        }

        protected void gridCarga_SelectedIndexChanged(object sender, EventArgs e)
        {

            decimal codCarga = Convert.ToDecimal(gridCarga.Rows[gridCarga.SelectedIndex].Cells[0].Text);

            gridViewProcAba2.DataSource = bll.CarregarGridProc(codCarga);
            gridViewProcAba2.DataBind();

            divGrid3.Visible = true;
        }

        protected void gridCarga_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridCarga.PageIndex = e.NewPageIndex;
            gridCarga.DataSource = bll.CarregarGridCarga(Convert.ToDecimal(ddlConvenenteAba2.SelectedValue));
            gridCarga.DataBind();
        }
    
        #endregion
       



  

      
       

     
       
    
    }

    }

    
