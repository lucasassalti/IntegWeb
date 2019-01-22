<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ArquivoParam.aspx.cs" Inherits="IntegWeb.Previdencia.Web.ArquivoParam" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="full_w">
        <div class="h_title">
        </div>
        
        <h1>Cadastro de Parâmetros</h1>
        <asp:UpdatePanel runat="server" ID="upUpdatepanel">
            <ContentTemplate>
                <asp:Panel class="tabelaPagina" ID="pnlPesquisa" runat="server">
                    <table>
                        <tr>
                            <td>Parâmetro:</td>
                            <td>
                                <asp:DropDownList ID="ddlPesqTipoParam" runat="server" OnSelectedIndexChanged="ddlPesqTipoParam_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="E-Mail Patrocinadora" Value="EMAIL_PATROCINADORA" Selected="True" />
                                    <asp:ListItem Text="E-Mail Área" Value="EMAIL_AREA" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Patrocinadora:</td>
                            <td>
                                <asp:DropDownList ID="ddlPesqGrupo" runat="server" Width="192px" AutoPostBack="true" OnSelectedIndexChanged="btnPesquisar_Click"></asp:DropDownList>
                            </td>
                        </tr>
                            <td>Área:</td>
                            <td>
                                <asp:DropDownList ID="ddlPesqArea" runat="server" Width="190px" Enabled="false"></asp:DropDownList>
                            </td>
                        <tr>
                            <td>Nome do parâmetro:</td>
                            <td><asp:TextBox ID="txtPesqSubParam" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" MaxLength="4"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Text="Pesquisar   " OnClick="btnPesquisar_Click" />
                                <asp:Button ID="btnLimpar" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimpar_Click" />
                                <asp:Button ID="btnNovo" runat="server" CssClass="button" Text="Novo" OnClick="btnNovo_Click" />
                            </td>
                            <td colspan="2">
                                <asp:Label id="lblPesquisa_Mensagem" runat="server" visible="False"></asp:Label> 
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <asp:Panel class="tabelaPagina" ID="pnlNovo" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnSalvar" runat="server" CssClass="button" Text="Salvar" OnClick="btnSalvar_Click" />
                                <asp:Button ID="btnVoltar" runat="server" CssClass="button" Text="Voltar" OnClick="btnVoltar_Click" />
                            </td>
                            <td colspan="2">
                                <asp:Label id="lblNovo_Mensagem" runat="server" visible="False"></asp:Label> 
                            </td>
                        </tr>

                        <tr>
                            <td>Parâmetro:</td>
                            <td>
                                <asp:Label ID="LblParametro" runat="server"></asp:Label>
                                <asp:HiddenField ID="hidParametro" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hidCodParametro" runat="server"></asp:HiddenField>
                            </td>
                        </tr>

                        <tr>
                            <td>Patrocinadora:</td>
                            <td>
                                <asp:DropDownList ID="ddlGrupo" runat="server" Width="192px" AutoPostBack="true" OnSelectedIndexChanged="btnPesquisar_Click"></asp:DropDownList>
                            </td>
                        </tr>
                            <td>Área:</td>
                            <td>
                                <asp:DropDownList ID="ddlArea" runat="server" Width="190px"></asp:DropDownList>
                            </td>
                        <tr>
                            <td>Valor do parâmetro:</td>
                            <td><asp:TextBox ID="txtParametro" Width="100px" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Sub nome:</td>
                            <td><asp:TextBox ID="txtSubParam" Width="100px" runat="server"></asp:TextBox></td>
                        </tr>
                    </table>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlLista" class="tabelaPagina" Visible="true">
                    <asp:ObjectDataSource runat="server" ID="odsParam"
                        TypeName="IntegWeb.Previdencia.Aplicacao.BLL.ArqParametrosBLL"
                        SelectMethod="GetData"
                        SelectCountMethod="GetDataCount"
                        EnablePaging="true"
                        SortParameterName="sortParameter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlPesqTipoParam" Name="pParametro" PropertyName="Text" Type="String" ConvertEmptyStringToNull="true"/>
                            <asp:ControlParameter ControlID="ddlPesqGrupo" Name="pGrupo" PropertyName="Text" Type="Int16" ConvertEmptyStringToNull="true"/>
                            <asp:ControlParameter ControlID="ddlPesqArea" Name="pArea" PropertyName="Text" Type="Int16" ConvertEmptyStringToNull="true"/>
                            <asp:ControlParameter ControlID="txtPesqSubParam" Name="pSubParam" PropertyName="Text" Type="String" ConvertEmptyStringToNull="true"/>
                        </SelectParameters>
                    </asp:ObjectDataSource>

                    <asp:GridView ID="grdParam" runat="server"
                        DataKeyNames="COD_ARQ_PARAM"
                        OnRowEditing="grdParam_RowEditing"
                        OnRowDeleting="grdParam_RowDeleting"
                        OnRowCreated="GridView_RowCreated"
                        OnRowDeleted="GridView_RowDeleted"
                        OnRowCancelingEdit="GridView_RowCancelingEdit"
                        AllowPaging="true"
                        AllowSorting="true"
                        AutoGenerateColumns="False"
                        EmptyDataText="A consulta não retornou registros"
                        CssClass="Table"
                        ClientIDMode="Static"
                        PageSize="8"
                        DataSourceID="odsParam">
                        <Columns>
                            <asp:BoundField HeaderText="Parâmetro" DataField="NOM_PARAM" SortExpression="NOM_PARAM" />
                            <asp:BoundField HeaderText="Patrocinadora" DataField="NOM_GRUPO_EMPRS" SortExpression="NOM_GRUPO_EMPRS" />
                            <asp:BoundField HeaderText="Área" DataField="NOM_ARQ_AREA" SortExpression="NOM_ARQ_AREA" />
                            <asp:BoundField HeaderText="Nome" DataField="NOM_PARAM_SUB" SortExpression="NOM_PARAM_SUB" />
                            <asp:BoundField HeaderText="Valor" DataField="DCR_PARAM" SortExpression="DCR_PARAM" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAlterar" runat="server" CssClass="button"
                                        Text="Abrir" CommandName="Edit" CommandArgument='<%# Eval("COD_ARQ_PARAM") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkExcluir" runat="server" CssClass="button"
                                        Text="Excluir" CommandName="Delete" CommandArgument='<%# Eval("COD_ARQ_PARAM") %>' OnClientClick="return confirm('Deseja realmente excluir?');"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </ContentTemplate>
<%--            <Triggers>
                <asp:PostBackTrigger ControlID="btnSalvar" />
            </Triggers>--%>
        </asp:UpdatePanel>
    </div>
</asp:Content>
