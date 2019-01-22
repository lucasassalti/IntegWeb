using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using System.IO;
using System.Configuration;
using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Entidades.Framework;
using IntegWeb.Administracao.Aplicacao;


namespace IntegWeb.Intranet.Web
{
    public partial class SingleSignOn_page : System.Web.UI.Page
    {

        byte codigoSistema = 7;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string param1 = Request.QueryString["param1"];

                // Endereço para testar:
                //http://homologacao.funcesp.com.br/wps/PA_IegracaoAppExterna/checktoken?url=https://intradesenv/Desenv/singlesignon.aspx
                //http://homologacao.funcesp.com.br/wps/PA_IegracaoAppExterna/checktoken?url=localhost:61022/ArquivoPatrocinadora.aspx

                if (param1 == null)
                {
                    lblMessagem.Text = " Authority " + Request.Url.Authority + " :";
                    lblMessagem.Text += "  Cód. erro: -1";
                    lblMessagem.Text += "  Msg. erro: Erro na autenticação!";
                }

                byte[] buf = new byte[8192];

                param1 = HttpUtility.UrlEncode(param1);


                string url_checktoken = ConfigurationManager.AppSettings["CheckTokenPortal"];

                url_checktoken = url_checktoken ?? "http://homologacao.funcesp.com.br/wps/PA_IegracaoAppExterna/checktoken";

                //"http://www.funcesp.com.br/wps/PA_IegracaoAppExterna/checktoken"
                //"http://homologacao.funcesp.com.br/wps/PA_IegracaoAppExterna/checktoken"

                //do get request
                HttpWebRequest request = (HttpWebRequest)
                    WebRequest.Create(url_checktoken + "?token=" + param1);

                HttpWebResponse response = (HttpWebResponse)
                    request.GetResponse();

                Stream resStream = response.GetResponseStream();

                string tempString = null;
                int count = 0;
                //read the data and print it
                do
                {
                    count = resStream.Read(buf, 0, buf.Length);
                    if (count != 0)
                    {
                        tempString = Encoding.ASCII.GetString(buf, 0, count);

                        sb.Append(tempString);
                    }
                }
                while (count > 0);

