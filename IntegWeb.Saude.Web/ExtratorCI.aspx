<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ExtratorCI.aspx.cs" Inherits="IntegWeb.Saude.Web.ExtratorCI" %>

<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">
            <h1>Extrator de Carteirinhas </h1>
            <asp:UpdatePanel runat="server" ID="upUpdatePanel">
                <ContentTemplate>
                    <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0">
                        <%--ABA 1--%>
                        <ajax:TabPanel ID="TbProcessamentoCI" HeaderText="Processamento CI" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnProcessarCI" runat="server" CssClass="button" Text="Processar Rotina CI" OnClick="btnProcessarCI_Click" /></td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Processo_Mensagem" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajax:TabPanel>
                        <%--ABA 2--%>
                        <ajax:TabPanel ID="TbExtracaoCI" HeaderText="Extração CI" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Data de Extração:</td>
                                        <td>

                                            <asp:TextBox ID="txtData" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                                            <asp:RequiredFieldValidator runat="server" ID="reqData" ControlToValidate="txtData" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaData" />
                                            <asp:RangeValidator
                                                runat="server"
                                                ID="rangData"
                                                Type="Date"
                                                ControlToValidate="txtData"
                                                MaximumValue="31/12/9999"
                                                MinimumValue="31/12/1000"
                                                ErrorMessage="Data Inválida"
                                                ForeColor="Red"
                                                Display="Dynamic"
                                                ValidationGroup="ValidaData" />
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>Tipo Cartão:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlTipoCartao" runat="server">
                                                <asp:ListItem Text="TODOS" Value="1" />
                                                <asp:ListItem Text="PES" Value="2" />
                                                <asp:ListItem Text="AUX.MEDICAMENTO" Value="3" />
                                                <asp:ListItem Text="EXTENSIVE" Value="4" />
                                                <asp:ListItem Text="DIGNA" Value="5" />
                                                <asp:ListItem Text="NOSSO" Value="6" />
                                            </asp:DropDownList>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnGerarCI" runat="server" CssClass="button" Text="Gerar Carteirinhas" OnClick="btnGerarCI_Click" ValidationGroup="ValidaData" CausesValidation="true" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajax:TabPanel>
                        <%--ABA 3--%>
                        <ajax:TabPanel ID="TbRelCiEmitido" HeaderText="Relatório Cartões Emitidos" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Empresa:
                                                    <asp:TextBox ID="txtEmpresa" runat="server" onkeypress="mascara(this, soNumeros)" Width="50px" MaxLength="3" />
                                            <asp:RequiredFieldValidator runat="server" ID="reqEmpresa" ControlToValidate="txtEmpresa" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaAB3" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Matrícula:
                                                    <asp:TextBox ID="txtMatricula" runat="server" onkeypress="mascara(this, soNumeros)" Width="100px" MaxLength="10" />
                                            <asp:RequiredFieldValidator runat="server" ID="reqMatricula" ControlToValidate="txtMatricula" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaAB3" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Sub:
                                                 <asp:TextBox ID="txtSubMatricula" runat="server" onkeypress="mascara(this, soNumeros)" Width="50px" MaxLength="2" />
                                            <asp:RequiredFieldValidator runat="server" ID="reqSubMatricula" ControlToValidate="txtSubMatricula" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaAB3" />
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnRelatorio" runat="server" CssClass="button" Text="Carregar Relatório" OnClick="btnRelatorio_Click" ValidationGroup="ValidaAB3" CausesValidation="true"/>
                                        </td>
                                    </tr>
                                </table>
                                <div>
                                    <uc1:ReportCrystal runat="server" ID="ReportCrystal" Style="display: none;" />
                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>
                    </ajax:TabContainer>
                </ContentTemplate>
                <%--  <Triggers>
                    <asp:PostBackTrigger ControlID="TabContainer" />
                </Triggers>--%>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server" Style="display: none">
                <ProgressTemplate>
                    <div id="carregando">
                        <div class="carregandoTxt">
                            <img src="img/processando.gif" />
                            <br />
                            <h2>Processando. Aguarde...</h2>
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>

</asp:Content>
