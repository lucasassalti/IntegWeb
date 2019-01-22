<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="DeParaAutopatroc_Verba.aspx.cs" Inherits="IntegWeb.Previdencia.Web.DeParaAutopatroc_Verba" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="full_w">
        <div class="h_title">
        </div>
        
        <h1>Integração Financeira - Autopatrocínio X Verba</h1>
        <asp:UpdatePanel runat="server" ID="upUpdatepanel">
            <ContentTemplate>
                <asp:Panel class="tabelaPagina" ID="pnlPesquisa" runat="server">
                    <table>
                        <tr>
                            <td>Empresa:</td>
                            <td><asp:TextBox ID="txtPesqEmpresa" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" MaxLength="4"></asp:TextBox></td>
                            <td>Plano Origem:</td>
                            <td><asp:TextBox ID="txtPesqPlanoOrigem" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" MaxLength="4"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Verba Funcesp:</td>
                            <td><asp:TextBox ID="txtPesqVerbaFund" Width="100px" runat="server" MaxLength="5"></asp:TextBox></td>
                            <td>Verba Destino:</td>
                            <td colspan="3"><asp:TextBox ID="txtPesqVerbaDest" Width="200px" runat="server" MaxLength="5"></asp:TextBox></td>
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
                            <td>Empresa:</td>
                            <td><asp:TextBox ID="txtEmpresa" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" MaxLength="4"></asp:TextBox></td>
                            <td>Núm. Orgão:</td>
                            <td><asp:TextBox ID="txtPlanoOrigem" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" MaxLength="4"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Verba Funcesp:</td>
                            <td><asp:TextBox ID="txtVerbaFund" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" MaxLength="5"></asp:TextBox></td>
                            <td>Verba Destino:</td>
                            <td colspan="3"><asp:TextBox ID="txtVerbaDest" onkeypress="mascara(this, soNumeros)" Width="200px" runat="server" MaxLength="5"></asp:TextBox></td>
                        </tr>
                    </table>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlLista" class="tabelaPagina" Visible="true">
                    <asp:ObjectDataSource runat="server" ID="odsCCusto"
                        TypeName="IntegWeb.Previdencia.Aplicacao.BLL.Capitalizacao.DeParaAutopatrocVerbaBLL"
                        SelectMethod="GetData"
                        SelectCountMethod="GetDataCount"
                        EnablePaging="true"
                        SortParameterName="sortParameter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtPesqEmpresa" Name="pEmpresa" PropertyName="Text" Type="Int16" ConvertEmptyStringToNull="true"/>
                            <asp:ControlParameter ControlID="txtPesqPlanoOrigem" Name="pPlanoOrigem" PropertyName="Text" Type="Int16" ConvertEmptyStringToNull="true"/>
                            <asp:ControlParameter ControlID="txtPesqVerbaFund" Name="pVerbaFund" PropertyName="Text" Type="Int32" ConvertEmptyStringToNull="true"/>
                            <asp:ControlParameter ControlID="txtPesqVerbaDest" Name="pVerbaDest" PropertyName="Text" Type="Int32" ConvertEmptyStringToNull="true"/>
                        </SelectParameters>
                    </asp:ObjectDataSource>

                    <asp:GridView ID="grdCCusto" runat="server"
                        DataKeyNames="PLANO_ORIGEM,NUM_VER_FUND,EMPRS_DEST"
                        OnRowEditing="grdCCusto_RowEditing"
                        OnRowDeleting="grdCCusto_RowDeleting"
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
                        DataSourceID="odsCCusto">
                        <Columns>
                            <asp:BoundField HeaderText="Empresa" DataField="EMPRS_DEST" SortExpression="EMPRS_DEST" />
                            <asp:BoundField HeaderText="Plano Origem" DataField="PLANO_ORIGEM" SortExpression="PLANO_ORIGEM" />
                            <asp:BoundField HeaderText="Verba Funcesp" DataField="NUM_VER_FUND" SortExpression="NUM_VER_FUND" />
                            <asp:BoundField HeaderText="Verba Destino" DataField="NUM_VER_DEST" SortExpression="NUM_VER_DEST" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAlterar" runat="server" CssClass="button"
                                        Text="Abrir" CommandName="Edit" CommandArgument='<%# Eval("EMPRS_DEST") + "," + Eval("PLANO_ORIGEM") + "," + Eval("NUM_VER_FUND") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkExcluir" runat="server" CssClass="button"
                                        Text="Excluir" CommandName="Delete" CommandArgument='<%# Eval("EMPRS_DEST") + "," + Eval("PLANO_ORIGEM") + "," + Eval("NUM_VER_FUND") %>' OnClientClick="return confirm('Deseja realmente excluir?');"></asp:LinkButton>
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