                //sb.Append("<Autenticacao><CPF>04040040</CPF><Empresa>123</Empresa> <Prontuario>12345</Prontuario> <Matric_previdencia>12345</Matric_previdencia> <Representante>12345</Representante> </Autenticacao>");
                //sb.Append("<Autenticacao><CPF>22528360835</CPF><Empresa>4</Empresa><Prontuario>2527</Prontuario>  <Matric_previdencia>87744</Matric_previdencia> <Representante>0</Representante>     </Autenticacao>");
                //sb.Append("<SingleSignOn><CPF>22528360835</CPF><Empresa>4</Empresa><Prontuario>2527</Prontuario><Matric_previdencia>87744</Matric_previdencia><Representante>0</Representante><Target>TrocaArquivos</Target><ListaEmpresas><Empresa>67</Empresa><Empresa>44</Empresa><Empresa>1</Empresa><Empresa>2</Empresa><Empresa>92</Empresa><Empresa>71</Empresa><Empresa>79</Empresa><Empresa>62</Empresa><Empresa>80</Empresa><Empresa>82</Empresa><Empresa>83</Empresa><Empresa>89</Empresa><Empresa>66</Empresa><Empresa>90</Empresa><Empresa>87</Empresa><Empresa>86</Empresa><Empresa>84</Empresa><Empresa>81</Empresa><Empresa>95</Empresa><Empresa>91</Empresa><Empresa>43</Empresa><Empresa>45</Empresa><Empresa>88</Empresa><Empresa>50</Empresa><Empresa>40</Empresa><Empresa>42</Empresa><Empresa>4</Empresa><Empresa>61</Empresa><Empresa>60</Empresa></ListaEmpresas></SingleSignOn>");
                //sb.Append("<Singlesignon><CPF>2588679730</CPF><Empresa>4</Empresa><Prontuario>2432</Prontuario><Matric_previdencia>84257</Matric_previdencia><Representante>0</Representante><Target>TrocaArquivos</Target><ListaEmpresas><Empresa>67</Empresa><Empresa>44</Empresa><Empresa>1</Empresa><Empresa>2</Empresa><Empresa>92</Empresa><Empresa>71</Empresa><Empresa>79</Empresa><Empresa>62</Empresa><Empresa>80</Empresa><Empresa>82</Empresa><Empresa>83</Empresa><Empresa>89</Empresa><Empresa>66</Empresa><Empresa>90</Empresa><Empresa>87</Empresa><Empresa>86</Empresa><Empresa>84</Empresa><Empresa>81</Empresa><Empresa>95</Empresa><Empresa>91</Empresa><Empresa>43</Empresa><Empresa>45</Empresa><Empresa>88</Empresa><Empresa>50</Empresa><Empresa>40</Empresa><Empresa>42</Empresa><Empresa>4</Empresa><Empresa>61</Empresa><Empresa>60</Empresa></ListaEmpresas></Singlesignon>");
                //sb.Append("<Singlesignon><CPF>2588679730</CPF><Empresa>4</Empresa><Prontuario>2432</Prontuario><Matric_previdencia>84257</Matric_previdencia><Representante>0</Representante><Target>ArquivoPatrocinadora.aspx</Target><ListaEmpresas><Empresa>67</Empresa><Empresa>44</Empresa><Empresa>1</Empresa><Empresa>2</Empresa><Empresa>92</Empresa><Empresa>71</Empresa><Empresa>79</Empresa><Empresa>62</Empresa><Empresa>80</Empresa><Empresa>82</Empresa><Empresa>83</Empresa><Empresa>89</Empresa><Empresa>66</Empresa><Empresa>90</Empresa><Empresa>87</Empresa><Empresa>86</Empresa><Empresa>84</Empresa><Empresa>81</Empresa><Empresa>95</Empresa><Empresa>91</Empresa><Empresa>43</Empresa><Empresa>45</Empresa><Empresa>88</Empresa><Empresa>50</Empresa><Empresa>40</Empresa><Empresa>42</Empresa><Empresa>4</Empresa><Empresa>61</Empresa><Empresa>60</Empresa></ListaEmpresas><ListaGrupos><Grupo>rh_patrocinadora_down_004</Grupo><Grupo>ATIVO_004</Grupo><Grupo>VINCULADO_004</Grupo><Grupo>VINCULADO_040</Grupo><Grupo>gerencias_chefias</Grupo><Grupo>investimentos - interno</Grupo><Grupo>demonstracoes contabeis</Grupo><Grupo>Demcont Prev CESP</Grupo><Grupo>Demcont Prev Elektro</Grupo><Grupo>Demcont Prev CPFL</Grupo><Grupo>Demcont Prev EMAE</Grupo><Grupo>Demcont Prev CTEEP</Grupo><Grupo>Demcont Prev DUKE</Grupo><Grupo>Demcont Prev Eletropaulo</Grupo><Grupo>Demcont Prev CPFL Pirat</Grupo><Grupo>Demcont Prev Bandeirante</Grupo><Grupo>Demcont Prev Edinfor</Grupo><Grupo>Follow up - Auditoria</Grupo><Grupo>Demcont Cons Funda??o CESP</Grupo><Grupo>Demcont Prev Funda??o CESP</Grupo><Grupo>Demcont Prev Tiet?</Grupo><Grupo>portal_participante_restrito</Grupo><Grupo>avaliacao_desempenho_gestor_executivo</Grupo><Grupo>Colaboradores_Demonstra??o_Cont?bil</Grupo><Grupo>Gerente_Executivo</Grupo><Grupo>Colaboradores_Relatorios_Investimentos</Grupo><Grupo>ATIVO</Grupo><Grupo>GRUPO_S_CESP</Grupo><Grupo>Func Demontra??es Cont?beis</Grupo><Grupo>niveis_hierarquicos</Grupo><Grupo>Func - Relatorios Gerenciais</Grupo><Grupo>Auditoria Folow-Up</Grupo><Grupo>VINCULADO</Grupo><Grupo>VINCULADO_S_CESP</Grupo><Grupo>consulta dados cadastrais</Grupo><Grupo>consulta ficha financeira</Grupo><Grupo>simula_emprestimo</Grupo><Grupo>consulta historico de adesao da previdencia</Grupo><Grupo>consultar apolice</Grupo><Grupo>processo de sinistro</Grupo><Grupo>Func - Previdencia - 2 via contribuicoes trimestrais</Grupo><Grupo>Func - Previdencia - Extrato de Tempo de Servico</Grupo><Grupo>Func - Previdencia - Efetuar Coligacao</Grupo><Grupo>Func - Previdencia - Resgate de Cotas</Grupo><Grupo>Func - Previdencia - Alteracao Contribuicao Voluntaria</Grupo><Grupo>Func - Seguros e Peculio - Alteracao Cobertura SAP</Grupo><Grupo>Func - Previdencia - 2 via cobranca vinculados</Grupo><Grupo>Func - Seguros e Peculio - 2 via cobranca</Grupo><Grupo>Func - Cadastro - Atualizacoes Cadastrais</Grupo><Grupo>Func - Previdencia - Simulacao de Beneficios</Grupo><Grupo>Func - Previdencia - Simulacao Resgate Cotas</Grupo><Grupo>Funcs - Saude</Grupo><Grupo>Func - Previdencia - Consulta Rentabilidade Plano</Grupo><Grupo>Funcs - Saude Restrito</Grupo><Grupo>Func - Previdencia - Extrato Previdenci?rio</Grupo><Grupo>Todos_Participantes</Grupo><Grupo>funcionalidades</Grupo><Grupo>Func - Seguros e Peculio - Simulacao Capital Segurado</Grupo><Grupo>Func - Cadastro - Atualizacoes Cadastrais Ativos</Grupo><Grupo>INTEGRANET</Grupo></ListaGrupos></Singlesignon>");
                //sb.Append("<Singlesignon><CPF>22528360835</CPF><Empresa>4</Empresa><Prontuario>2527</Prontuario><Matric_previdencia>87744</Matric_previdencia><Representante>0</Representante><Target>ArquivoPatrocinadora.aspx</Target><ListaEmpresas><Empresa>67</Empresa><Empresa>44</Empresa><Empresa>1</Empresa><Empresa>2</Empresa><Empresa>92</Empresa><Empresa>71</Empresa><Empresa>79</Empresa><Empresa>62</Empresa><Empresa>80</Empresa><Empresa>82</Empresa><Empresa>83</Empresa><Empresa>89</Empresa><Empresa>66</Empresa><Empresa>90</Empresa><Empresa>87</Empresa><Empresa>86</Empresa><Empresa>84</Empresa><Empresa>81</Empresa><Empresa>95</Empresa><Empresa>91</Empresa><Empresa>43</Empresa><Empresa>45</Empresa><Empresa>88</Empresa><Empresa>50</Empresa><Empresa>40</Empresa><Empresa>42</Empresa><Empresa>4</Empresa><Empresa>61</Empresa><Empresa>60</Empresa></ListaEmpresas><ListaGrupos><Grupo>wpsadmins</Grupo><Grupo>crm</Grupo><Grupo>ATIVO_004</Grupo><Grupo>rh_patrocinadora_down_004</Grupo><Grupo>rh_patrocinadora_down_043</Grupo><Grupo>rh_patrocinadora_down_042</Grupo><Grupo>rh_patrocinadora_down_040_044</Grupo><Grupo>Colaboradores_Saude_Connect</Grupo><Grupo>ATIVO</Grupo><Grupo>GRUPO_S_CESP</Grupo><Grupo>consulta dados cadastrais</Grupo><Grupo>consulta ficha financeira</Grupo><Grupo>consulta historico de adesao da previdencia</Grupo><Grupo>consultar apolice</Grupo><Grupo>processo de sinistro</Grupo><Grupo>Func - Previdencia - 2 via contribuicoes trimestrais</Grupo><Grupo>Func - Previdencia - 2 via Rendimentos Anuais</Grupo><Grupo>Func - Previdencia - 2 via Aviso Suplementos</Grupo><Grupo>Func - Previdencia - Extrato de Tempo de Servico</Grupo><Grupo>Func - Previdencia - Adesao ao Plano</Grupo><Grupo>Func - Previdencia - 2 via cobranca vinculados</Grupo><Grupo>Func - Seguros e Peculio - 2 via cobranca</Grupo><Grupo>Func - Previdencia - Simulacao de Beneficios</Grupo><Grupo>Func - Previdencia - Simulacao Resgate Cotas</Grupo><Grupo>Func - Seguros e Peculio - Simulacao Capital Segurado</Grupo><Grupo>Func - Previdencia - 2 via revisao de suplementacao</Grupo><Grupo>ATIVA_CONSULTA_PARTICIPANTE</Grupo><Grupo>Func_inibicao_envio_extrato</Grupo><Grupo>Func - Previdencia - Extrato Previdenci?rio</Grupo><Grupo>auditoria</Grupo><Grupo>Conselho Fiscal - Publicador</Grupo><Grupo>funcionalidades</Grupo><Grupo>simula_emprestimo</Grupo><Grupo>Func - Previdencia - Alteracao Contribuicao Voluntaria</Grupo><Grupo>Func - Seguros e Peculio - Alteracao Cobertura SAP</Grupo><Grupo>Funcs - Saude</Grupo><Grupo>Func - Previdencia - Consulta Rentabilidade Plano</Grupo><Grupo>Func - Cadastro - Atualizacoes Cadastrais Ativos</Grupo><Grupo>Todos_Participantes</Grupo><Grupo>INTEGRANET</Grupo><Grupo>Editores_Portal_WCM</Grupo><Grupo>Gerenciador de Conteudo</Grupo><Grupo>Auditoria Folow-Up</Grupo></ListaGrupos></Singlesignon>");
                //sb.Append("<Singlesignon><CPF>22528360835</CPF><Empresa>4</Empresa><Prontuario>2527</Prontuario><Matric_previdencia>87744</Matric_previdencia><Representante>0</Representante><Target>ArquivoPatrocinadora.aspx</Target><ListaEmpresas><Empresa>67</Empresa><Empresa>44</Empresa><Empresa>1</Empresa><Empresa>2</Empresa><Empresa>92</Empresa><Empresa>71</Empresa><Empresa>79</Empresa><Empresa>62</Empresa><Empresa>80</Empresa><Empresa>82</Empresa><Empresa>83</Empresa><Empresa>89</Empresa><Empresa>66</Empresa><Empresa>90</Empresa><Empresa>87</Empresa><Empresa>86</Empresa><Empresa>84</Empresa><Empresa>81</Empresa><Empresa>95</Empresa><Empresa>91</Empresa><Empresa>43</Empresa><Empresa>45</Empresa><Empresa>88</Empresa><Empresa>50</Empresa><Empresa>40</Empresa><Empresa>42</Empresa><Empresa>4</Empresa><Empresa>61</Empresa><Empresa>60</Empresa></ListaEmpresas><ListaGrupos><Grupo>wpsadmins</Grupo><Grupo>crm</Grupo><Grupo>ATIVO_004</Grupo><Grupo>ATIVO</Grupo><Grupo>GRUPO_S_CESP</Grupo><Grupo>consulta dados cadastrais</Grupo><Grupo>consulta ficha financeira</Grupo><Grupo>consulta historico de adesao da previdencia</Grupo><Grupo>consultar apolice</Grupo><Grupo>processo de sinistro</Grupo><Grupo>Func - Previdencia - 2 via contribuicoes trimestrais</Grupo><Grupo>Func - Previdencia - 2 via Rendimentos Anuais</Grupo><Grupo>Func - Previdencia - 2 via Aviso Suplementos</Grupo><Grupo>Func - Previdencia - Extrato de Tempo de Servico</Grupo><Grupo>Func - Previdencia - Adesao ao Plano</Grupo><Grupo>Func - Previdencia - 2 via cobranca vinculados</Grupo><Grupo>Func - Seguros e Peculio - 2 via cobranca</Grupo><Grupo>Func - Previdencia - Simulacao de Beneficios</Grupo><Grupo>Func - Previdencia - Simulacao Resgate Cotas</Grupo><Grupo>Func - Seguros e Peculio - Simulacao Capital Segurado</Grupo><Grupo>Func - Previdencia - 2 via revisao de suplementacao</Grupo><Grupo>ATIVA_CONSULTA_PARTICIPANTE</Grupo><Grupo>Func_inibicao_envio_extrato</Grupo><Grupo>Func - Previdencia - Extrato Previdenci?rio</Grupo><Grupo>auditoria</Grupo><Grupo>Conselho Fiscal - Publicador</Grupo><Grupo>funcionalidades</Grupo><Grupo>simula_emprestimo</Grupo><Grupo>Func - Previdencia - Alteracao Contribuicao Voluntaria</Grupo><Grupo>Func - Seguros e Peculio - Alteracao Cobertura SAP</Grupo><Grupo>Funcs - Saude</Grupo><Grupo>Func - Previdencia - Consulta Rentabilidade Plano</Grupo><Grupo>Func - Cadastro - Atualizacoes Cadastrais Ativos</Grupo><Grupo>Todos_Participantes</Grupo><Grupo>INTEGRANET</Grupo><Grupo>Editores_Portal_WCM</Grupo><Grupo>rh_patrocinadora_down_050_088</Grupo><Grupo>Gerenciador de Conteudo</Grupo><Grupo>Auditoria Folow-Up</Grupo></ListaGrupos></Singlesignon>");

