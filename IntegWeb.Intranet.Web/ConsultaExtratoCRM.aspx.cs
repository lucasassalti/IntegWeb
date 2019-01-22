//using MySql.Data.MySqlClient;
//using IntegWeb.Intranet.Web.WS_CRM_FuncaoExtra;
using Intranet.Aplicacao.BLL;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
//using System.ServiceModel;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace IntegWeb.Intranet.Web
{
    public partial class ConsultaExtratoCRM : System.Web.UI.Page
    {

        public string Servidor = "10.190.35.143"; //doctst

        protected Filtro_Pesquisa ler_variaveis_ambiente()
        {
            Filtro_Pesquisa filtro = new Filtro_Pesquisa();
            filtro.TipoDocumento = Convert.ToInt16(Request.QueryString["BdTypeName"]);
            filtro.CodigoEmpresa = Request.QueryString["CP01"];
            filtro.Registro = Request.QueryString["CP02"];
            filtro.Representante = Request.QueryString["CP03"];
            filtro.PesquisaAnoInicio = Request.QueryString["CP04"];
            filtro.PesquisaAnoFim = Request.QueryString["CP05"];
            filtro.PesquisaMesInicio = Request.QueryString["CP06"];
            filtro.PesquisaMesFim = Request.QueryString["CP07"];
            filtro.ParticipanteNome = Request.QueryString["CP08"];
            filtro.CPF = Request.QueryString["CP09"];
            filtro.ParticipanteEmail = Request.QueryString["CP10"];
            filtro.NrChamado = Request.QueryString["CP11"];
            filtro.NrManifestacao = Request.QueryString["CP12"];
            filtro.Sexo = Request.QueryString["CP13"];
            filtro.cnpj = Request.QueryString["CP14"];
            //filtro.nom_convenente = Request.QueryString["CP15"];
            filtro.contrato = Request.QueryString["CP16"];

            switch (filtro.TipoDocumento)
            {
                case 1:
                    filtro.IdIdocs = "tp_pagsupl";
                    break;
                case 2:
                    filtro.IdIdocs = "tp_contrib";
                    break;
                case 3:
                    filtro.IdIdocs = "tp_irrfapo";
                    break;
                case 4:
                    filtro.IdIdocs = "tp_cobvinc";
                    break;
                case 5:
                    filtro.IdIdocs = "tp_cobrsep";
                    break;
                case 6:
                    filtro.IdIdocs = "tp_revisao";
                    break;
                case 8:
                    filtro.IdIdocs = "tp_cobsaud";
                    break;
                case 9:
                    filtro.IdIdocs = "tp_utilamh";
                    break;
                case 10:
                    filtro.IdIdocs = "tp_utilpes";
                    break;
                case 11:
                    filtro.IdIdocs = "tp_credree";
                    break;
                case 12:
                    filtro.IdIdocs = "tp_utilanu";
                    break;
                case 13:
                    filtro.IdIdocs = "tp_cejadmn";
                    break;
                case 14:
                    filtro.IdIdocs = "tp_irrfcre";
                    break;
                case 15:
                    filtro.IdIdocs = "tp_terquit";
                    break;
                case 16:
                    filtro.IdIdocs = "tp_pagcompc";
                    break;
                case 17:
                    filtro.IdIdocs = "tp_cejsaud";
                    break;
                case 18:
                    filtro.IdIdocs = "tp_credpcc";
                    break;
                default:
                    break;
            }
            return filtro;
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            // Ler as variaveis            
            Filtro_Pesquisa filtro = ler_variaveis_ambiente();

            //Regisro para teste 
            //if (Consulta_GED_CRM.Properties.Settings.Default.Ambiente != "Producao" && filtro.TipoDocumento == 0)
            ////{
            //// NAO ESQUECER DE COMENTAR ESSA PARTE DAQUI
            //    filtro.TipoDocumento = 9;
            //    filtro.CodigoEmpresa = "088";
            //    filtro.Registro = "0000200007";
            //    filtro.Representante = "0";
            //    filtro.ParticipanteNome = "FULVIO CORRALES DE ANDRADE";
            //    filtro.PesquisaAnoInicio = "2000";
            //    filtro.PesquisaAnoFim = "2020";
            //    filtro.PesquisaMesInicio = "01";
            //    filtro.PesquisaMesFim = "12";
            //    filtro.ParticipanteEmail = "gustavo.geraldo@funcesp.com.br";
            //    filtro.NrChamado = "921";
            //    filtro.NrManifestacao = "701";

            ////ATÉ AQUI
            ////}

            // Apenas informar e-mail a ser enviado
            if (!IsPostBack)
            {
                txtEmailDestinatario.Text = filtro.ParticipanteEmail;
            }


            //if (ConfigurationManager.AppSettings["Config"] == "P")
            //{
            //    Servidor = "10.190.35.57"; // docprod
            //}

            if (ConfigurationManager.AppSettings["Config"] == "P" || ConfigurationManager.AppSettings["Config"] == "T")
            {
                Servidor = "10.190.35.57"; // docprod
            }

            // Apenas informar  sobre ambiente de Homologacao
            //if (Consulta_GED_CRM.Properties.Settings.Default.Ambiente != "Producao")
            //{
            //    Label4.Text = "Existem poucos documentos no ambiente de Homologacao - Para testes favor utilizar a empresa 88";
            //}

            // Consultar documentos
            List<DocumentoPDF> documentos = Consultar_Documentos.obter_lista_arquivos_PDF(filtro, Servidor);
            if (documentos.Count == 0)
            {
                Response.Write("Nenhum arquivo encontrado");
                txtEmailDestinatario.Visible = false;
                btnEnviarEmail.Visible = false;
            }
            else
            {
                gerar_pagina_html_com_resultado(documentos, filtro.IdIdocs);
                txtEmailDestinatario.Visible = true;
                btnEnviarEmail.Visible = true;
            }
        }

        protected void gerar_pagina_html_com_resultado(List<DocumentoPDF> documentos, string IdIdocs)
        {
            // Criar layout com os documentos encontrados
            int altura = 50;
            int contador = 0;

            // Caso o array com documento seja diferente de nulo
            if (documentos != null)
            {
                // zerar variavies
                int contador_tipo = 0;
                int contador_ano = 0;
                String tipoArquivo = "";
                String Ano = "";

                // Para cada documento do array
                foreach (DocumentoPDF d in documentos)
                {
                    // Apenas informar o participantes consultado
                    // lblParticipante.Text = "Consultando Empresa: " + d.Empresa + " Registro: " + d.Matricula + " Nome: " + d.Nome;


                    // Para cada documento do array, quando mudar o tipo de documento 
                    if (tipoArquivo != d.TipoDocumento)
                    {
                        // Para cada documento do array, quando mudar o tipo de documento 
                        // Criar checkbox com o novo tipo de documento  (este checkbox irá permitir selecionar todos os documentos)                        
                        contador_tipo = 0;
                        tipoArquivo = d.TipoDocumento;
                        altura = altura + 10;
                        CheckBox lbl2 = new CheckBox();
                        lbl2.ID = "chk_" + d.TipoDocumento + "_todos";
                        lbl2.Text = "";
                        lbl2.Style.Add("Position", "Absolute");
                        lbl2.Style.Add("Left", "5px");
                        lbl2.Style.Add("Top", altura.ToString() + "px");
                        lbl2.Attributes.Add("name", "chk_" + d.TipoDocumento + "_todos");
                        lbl2.Attributes.Add("onclick", "checkAll(this.checked,\'" + "chk_" + d.TipoDocumento + "')");
                        divDocumentos.Controls.Add(lbl2);

                        // Criar label do checkbox acima
                        Label l_lbl2 = new Label();
                        l_lbl2.Text = d.TipoDocumento;
                        l_lbl2.Style.Add("Position", "Absolute");
                        l_lbl2.Style.Add("Left", "25px");
                        l_lbl2.Style.Add("Top", altura.ToString() + "px");
                        divDocumentos.Controls.Add(l_lbl2);
                    }

                    // Para cada documento do array, quando mudar o ano do documento
                    if (Ano != d.Ano)
                    {
                        // Para cada documento do array, quando mudar o tipo de documento 
                        // Criar checkbox com o novo Ano  (este checkbox irá permitir selecionar todos os documentos naquele ano)                        
                        contador_ano = 0;
                        Ano = d.Ano;
                        altura = altura + 20;
                        CheckBox lblAno = new CheckBox();
                        lblAno.ID = "chk_" + d.TipoDocumento + "_" + d.Ano + "_todos";
                        lblAno.Text = "";
                        lblAno.Style.Add("Position", "Absolute");
                        lblAno.Style.Add("Left", "25px");
                        lblAno.Style.Add("Top", altura.ToString() + "px");
                        lblAno.Attributes.Add("name", "chk_" + d.TipoDocumento + "_todos");
                        lblAno.Attributes.Add("onclick", "checkAll(this.checked,\'" + "chk_" + d.TipoDocumento + "_" + d.Ano + "')");
                        divDocumentos.Controls.Add(lblAno);

                        // Criar label do checkbox acima
                        Label l_lblAno = new Label();
                        l_lblAno.Text = d.Ano;
                        l_lblAno.Style.Add("Position", "Absolute");
                        l_lblAno.Style.Add("Left", "45px");
                        l_lblAno.Style.Add("Top", altura.ToString() + "px");
                        divDocumentos.Controls.Add(l_lblAno);
                    }

                    // Criar Checkbox para selecionar o documento (selecionar somente o documento)                    
                    contador_tipo++;
                    altura = altura + 20;
                    CheckBox lbl1 = new CheckBox();
                    lbl1.ID = "chk_" + d.TipoDocumento + "_" + d.Ano + "_" + contador_tipo + "__" + d.DocumentoArquivo;
                    lbl1.Text = "";
                    lbl1.Style.Add("Position", "Absolute");
                    lbl1.Style.Add("Left", "45px");
                    lbl1.Style.Add("Top", altura.ToString() + "px");
                    lbl1.Attributes.Add("name", "chk_" + d.TipoDocumento + "_" + contador_tipo);
                    divDocumentos.Controls.Add(lbl1);

                    // Criar label do checkbox acima
                    String strDescricao = "";
                    strDescricao = d.Ano == null ? null : " Ano: " + d.Ano;
                    strDescricao += (d.Mes == null || d.Mes == "") ? null : " Mês:  " + d.Mes;
                    if (d.Plano != "")
                    {
                        strDescricao += " Plano:  " + d.Plano;
                    }
                    strDescricao += (d.Nome == null || d.Nome == "") ? null : " Nome:  " + d.Nome;
                    Label l_lbl1 = new Label();
                    l_lbl1.Text = strDescricao;
                    // l_lbl1.Style.Add("background", "#ccc");
                    l_lbl1.Style.Add("Position", "Absolute");
                    l_lbl1.Style.Add("Left", "65px");
                    l_lbl1.Style.Add("Top", altura.ToString() + "px");
                    divDocumentos.Controls.Add(l_lbl1);

                    // Criar LinkButton para visualizar checkbox do checkbox acima
                    LinkButton lkbVisualizar = new LinkButton();
                    lkbVisualizar.ID = "lkbVisualizar" + d.TipoDocumento + "_" + contador_tipo;
                    lkbVisualizar.CssClass = "btnLkPDF";
                    //lkbVisualizar.Style.Add("Position", "Absolute");
                    //lkbVisualizar.Style.Add("Left", "680px");
                    lkbVisualizar.Style.Add("Top", altura.ToString() + "px");
                    lkbVisualizar.Text = "Visualizar ";
                    divDocumentos.Controls.Add(lkbVisualizar);
                    lkbVisualizar.CommandArgument = d.DocumentoArquivo + "@|#" + IdIdocs;
                    lkbVisualizar.Command += new CommandEventHandler(this.onVisualizarDocumento);

                }

            }
        }
        
        protected void onVisualizarDocumento(object sender, EventArgs e)
        {
            // Ler o ID do documento clicado
            String DocumentoEscolhido = ((LinkButton)sender).CommandArgument.Substring(0, ((LinkButton)sender).CommandArgument.IndexOf("@|#"));
            string IdIdocs = ((LinkButton)sender).CommandArgument.Substring(((LinkButton)sender).CommandArgument.IndexOf("@|#") + 3, ((LinkButton)sender).CommandArgument.Length - (((LinkButton)sender).CommandArgument.IndexOf("@|#") + 3));
            // chamar pagina PHP para abrir documento
            //String Servidor = Consulta_GED_CRM.Properties.Settings.Default.Servidor_GED;

            Response.Write("<script type='text/javascript'> window.open('http://" + Servidor + "/ged/idocs_visualizar_doc.php?tipo_doc=" + IdIdocs + "&conta=fcesp&file_doc=" + DocumentoEscolhido + "&name_doc=" + DocumentoEscolhido.Substring(8, DocumentoEscolhido.Length - 8) + "','_blank'); </script>");
        }



        protected void btnEnviarEmail_Click(object sender, EventArgs e)
        {
            // ler os checkbox da pagina que foram selecionados
            List<String> documentos_selecionados = new List<String>();

            // Percorrer todos os componente da pagina
            foreach (String cc in Request.Form.Keys)
            {
                // se o componente for checkbox 
                if (cc.IndexOf("chk_") > -1 && cc.IndexOf("todos") == -1)
                {
                    // Adicionar a lista de documentos clicados
                    documentos_selecionados.Add(cc.Substring(cc.IndexOf("__") + 2, cc.Length - (cc.IndexOf("__") + 2)));
                }
            
           }


            if (documentos_selecionados.Count > 0)
            {
                Filtro_Pesquisa filtro = ler_variaveis_ambiente();
                filtro.ParticipanteEmail = txtEmailDestinatario.Text;

                ////Regisro para teste 
                //if (Consulta_GED_CRM.Properties.Settings.Default.Ambiente != "Producao" && filtro.TipoDocumento == 0)
                //{
                //    filtro.TipoDocumento = 9;
                //    filtro.CodigoEmpresa = "088";
                //    filtro.Registro = "0000200007";
                //    filtro.Representante = "0";
                //    filtro.ParticipanteNome = "FULVIO CORRALES DE ANDRADE";
                //    filtro.PesquisaAnoInicio = "2000";
                //    filtro.PesquisaAnoFim = "2020";
                //    filtro.PesquisaMesInicio = "01";
                //    filtro.PesquisaMesFim = "12";
                //    filtro.ParticipanteEmail = "gustavo.geraldo@funcesp.com.br";
                //    filtro.NrChamado = "921";
                //    filtro.NrManifestacao = "701";
                //}

                if (String.IsNullOrEmpty(filtro.ParticipanteEmail))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "alert('Campo E-Mail obrigatório.');", true);
                    txtEmailDestinatario.Focus();
                }
                else if (!Util.ValidaEmail(filtro.ParticipanteEmail))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "alert('Campo E-Mail inválido.');", true);
                    txtEmailDestinatario.Focus();
                }
                else
                {
                    Enviar_Email.Executar(filtro, documentos_selecionados, Servidor);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "alert('E-Mail Enviado com sucesso.');", true);
                }
            }

            else {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "alert('Favor selecionar um documento para prosseguir.');", true);
            }


        }

    }

    public class Enviar_Email
    {

        public static void Executar(Filtro_Pesquisa filtro, List<String> documentos_selecionados, String Servidor)
        {

            //Criar Mensagem de email
            MailMessage mail = criar_email(filtro);

            //Percorrer a lista de documentos a serem enviados
            int contador = 0;
            int NumeroMaximoDeAnexoNoEmail = 4; // Convert.ToInt32(Consulta_GED_CRM.Properties.Settings.Default.Nr_Maximo_de_Anexos);
            string arquivo_fisico = "";
            foreach (String DocumentoPDF in documentos_selecionados)
            {
                adicionar_anexo(mail, DocumentoPDF, filtro.IdIdocs, Servidor);

                // quando o contador chegar ao numero NumeroMaximoDeAnexoNoEmail
                contador++;
                if (contador == NumeroMaximoDeAnexoNoEmail)
                {
                    // disparar e-mail
                    disparar_email(mail, filtro);
                    // Criar novo e-mail
                    mail = criar_email(filtro);
                    //zerar contador
                    contador = 0;
                }
            }

            // se o total de documentos nao atingir o NumeroMaximoDeAnexoNoEmail enviar e-mail mesmo assim 
            if (contador != 0)
            {
                // disparar e-mail
                disparar_email(mail, filtro);

            }

        }


        private static MailMessage criar_email(Filtro_Pesquisa filtro)
        {
            //Criar Mensagem de email
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("atendimento@funcesp.com.br"); //Consulta_GED_CRM.Properties.Settings.Default.Email_Remetente);
            mail.To.Add(filtro.ParticipanteEmail);
            mail.IsBodyHtml = true;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.Subject = gerar_titulo_email(filtro);
            mail.Body = gerar_corpo_email(filtro);
            return mail;
        }


        private static String gerar_corpo_email(Filtro_Pesquisa filtro)
        {

            //StreamReader sr = new StreamReader(Consulta_GED_CRM.Properties.Settings.Default.Pasta_Templates_Corpo_Email + "\\" + filtro.TipoDocumento + "\\Template_Corpo_Email.htm");

            String idocs = HttpContext.Current.Server.MapPath(@"Modelos\Idocs\");

            StreamReader sr = new StreamReader(idocs + filtro.TipoDocumento + @"\Template_Corpo_Email.htm");
            String line = sr.ReadToEnd();

            //cria string para verificar o sexo do participante e incluir o SR. ou Sra.
            string tratamento = "";
            line = line.Replace("TipoDocumento", Consultar_Documentos.converter_nome2(filtro.TipoDocumento));

            string str_periodo_dia = "";



            TimeSpan horarioAtual = DateTime.Now.TimeOfDay;
            TimeSpan periodo_dia = new TimeSpan(12, 0, 0);

            if (horarioAtual < periodo_dia)
                str_periodo_dia = "Bom Dia";
            else
                str_periodo_dia = "Boa Tarde";

            //compara a string e cria e retorna true ou false
            tratamento = (filtro.Sexo.Equals("M") ? "Sr." : "Sra.");
            line = line.Replace("periodo_dia", str_periodo_dia);
            line = line.Replace("Tratamento", tratamento);
            line = line.Replace("NomeParticipante", filtro.ParticipanteNome);
            line = line.Replace("EmpresaParticipante", "");
            line = line.Replace("RegistroParticipante", "");
            line = line.Replace("NumeroRepresentanteParticipante", "");
            line = line.Replace("DiaAtual", DateTime.Now.ToString("dd/mm/yyyy"));
            line = line.Replace("HoraAtual", DateTime.Now.ToString("hh:mi:ss"));
            return line;
        }

        private static String gerar_titulo_email(Filtro_Pesquisa filtro)
        {
            String titulo = "Solicitação de segunda via " + Consultar_Documentos.converter_nome2(filtro.TipoDocumento);
            if (filtro.TipoDocumento == 1)
                titulo = "Solicitação de segunda via " + Consultar_Documentos.converter_nome2(filtro.TipoDocumento);
            if (filtro.TipoDocumento == 2)
                titulo = "Solicitação de segunda via " + Consultar_Documentos.converter_nome2(filtro.TipoDocumento);
            if (filtro.TipoDocumento == 3)
                titulo = "Solicitação de segunda via " + Consultar_Documentos.converter_nome2(filtro.TipoDocumento);
            if (filtro.TipoDocumento == 4)
                titulo = "Solicitação de segunda via " + Consultar_Documentos.converter_nome2(filtro.TipoDocumento);
            if (filtro.TipoDocumento == 5)
                titulo = "Solicitação de segunda via " + Consultar_Documentos.converter_nome2(filtro.TipoDocumento);
            if (filtro.TipoDocumento == 6)
                titulo = "Solicitação de segunda via " + Consultar_Documentos.converter_nome2(filtro.TipoDocumento);
            if (filtro.TipoDocumento == 7)
                titulo = "Solicitação de segunda via " + Consultar_Documentos.converter_nome2(filtro.TipoDocumento);
            if (filtro.TipoDocumento == 8)
                titulo = "Solicitação de segunda via " + Consultar_Documentos.converter_nome2(filtro.TipoDocumento);
            if (filtro.TipoDocumento == 9)
                titulo = "Solicitação de segunda via " + Consultar_Documentos.converter_nome2(filtro.TipoDocumento);
            if (filtro.TipoDocumento == 10)
                titulo = "Solicitação de segunda via " + Consultar_Documentos.converter_nome2(filtro.TipoDocumento);
            if (filtro.TipoDocumento == 11)
                titulo = "Solicitação de segunda via " + Consultar_Documentos.converter_nome2(filtro.TipoDocumento);
            if (filtro.TipoDocumento == 12)
                titulo = "Solicitação de segunda via " + Consultar_Documentos.converter_nome2(filtro.TipoDocumento);
            if (filtro.TipoDocumento == 13)
                titulo = "Solicitação de segunda via " + Consultar_Documentos.converter_nome2(filtro.TipoDocumento);
            if (filtro.TipoDocumento == 14)
                titulo = "Solicitação de segunda via " + Consultar_Documentos.converter_nome2(filtro.TipoDocumento);
            if (filtro.TipoDocumento == 15)
                titulo = "Solicitação de segunda via " + Consultar_Documentos.converter_nome2(filtro.TipoDocumento);
            if (filtro.TipoDocumento == 16)
                titulo = "Solicitação de segunda via " + Consultar_Documentos.converter_nome2(filtro.TipoDocumento);
            if (filtro.TipoDocumento == 17)
                titulo = "Solicitação de segunda via " + Consultar_Documentos.converter_nome2(filtro.TipoDocumento);
            if (filtro.TipoDocumento == 18)
                titulo = "Solicitação de segunda via " + Consultar_Documentos.converter_nome2(filtro.TipoDocumento);
            return titulo;
        }

        protected static void adicionar_anexo(MailMessage mail, String DocumentoPDF, String IdIdocs, String Servidor)
        {
            //String pastaTemporaria = criar_pasta_temporaria("Arquivo");
            MemoryStream Arquivo_Fisico = fazer_download_arquivo(DocumentoPDF, IdIdocs, Servidor);
            DocumentoPDF = DocumentoPDF.Substring(DocumentoPDF.LastIndexOf("/") + 1, DocumentoPDF.Length - DocumentoPDF.LastIndexOf("/") - 1);
            //Adicionar arquivo anexo no e-mail
            mail.Attachments.Add(new Attachment(Arquivo_Fisico, DocumentoPDF));
        }

        protected static void disparar_email(MailMessage mail, Filtro_Pesquisa filtro)
        {
            //Fazer conexao no servidor de e-mail
            //SmtpClient client = new SmtpClient("smtp.funcesp.com.br");
            //SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["Servidor_Email"]);

            // Somente enviar e-mail externo quando for o ambiente de Producao
            //if (Consulta_GED_CRM.Properties.Settings.Default.Email_Limitar_Envio_Interno != "S")
            //{
            // Passar dados do usuario, passo necessario para envio de email externo ao ambiente
            //String usuario = Consulta_GED_CRM.Properties.Settings.Default.Email_Usuario;
            //String senha = Consulta_GED_CRM.Properties.Settings.Default.Email_Usuario_Senha;
            //String usuario = "atendimento";
            //String senha = "WebCrm#14";
            //NetworkCredential basicCredential = new NetworkCredential(usuario, senha);
            //client.UseDefaultCredentials = true;
            //client.Credentials = basicCredential;
            //}

            // Enviar e-mail
            //client.Send(mail);

            SmtpClient client = new Email().EnviaEmailMensagem(mail);

            //salvar e-mail em disco
            String pastaTemporariaEmail = criar_pasta_temporaria("Email");
            client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            client.PickupDirectoryLocation = pastaTemporariaEmail;
            client.Send(mail);

            // Ler o e-mail salvo no passo anterior e renomear 
            string[] filePaths = Directory.GetFiles(pastaTemporariaEmail);
            String novo_nome_de_arquivo = pastaTemporariaEmail + "\\email_" + Consultar_Documentos.converter_nome(filtro.TipoDocumento) + "(Enviado em " + DateTime.Now.ToString("ddMMyyyy_hhm") + ").eml";
            System.IO.File.Move(@filePaths[0], novo_nome_de_arquivo);

            // Anexar e-mail salvo em disco a aplicacao.
            ConsultaExtratoCRMBLL CRMBLL = new ConsultaExtratoCRMBLL();
            String resultado = CRMBLL.Anexar_Email(novo_nome_de_arquivo, filtro);

            System.IO.File.Delete(novo_nome_de_arquivo);

            if (resultado != "Arquivo adicionado com sucesso!")
            {
                throw new ArgumentNullException(resultado);
            }

        }

        protected static String criar_pasta_temporaria(String sufixo)
        {
            // criar pasta temporaria para identificar o email       
            // Gerar numero randomico para criar a pasta
            Random rndNum = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            int rnd = rndNum.Next(0, 1000000000);
            // criar a pasta utilizando a data e o numero randomico
            //String pastaTemporaria = Consulta_GED_CRM.Properties.Settings.Default.Pasta_Arquivos_Temporarios;
            String pastaTemporaria = HttpContext.Current.Server.MapPath(@"UploadFile\Idocs");
            if (!Directory.Exists(pastaTemporaria))
            {
                Directory.CreateDirectory(pastaTemporaria);
            }
            pastaTemporaria = pastaTemporaria + "\\" + sufixo + "_" + DateTime.Today.ToString("yyyyMMdd") + "_" + rnd.ToString();
            System.IO.Directory.CreateDirectory(pastaTemporaria);
            return pastaTemporaria;
        }

        protected static MemoryStream fazer_download_arquivo(string DocumentoPDF, String IdIdocs, String Servidor)
        {

            //String Servidor = Consulta_GED_CRM.Properties.Settings.Default.Servidor_GED;

            //String Servidor = "10.190.35.143";  //doctst
            //String Servidor = "10.190.35.57";//docprod

            // Obter o nome do arquivo PDF
            //String DocumentoPDF_Nome = DocumentoPDF.Substring(DocumentoPDF.LastIndexOf("/") + 1, DocumentoPDF.Length - DocumentoPDF.LastIndexOf("/") - 1);
            //String DocumentoPDF_Nome_Completo = pastaTemporariaEmailAnexo + "\\" + DocumentoPDF_Nome;

            //baixar arquivo binario PDF
            WebClient web = new WebClient();
            System.IO.Stream rawStream = web.OpenRead("http://" + Servidor + "/ged/idocs_visualizar_doc.php?tipo_doc=" + IdIdocs + "&conta=fcesp&file_doc=" + DocumentoPDF + "&name_doc=" + DocumentoPDF.Substring(8, DocumentoPDF.Length - 8));
            //System.IO.Stream stream_pagina_web = web.OpenRead("http://" + Servidor + "/Fcesp_V.php?Documento=" + DocumentoPDF);
            //string uriPath = "http://docprod/ged/idocs_download.php?tipodoc=tp_cobsaud&pathdoc=2012/cp17_COBSAUD_00100004431_012012_T754315.pdf&filedoc_down=cp17_COBSAUD_00100004431_012012_T754315.pdf";             
            //stream_pagina_web.Flush();

            String pdf = null;

            using (StreamReader reader = new StreamReader(rawStream, Encoding.GetEncoding("ISO-8859-1")))
            {
                pdf = reader.ReadToEnd();
                reader.Close();
            }

            // salvar o arquivo na pasta temporaria
            //System.IO.MemoryStream stream_arquivo_so = new System.IO.MemoryStream();//new FileStream(DocumentoPDF_Nome_Completo, FileMode.Create, FileAccess.Write);
            //stream_pagina_web.CopyTo(stream_arquivo_so);

            // limpar as variaveis
            rawStream.Close();
            //stream_arquivo_so.Close();

            //return DocumentoPDF_Nome_Completo;

            byte[] byteArray = Encoding.GetEncoding("ISO-8859-1").GetBytes(pdf);
            //byte[] byteArray = Encoding.ASCII.GetBytes(pdf);
            MemoryStream stream = new MemoryStream(byteArray);

            return stream;
        }
    }

    public static class Consultar_Documentos
    {

        public static String converter_nome(int nr)
        {
            switch (nr)
            {
                case 1:
                    return "pagsupl";
                case 2:
                    return "contrib";
                case 3:
                    return "irrfapo";
                case 4:
                    return "cobvinc";
                case 5:
                    return "cobrsep";
                case 6:
                    return "revisao";
                case 7:
                    return "Não Utiliza";
                case 8:
                    return "cobsaud";
                case 9:
                    return "utilamh";
                case 10:
                    return "utilpes";
                case 11:
                    return "credree";
                case 12:
                    return "utilanu";
                case 13:
                    return "cejadmn";
                case 14:
                    return "irrfcre";
                case 15:
                    return "terquit";
                case 16:
                    return "pagcompc";
                case 17:
                    return "cejsaud";
                case 18:
                    return "credpcc";
                default:
                    return "Invalido";
            }
        }

        public static String converter_nome2(int nr)
        {
            switch (nr)
            {
                case 1:
                    return "Aviso Pagamento - Suplementado";
                case 2:
                    return "Extrato Previdenciário";
                case 3:
                    return "Informe Rendimento Assistidos";
                case 4:
                    return "Extrato Cobrança - Autopatrocinados";
                case 5:
                    return "Extrato Cobrança - Seguros e Pecúlio";
                case 6:
                    return "Carta Revisão INSS";
                case 7:
                    return "Não Utiliza";
                case 8:
                    return "Extrato Cobrança - Saúde";
                case 9:
                    return "Extrato Mensal Utilização - AMH";
                case 10:
                    return "Extrato Mensal Utilização - PES";
                case 11:
                    return "Crédito Reembolso";
                case 12:
                    return "Extrato Anual Utilização";
                case 13:
                    return "Carta de Cobrança ExtraJudicial Adm";
                case 14:
                    return "Informe Rendimento Credenciados";
                case 15:
                    return "Termo Quitação Serviços Prestados";
                case 16:
                    return "Aviso Pagamento - Complementados";
                case 17:
                    return "Extrato Cobrança - Saúde";  //Solicitação feita pela Nilce para alterar de "Carta Cobrança Extrajudicial - Saúde (PES)" para "Extrato Cobrança - Saúde".
                case 18:
                    return "Comprovante Anual – Outros impostos";  //Solicitação feita pela Nilce
                default:
                    return "Invalido";
            }
        }

        public static List<DocumentoPDF> obter_lista_arquivos_PDF(Filtro_Pesquisa filtro, String Servidor)
        {
            ConsultaExtratoCRMBLL CRMBLL = new ConsultaExtratoCRMBLL();
            List<String> Lista_conversao_nomes = CRMBLL.Obter_lista_conversao_nomes(filtro);
            String SQL = CRMBLL.Gerar_Comando_Select(ref filtro, Lista_conversao_nomes);
            List<DocumentoPDF> documentos = CRMBLL.executar_comando_Select(SQL, Servidor);
            return documentos;
        }

    }
}