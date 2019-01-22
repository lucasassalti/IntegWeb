using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Configuration;
using IntegWeb.Framework;
using IntegWeb.Entidades.Framework;

namespace IntegWeb.Portal.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (!VerificaSemAcesso(Request.RawUrl)) VerificaAcessso(Request.RawUrl);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (!VerificaSemAcesso(Request.RawUrl)) VerificaAcessso(Request.RawUrl);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }


        private void VerificaAcessso(string url)
        {

            if ((ConfigurationManager.AppSettings["Config"] ?? String.Empty) == "D")
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["SingleSignOn"] == null)
                {
                    string SingleSignOn_teste = "<Singlesignon><CPF>22528360835</CPF><Empresa>4</Empresa><Prontuario>2527</Prontuario><Matric_previdencia>87744</Matric_previdencia><Representante>0</Representante><Target>ArquivoPatrocinadora.aspx</Target><ListaEmpresas><Empresa>67</Empresa><Empresa>44</Empresa><Empresa>1</Empresa><Empresa>2</Empresa><Empresa>92</Empresa><Empresa>71</Empresa><Empresa>79</Empresa><Empresa>62</Empresa><Empresa>80</Empresa><Empresa>82</Empresa><Empresa>83</Empresa><Empresa>89</Empresa><Empresa>66</Empresa><Empresa>90</Empresa><Empresa>87</Empresa><Empresa>86</Empresa><Empresa>84</Empresa><Empresa>81</Empresa><Empresa>95</Empresa><Empresa>91</Empresa><Empresa>99</Empresa><Empresa>43</Empresa><Empresa>45</Empresa><Empresa>88</Empresa><Empresa>50</Empresa><Empresa>40</Empresa><Empresa>42</Empresa><Empresa>4</Empresa><Empresa>61</Empresa><Empresa>60</Empresa></ListaEmpresas><ListaGrupos><Grupo>wpsadmins</Grupo><Grupo>crm</Grupo><Grupo>ATIVO_004</Grupo><Grupo>rh_patrocinadora_down_040_044</Grupo><Grupo>rh_patrocinadora_down_043</Grupo><Grupo>rh_patrocinadora_down_050</Grupo><Grupo>rh_patrocinadora_down_050_088</Grupo><Grupo>rh_patrocinadora_down_088</Grupo><Grupo>rh_patrocinadora_down_002_062_066_071_072</Grupo><Grupo>rh_patrocinadora_down_004</Grupo><Grupo>rh_patrocinadora_down_001</Grupo><Grupo>rh_patrocinadora_down_040</Grupo><Grupo>rh_patrocinadora_down_042</Grupo><Grupo>rh_patrocinadora_down_044</Grupo><Grupo>rh_patrocinadora_down_045</Grupo><Grupo>Colaboradores_Saude_Connect</Grupo><Grupo>ATIVO</Grupo><Grupo>GRUPO_S_CESP</Grupo><Grupo>consulta dados cadastrais</Grupo><Grupo>consulta ficha financeira</Grupo><Grupo>consulta historico de adesao da previdencia</Grupo><Grupo>consultar apolice</Grupo><Grupo>processo de sinistro</Grupo><Grupo>Func - Previdencia - 2 via contribuicoes trimestrais</Grupo><Grupo>Func - Previdencia - 2 via Rendimentos Anuais</Grupo><Grupo>Func - Previdencia - 2 via Aviso Suplementos</Grupo><Grupo>Func - Previdencia - Extrato de Tempo de Servico</Grupo><Grupo>Func - Previdencia - Adesao ao Plano</Grupo><Grupo>Func - Previdencia - 2 via cobranca vinculados</Grupo><Grupo>Func - Seguros e Peculio - 2 via cobranca</Grupo><Grupo>Func - Previdencia - Simulacao de Beneficios</Grupo><Grupo>Func - Previdencia - Simulacao Resgate Cotas</Grupo><Grupo>Func - Seguros e Peculio - Simulacao Capital Segurado</Grupo><Grupo>Func - Previdencia - 2 via revisao de suplementacao</Grupo><Grupo>ATIVA_CONSULTA_PARTICIPANTE</Grupo><Grupo>Func_inibicao_envio_extrato</Grupo><Grupo>Func - Previdencia - Extrato Previdenci?rio</Grupo><Grupo>auditoria</Grupo><Grupo>Conselho Fiscal - Publicador</Grupo><Grupo>funcionalidades</Grupo><Grupo>simula_emprestimo</Grupo><Grupo>Func - Previdencia - Alteracao Contribuicao Voluntaria</Grupo><Grupo>Func - Seguros e Peculio - Alteracao Cobertura SAP</Grupo><Grupo>Funcs - Saude</Grupo><Grupo>Func - Previdencia - Consulta Rentabilidade Plano</Grupo><Grupo>Func - Cadastro - Atualizacoes Cadastrais Ativos</Grupo><Grupo>Todos_Participantes</Grupo><Grupo>INTEGRANET</Grupo><Grupo>Editores_Portal_WCM</Grupo><Grupo>Gerenciador de Conteudo</Grupo><Grupo>Auditoria Folow-Up</Grupo></ListaGrupos></Singlesignon>";
                    //string SingleSignOn_teste = "<Singlesignon><CPF>22528360835</CPF><Empresa>4</Empresa><Prontuario>2527</Prontuario><Matric_previdencia>87744</Matric_previdencia><Representante>0</Representante><Target>ArquivoPatrocinadora.aspx</Target><ListaEmpresas><Empresa>67</Empresa><Empresa>44</Empresa><Empresa>1</Empresa><Empresa>2</Empresa><Empresa>92</Empresa><Empresa>71</Empresa><Empresa>79</Empresa><Empresa>62</Empresa><Empresa>80</Empresa><Empresa>82</Empresa><Empresa>83</Empresa><Empresa>89</Empresa><Empresa>66</Empresa><Empresa>90</Empresa><Empresa>87</Empresa><Empresa>86</Empresa><Empresa>84</Empresa><Empresa>81</Empresa><Empresa>95</Empresa><Empresa>91</Empresa><Empresa>99</Empresa><Empresa>43</Empresa><Empresa>45</Empresa><Empresa>88</Empresa><Empresa>50</Empresa><Empresa>40</Empresa><Empresa>42</Empresa><Empresa>4</Empresa><Empresa>61</Empresa><Empresa>60</Empresa></ListaEmpresas><ListaGrupos><Grupo>wpsadmins</Grupo><Grupo>crm</Grupo><Grupo>ATIVO_004</Grupo><Grupo>ATIVO</Grupo><Grupo>GRUPO_S_CESP</Grupo><Grupo>consulta dados cadastrais</Grupo><Grupo>consulta ficha financeira</Grupo><Grupo>consulta historico de adesao da previdencia</Grupo><Grupo>consultar apolice</Grupo><Grupo>processo de sinistro</Grupo><Grupo>Func - Previdencia - 2 via contribuicoes trimestrais</Grupo><Grupo>Func - Previdencia - 2 via Rendimentos Anuais</Grupo><Grupo>Func - Previdencia - 2 via Aviso Suplementos</Grupo><Grupo>Func - Previdencia - Extrato de Tempo de Servico</Grupo><Grupo>Func - Previdencia - Adesao ao Plano</Grupo><Grupo>Func - Previdencia - 2 via cobranca vinculados</Grupo><Grupo>Func - Seguros e Peculio - 2 via cobranca</Grupo><Grupo>Func - Previdencia - Simulacao de Beneficios</Grupo><Grupo>Func - Previdencia - Simulacao Resgate Cotas</Grupo><Grupo>Func - Seguros e Peculio - Simulacao Capital Segurado</Grupo><Grupo>Func - Previdencia - 2 via revisao de suplementacao</Grupo><Grupo>ATIVA_CONSULTA_PARTICIPANTE</Grupo><Grupo>Func_inibicao_envio_extrato</Grupo><Grupo>Func - Previdencia - Extrato Previdenci?rio</Grupo><Grupo>auditoria</Grupo><Grupo>Conselho Fiscal - Publicador</Grupo><Grupo>funcionalidades</Grupo><Grupo>simula_emprestimo</Grupo><Grupo>Func - Previdencia - Alteracao Contribuicao Voluntaria</Grupo><Grupo>Func - Seguros e Peculio - Alteracao Cobertura SAP</Grupo><Grupo>Funcs - Saude</Grupo><Grupo>Func - Previdencia - Consulta Rentabilidade Plano</Grupo><Grupo>Func - Cadastro - Atualizacoes Cadastrais Ativos</Grupo><Grupo>Todos_Participantes</Grupo><Grupo>INTEGRANET</Grupo><Grupo>Editores_Portal_WCM</Grupo><Grupo>rh_patrocinadora_down_050_088</Grupo><Grupo>rh_patrocinadora_down_002_062_066_071_072</Grupo><Grupo>Gerenciador de Conteudo</Grupo><Grupo>Auditoria Folow-Up</Grupo></ListaGrupos></Singlesignon>";
                    //string SingleSignOn_teste = "<Singlesignon><CPF>02141732832</CPF><Empresa>42</Empresa><Prontuario>565</Prontuario><Matric_previdencia>0</Matric_previdencia><Representante>0</Representante><Target>ArquivoPatrocinadora.aspx</Target><ListaEmpresas><Empresa>42</Empresa></ListaEmpresas><ListaGrupos><Grupo>rh_patrocinadora_down_042</Grupo></ListaGrupos></Singlesignon>";
                    HttpContext.Current.Session["SingleSignOn"] = (Singlesignon)Util.Object2XML(SingleSignOn_teste, typeof(Singlesignon));
                    List<string> list = new List<string>();
                    list.Add("ArquivoPatrocinadora.aspx");
                    list.Add("ArquivoPatrocinadoraEnv");
                    list.Add("AcertoDemonstrativo.aspx");
                    list.Add("TrocaArquivos.aspx");
                    list.Add("ExtratoUtilizacaoComponente.aspx");
                    list.Add("index.aspx");
                    Session["Acessos"] = list;
                }
            }

            // Caso tenha permissão para ArquivoPatrocinadora.aspx, libera para ArquivoPatrocinadora.ASHX:
            url = url.Replace("ArquivoPatrocinadora.ashx", "ArquivoPatrocinadora.aspx");

            if (url.ToLower().IndexOf("/singlesignon.aspx") > -1 ||
                url.IndexOf("/Timeout.aspx") > -1 ||
                url.IndexOf("/Accessdenied.aspx") > -1 ||
                url.IndexOf("/Content") > -1 ||
                url.IndexOf("/css") > -1 ||
                url.IndexOf("/js") > -1 ||
                url.IndexOf("/img") > -1 ||
                url.IndexOf("/WebResource.axd") > -1 ||
                url.IndexOf("/ScriptResource.axd") > -1 ||
                url.IndexOf("/CrystalImageHandler.aspx") > -1 ||
                url.IndexOf("/crystalreportviewers13") > -1 ||
                url.IndexOf("/favicon.ico") > -1) // ||
                //(ConfigurationManager.AppSettings["Config"] ?? String.Empty) == "D")
            {
                return;
            }

            if ((HttpContext.Current.Session != null) &&
                HttpContext.Current.Session["Acessos"] != null)
            {
                var list = (List<string>)HttpContext.Current.Session["Acessos"];
                string[] vet = url.Split(char.Parse("/"));
                bool isValid = false;
                url = vet[vet.Length - 1].ToString();
                foreach (var item in list)
                {
                    //if (item.Contains(url))
                    //    isValid = true;
                    // Habilita querystring
                    if (url.Contains(item))
                        isValid = true;
                }

                if (!isValid)
                {
                    //Session["mensagem"] = "Erro!!!\\nVocê não tem permissão para essa página!";
                    //Response.Redirect("~/index.aspx");
                    Response.Redirect("~/Accessdenied.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Timeout.aspx");
            }
        }

        private bool VerificaSemAcesso(string pagina)
        {
            //Função para uma relação de páginas aspx que não precisam do SingleSignOn
            return pagina.Contains("/AtualizacaoCadastral.aspx")
                || pagina.Contains("/AtualizacaoCadastralLogin.aspx")
                || pagina.Contains("/AtualizacaoCadastralConfirmacao.aspx")
                || pagina.Contains("/AtualizacaoCadastralErro.aspx");
        }
    }
}