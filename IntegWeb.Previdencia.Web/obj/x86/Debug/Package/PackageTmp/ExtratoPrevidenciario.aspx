<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Principal.Master" CodeBehind="ExtratoPrevidenciario.aspx.cs" Inherits="IntegWeb.Previdencia.Web.ExtratoPrev" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="MarginGrid">
             <script type="text/javascript">

                 //$(document).ready(function () {
                 function javascriptFunctionName() {

                     $('#ContentPlaceHolder1_optTipo_1').click(function () {
                         $('.periodo1').show();
                         $('.periodo2').hide();
                         $('#ContentPlaceHolder1_txtEMail').removeAttr('disabled');
                         $('#ContentPlaceHolder1_btnGerarPdf').removeAttr('disabled');
                         $('#ContentPlaceHolder1_btnEmail').removeAttr('disabled');
                         $('#ContentPlaceHolder1_ddlPeriodo').removeAttr('disabled');
                     });

                     $('#ContentPlaceHolder1_optTipo_0, #ContentPlaceHolder1_optTipo_2').click(function () {
                         $('.periodo1').hide();
                         $('.periodo2').hide();
                         $('#ContentPlaceHolder1_txtEMail').removeAttr('disabled');
                         $('#ContentPlaceHolder1_btnGerarPdf').removeAttr('disabled');
                         $('#ContentPlaceHolder1_btnEmail').removeAttr('disabled');
                     });

                     $('#ContentPlaceHolder1_optTipo_3').click(function () {
                         $('.periodo1').hide();
                         $('.periodo2').show();
                         $('#ContentPlaceHolder1_txtEMail').attr('disabled', 'disabled');
                         $('#ContentPlaceHolder1_btnGerarPdf').attr('disabled', 'disabled');
                         $('#ContentPlaceHolder1_btnEmail').attr('disabled', 'disabled');
                     });

                 };

                </script>
        <asp:UpdatePanel runat="server" ID="UpdatePanel">
            <ContentTemplate>
            <div class="full_w">
                <div class="tabelaPagina">
                <h1>Extrato Previdenciário</h1>
                <table>
                    <tr>
	                    <td>Empresa</td>
	                    <td>	                
                            <asp:TextBox ID="txtCodEmpresa" runat="server" style="width:100px;"></asp:TextBox>
	                    </td>
                        <td>Matrícula</td>
	                    <td>
                            <asp:TextBox ID="txtCodMatricula" runat="server" style="width:100px;" OnTextChanged="txtCodMatricula_TextChanged"></asp:TextBox>
	                    </td>
	                </tr>
	                <tr>
                        <td>Tipo de extrato:</td>
	                    <td colspan="3">
                            <asp:RadioButtonList ID="optTipo" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Text="Simples&nbsp&nbsp&nbsp&nbsp&nbsp" Value="1" Selected="True"></asp:ListItem>                                
                                <asp:ListItem Text="Detalhado&nbsp&nbsp&nbsp" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Dados Previdenciários&nbsp&nbsp&nbsp" Value="3"></asp:ListItem>                                
                                <asp:ListItem Text="Períodos anteriores" Value="4"></asp:ListItem>
                            </asp:RadioButtonList>
	                    </td>
	                </tr>
	                <tr class="periodo1" runat="server" id="periodo1" style="display:none;">
                        <td>Período:</td>
	                    <td colspan="3">
                            <asp:DropDownList ID="ddlPeriodo" runat="server" Width="200px" Enabled="false"></asp:DropDownList>
	                    </td>
	                </tr>
	                <tr class="periodo2" runat="server" id="periodo2" style="display:none;">
                        <td>Período De:</td>
	                    <td>
                            <asp:DropDownList id="ddlTrimestreDe" runat="server" style="width:130px;">
                                <asp:ListItem Value="0" Text="Trimestre"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Janeiro/Fevereiro/Março"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Abril/Maio/Junho"></asp:ListItem>
                                <asp:ListItem Value="9" Text="Julho/Agosto/Setembro"></asp:ListItem>
                                <asp:ListItem Value="12" Text="Outubro/Novembro/Dezembro"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList id="ddlAnoDe" runat="server">
                                <asp:ListItem Value="0" Text="Ano"></asp:ListItem>                                
                            </asp:DropDownList>
	                    </td>
                        <td>&nbspAté:&nbsp</td>
	                    <td>
                            <asp:DropDownList id="ddlTrimestreAte" runat="server" style="width:130px;">
                                <asp:ListItem Value="0" Text="Trimestre"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Janeiro/Fevereiro/Março"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Abril/Maio/Junho"></asp:ListItem>
                                <asp:ListItem Value="9" Text="Julho/Agosto/Setembro"></asp:ListItem>
                                <asp:ListItem Value="12" Text="Outubro/Novembro/Dezembro"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList id="ddlAnoAte" runat="server">
                                <asp:ListItem Value="0" Text="Ano"></asp:ListItem>                                
                            </asp:DropDownList>
	                    </td>
	                </tr>
	                <tr>
                        <td>Enviar por e-mail</td>
	                    <td colspan="3">
                            <asp:TextBox ID="txtEMail" runat="server" Width="200px"></asp:TextBox><asp:Button ID="btnEmail" runat="server" CssClass="button" Text="Enviar" OnClick="btnEmail_Click" style="height: 20px; padding-top: 3px;" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="E-Mail inválido." ControlToValidate="txtEMail"
                            ForeColor="Red" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" Text="" ValidationGroup="email"></asp:RegularExpressionValidator>
	                    </td>
	                </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnVisualizar" runat="server" CssClass="button" Text="Visualizar" OnClick="btnVisualizar_Click" />
                            <asp:Button ID="btnGerarPdf" runat="server" CssClass="button" Text="Download Pdf" OnClick="btnGerarPdf_Click" />                            
                            <asp:Button ID="btnRelatorio" runat="server" CssClass="button" Text="Carregar Relatório" Visible="false"/>
                        </td>
                    </tr>
                </table>

                    <iframe ID="ifExtratoPrevSysDocs" name="ifExtratoPrevSysDocs" height="380px" width="90%" src="" frameborder="0" runat="server" visible="false">
                    </iframe>
                    
                    </div>
                <uc1:ReportCrystal runat="server" ID="ReportCrystal" Sytle="display: none;" />
            </div>        
        </ContentTemplate>
        </asp:UpdatePanel>
        </div>    
        <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
            <ProgressTemplate>
                <div id="carregando">
                    <div class="carregandoTxt">
                        <img src="img/processando.gif" />
                        <br /><br />
                        <h2>Processando. Aguarde...</h2>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
</asp:Content>