                if (sb.ToString().IndexOf("Singlesignon") > -1)
                {
                    Singlesignon sso_session = (Singlesignon)Util.Object2XML(sb.ToString(), typeof(Singlesignon));

                    if (String.IsNullOrEmpty(sso_session.MsgErro))
                    {
                        Session["SingleSignOn"] = sso_session;
                        MenuBLL bll = new MenuBLL();
                        List<string> list = bll.ListarAcessoPorGrupo(codigoSistema, sso_session.ListaGrupos);
                        list.Add("index.aspx");
                        Session["Acessos"] = list;
                        //Response.Redirect("~/ArquivoPatrocinadora.aspx");
                        Response.Redirect("~/" + sso_session.Target);
                    }
                    else
                    {
                        lblMessagem.Text = " Authority " + Request.Url.Authority + " :";
                        lblMessagem.Text += "  Cód. erro: " + sso_session.CodErro;
                        lblMessagem.Text += "  Msg. erro: " + sso_session.MsgErro;
                    }
                    //Response.Redirect("~/ArquivoPatrocinadora.aspx");
                }
                else
                {
                    Response.Redirect("~/timeout.aspx");
                    //Response.Write(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                lblMessagem.Text = " Authority " + Request.Url.Authority + " :";
                lblMessagem.Text += "  Cód. erro: " + 9999;
                lblMessagem.Text += "  Msg. erro: " + ex.Message;
            }

            //Response.Write(sb.ToString());            
        }
    }
}