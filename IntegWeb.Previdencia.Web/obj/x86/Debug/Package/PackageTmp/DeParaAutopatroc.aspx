<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="DeParaAutopatroc.aspx.cs" Inherits="IntegWeb.Previdencia.Web.DeParaAutopatroc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="full_w">
        <div class="h_title">
        </div>
        
        <h1>Integração Financeira - Autopatrocínio X Empresa Destino</h1>
        <asp:UpdatePanel runat="server" ID="upUpdatepanel">
            <ContentTemplate>
                <asp:Panel class="tabelaPagina" ID="pnlPesquisa" runat="server">
                    <table>
                        <tr>
                            <td>Empresa:</td>
                            <td><asp:TextBox ID="txtPesqEmpresa" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" MaxLength="4"></asp:TextBox></td>
                            <td>Matrícula:</td>
                            <td><asp:TextBox ID="txtPesqMatricula" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" MaxLength="7"></asp:TextBox></td>
                            <td>Empresa Destino:</td>
                            <td><asp:TextBox ID="txtPesqEmpresaDestino" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" MaxLength="4"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Exclusão Total:</td>
                            <td><asp:CheckBox ID="chkPesqExclusaoTotal" runat="server" /></td>
                            <td>VD Patrocinadora:</td>
                            <td><asp:CheckBox ID="chkPesqVDPatroc" runat="server" /></td>
                            <td>VD Participante:</td>
                            <td><asp:CheckBox ID="chkPesqVDPartic" runat="server" /></td>
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
                            <td>Matrícula:</td>
                            <td><asp:TextBox ID="txtMatricula" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" MaxLength="7"></asp:TextBox></td>
                            <td>Empresa Destino:</td>
                            <td><asp:TextBox ID="txtEmpresaDestino" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" MaxLength="4"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Exclusão Total:</td>
                            <td><asp:CheckBox ID="chkExclusaoTotal" runat="server" /></td>
                            <td>VD Patrocinadora:</td>
                            <td><asp:CheckBox ID="chkVDPatroc" runat="server" /></td>
                            <td>VD Participante:</td>
                            <td><asp:CheckBox ID="chkVDPartic" runat="server" /></td>
                        </tr>
                    </table>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlLista" class="tabelaPagina" Visible="true">
                    <asp:ObjectDataSource runat="server" ID="odsCCusto"
                        TypeName="IntegWeb.Previdencia.Aplicacao.BLL.Capitalizacao.DeParaAutopatrocBLL"
                        SelectMethod="GetData"
                        SelectCountMethod="GetDataCount"
                        EnablePaging="true"
                        SortParameterName="sortParameter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtPesqEmpresa" Name="pEmpresa" PropertyName="Text" Type="Int16" ConvertEmptyStringToNull="true"/>
                            <asp:ControlParameter ControlID="txtPesqMatricula" Name="pMatricula" PropertyName="Text" Type="Int32" ConvertEmptyStringToNull="true"/>
                            <asp:ControlParameter ControlID="txtPesqEmpresaDestino" Name="pEmpresaDestino" PropertyName="Text" Type="Int16" ConvertEmptyStringToNull="true"/>
                            <asp:ControlParameter ControlID="chkPesqExclusaoTotal" Name="pFlagExclusao" PropertyName="Checked" Type="Boolean" ConvertEmptyStringToNull="true"/>
                            <asp:ControlParameter ControlID="chkPesqVDPatroc" Name="pFlagPatroc" PropertyName="Checked" Type="Boolean" ConvertEmptyStringToNull="true"/>
                            <asp:ControlParameter ControlID="chkPesqVDPartic" Name="pFlagParticip" PropertyName="Checked" Type="Boolean" ConvertEmptyStringToNull="true"/>
                        </SelectParameters>
                    </asp:ObjectDataSource>

                    <asp:GridView ID="grdCCusto" runat="server"
                        DataKeyNames="COD_EMPRS_ORIGEM,NUM_RGTRO_EMPRG,COD_EMPRS_DESTINO"
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
                            <asp:BoundField HeaderText="Empresa" DataField="COD_EMPRS_ORIGEM" SortExpression="COD_EMPRS_ORIGEM" />
                            <asp:BoundField HeaderText="Matrícula" DataField="NUM_RGTRO_EMPRG" SortExpression="NUM_RGTRO_EMPRG" />
                            <asp:BoundField HeaderText="Empresa Destino" DataField="COD_EMPRS_DESTINO" SortExpression="COD_EMPRS_DESTINO" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAlterar" runat="server" CssClass="button"
                                        Text="Abrir" CommandName="Edit" CommandArgument='<%# Eval("COD_EMPRS_ORIGEM") + "," + Eval("NUM_RGTRO_EMPRG") + "," + Eval("COD_EMPRS_DESTINO") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkExcluir" runat="server" CssClass="button"
                                        Text="Excluir" CommandName="Delete" CommandArgument='<%# Eval("COD_EMPRS_ORIGEM") + "," + Eval("NUM_RGTRO_EMPRG") + "," + Eval("COD_EMPRS_DESTINO") %>' OnClientClick="return confirm('Deseja realmente excluir?');"></asp:LinkButton>
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
