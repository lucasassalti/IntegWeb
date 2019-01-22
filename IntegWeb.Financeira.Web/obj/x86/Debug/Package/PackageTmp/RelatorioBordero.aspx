<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="RelatorioBordero.aspx.cs" Inherits="IntegWeb.Financeira.Web.RelatorioBordero" %>

<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        var updateProgress = null;

        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProg1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }
    </script>
    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">
            <h1>Relatórios Borderôs</h1>
            <asp:UpdatePanel runat="server" ID="upUpdatePanel">
                <ContentTemplate>
                    <asp:RadioButtonList ID="rdListPesquisa" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdListPesquisa_SelectedIndexChanged">
                        <asp:ListItem Text="Por data" Value="1">
                        </asp:ListItem>
                        <asp:ListItem Text="Por borderô" Value="2"></asp:ListItem> 
                            </asp:RadioButtonList>
                    <asp:Table ID="tbPrincipal" runat="server">
                        <asp:TableRow ID="tbRowData" runat="server" Visible="false"> 
                            <asp:TableCell>                  
                            Data inicial: <asp:TextBox ID="txtDtInicial" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px"></asp:TextBox>
                                    <asp:RangeValidator
                                        runat="server"
                                        ID="rangDtInicial"
                                        Type="Date"
                                        ControlToValidate="txtDtInicial"
                                        MaximumValue="31/12/9999"
                                        MinimumValue="31/12/1000"
                                        ErrorMessage="Data Inválida"
                                        ForeColor="Red"
                                        Display="Dynamic"
                                        ValidationGroup="ValiDt" />
                                
                            
                            </asp:TableCell><asp:TableCell>
                                Data final:
                                <asp:TextBox ID="txtDtFinal" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px"></asp:TextBox>
                                
                                <asp:RangeValidator
                                        runat="server"
                                        ID="rangDtFinal"
                                        Type="Date"
                                        ControlToValidate="txtDtFinal"
                                        MaximumValue="31/12/9999"
                                        MinimumValue="31/12/1000"
                                        ErrorMessage="Data Inválida"
                                        ForeColor="Red"
                                        Display="Dynamic"
                                        ValidationGroup="ValiDt" />  
                            </asp:TableCell></asp:TableRow><asp:TableRow ID="tbRowBord" runat="server" Visible="false">
                            <asp:TableCell>
                            Nº borderô inicial: 
                                <asp:TextBox ID="txtNumBorderoInicial" runat="server" Width="68" MaxLength="6"></asp:TextBox>  
                            
                                </asp:TableCell><asp:TableCell>
                            
                                Nº borderô final: 
                                <asp:TextBox ID="txtNumBorderoFinal" runat="server" Width="68" MaxLength="6"></asp:TextBox>
                            
                        </asp:TableCell></asp:TableRow><asp:TableRow runat="server">
                        
                        <asp:TableCell>    
                           
                                <center><asp:Button ID="btnGerarRel" runat="server" CssClass="button" OnClick="btnGerarRel_Click" Text="Gerar relatório" CausesValidation="true" ValidationGroup="ValiDt" /></center>
                            
                        </asp:TableCell></asp:TableRow></asp:Table></ContentTemplate></asp:UpdatePanel><asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server" Style="display: none">
                <ProgressTemplate>
                    <div id="carregando">
                        <div class="carregandoTxt">
                            <img src="img/processando.gif" />
                            <br />
                            <h2>Processando. Aguarde...</h2></div></div></ProgressTemplate></asp:UpdateProgress></div></div></asp:Content>