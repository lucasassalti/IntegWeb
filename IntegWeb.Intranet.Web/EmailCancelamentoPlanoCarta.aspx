<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="EmailCancelamentoPlanoCarta.aspx.cs" Inherits="IntegWeb.Intranet.Web.EmailCancelamentoPlanoCarta" %>

<!DOCTYPE html>
<html>
<head>
    <title>Cancelamento de Plano de Saúde</title>
    <link href="css/styleCancelamento.css" rel="stylesheet" />

</head>

<body>
    <form id="formCancelamento" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="UpdatePanel">
            <ContentTemplate>
                <div id="conteudo" runat="server" align="center">
                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                      <tr>
    	                    <td width="32%" valign="baseline" class="cabec">Alameda Santos, 2477<br/>
    	                      01419 907 S&atilde;o Paulo SP<br/>
    	                      <span class="cabec_negrito">funcesp.com.br</span></td>
                            <td width="62%" valign="baseline" class="cabec">Disque-Funda&ccedil;&atilde;o<br/>
                              + 55 11 3065 3000<br/>
                    0800 012 7173</td>
                            <td width="6%"><img src="/img/imgFuncesp.png"/></td>
                            </tr>

                    </table>

                    <div align="left">
                        <p>
                            <strong>DATA</strong>:&nbsp;<asp:Label ID="lblDataCancelamento" runat="server" /><br />
                            <strong>HORA</strong>:&nbsp;<asp:Label ID="lblHoraCancelamento" runat="server" /><br />
                            <strong>PROTOCOLO</strong>:&nbsp;<asp:Label ID="lblProtocoCancelamento" runat="server" />
                        </p>
                    </div>
                    <br />
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="50%">
                                <p><strong>EMPRESA</strong></p>
                            </td>
                            <td width="50%">
                                <p><strong>MATRÍCULA</strong></p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p>
                                    <asp:Label ID="lblEmpresa" runat="server" />
                                </p>
                            </td>
                            <td>
                                <p>
                                    <asp:Label ID="lblMatricula" runat="server" />
                                </p>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="conteudo-carta">
                        <p>
                            Caro(a) &nbsp;
                            <asp:Label ID="lblResponsavelPlano" runat="server" />
                        </p>
                        <br />

                        <p>Confirmamos o recebimento do seu pedido de exclusão do plano de saúde, registrado nesta data, para os beneficiários informados a seguir: </p>
                        <br />
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td><strong>
                                    <asp:Label ID="lblBeneficiario1" runat="server" />
                                </strong></td>
                                <td>
                                    <asp:Label ID="lblPlano1" runat="server" /></td>
                            </tr>
                            <tr>
                                <td><strong>
                                    <asp:Label ID="lblBeneficiario2" runat="server" />
                                </strong></td>
                                <td>
                                    <asp:Label ID="lblPlano2" runat="server" /></td>
                            </tr>
                            <tr>
                                <td><strong>
                                    <asp:Label ID="lblBeneficiario3" runat="server" />
                                </strong></td>
                                <td>
                                    <asp:Label ID="lblPlano3" runat="server" /></td>
                            </tr>
                        </table>
                        <br />
                        <p>Ressaltamos que, nos termos do artigo 15 da RN 412 da Agência Nacional de Saúde Suplementar (ANS) de 10 de novembro de 2016, a solicitação de exclusão de beneficiário do plano de saúde tem efeito imediato e caráter irrevogável a partir da ciência da operadora, resultando nas seguintes consequências:</p><br />

                        <p><strong>1 </strong>– Eventual ingresso em novo plano de saúde poderá importar:</p><br />
                        <blockquote>a) no cumprimento de novos períodos de carência, observado o disposto no inciso V do artigo 12, da Lei nº 9.656, de 3 de junho de 1998;</blockquote><br />
                        <blockquote>b) na perda do direito à portabilidade de carências, caso não tenha sido este o motivo do pedido, nos termos previstos na RN nº 186, de 14 de janeiro de 2009, que dispõe, em especial, sobre a regulamentação da portabilidade das carências previstas no inciso V do art. 12 da Lei nº 9.656, de 3 de junho de 1998;</blockquote><br />
                        <blockquote>c) no preenchimento de nova declaração de saúde, e, caso haja doença ou lesão preexistente – DLP, no cumprimento de Cobertura Parcial Temporária – CPT, que determina, por um período ininterrupto de até 24 meses, a partir da data da contratação ou adesão ao novo plano, a suspensão da cobertura de Procedimentos de Alta Complexidade (PAC), leitos de alta tecnologia e procedimentos cirúrgicos;</blockquote><br />
                        <p><strong>2 </strong>- Efeito imediato e caráter irrevogável da solicitação de cancelamento do contrato ou exclusão de beneficiário, a partir da ciência da operadora;  </p><br />
                        <strong>3</strong> – As contraprestações pecuniárias vencidas e/ou eventuais coparticipações devidas, nos planos em pré-pagamento ou em pós-pagamento, pela utilização de serviços realizados antes da solicitação de cancelamento ou exclusão do plano de saúde são de responsabilidade do beneficiário;<br />

                    <p><strong>4</strong> - As despesas decorrentes de eventuais utilizações dos serviços pelos beneficiários após a data de solicitação de cancelamento ou exclusão do plano de saúde, inclusive nos casos de urgência ou emergência, correrão por sua conta;</p><br />



                        <p><strong>5</strong> – A exclusão do beneficiário titular do contrato coletivo empresarial observará as disposições contratuais quanto à exclusão ou não dos dependentes, conforme o disposto no inciso II do parágrafo único do artigo 18, da RN nº 195, de 14 de julho de 2009, que dispõe sobre a classificação e características dos planos privados de assistência à saúde, regulamenta a sua contratação, institui a orientação para contratação de planos privados de assistência à saúde e dá outras providências</p>



                        <p align="center">Comunicamos que todas as utilizações do plano de saúde realizadas pelos beneficiários até a data de exclusão do plano, serão cobradas posteriormente pela Funcesp. </p>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!--Fim conteudo-->
    </form>
</body>
</html>
