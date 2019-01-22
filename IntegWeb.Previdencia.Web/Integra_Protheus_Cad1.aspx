<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="Integra_Protheus_Cad1.aspx.cs" Inherits="IntegWeb.Previdencia.Web.Integra_Protheus_Cad1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="full_w">
        <div class="h_title">
        </div>
        
        <h1>
            
            <asp:Label ID="lblDescricaoTela" runat="server"/>
            <%--Integração Protheus - Cadastro Centro de Custo Saúde--%>
        </h1>
        <asp:UpdatePanel runat="server" ID="upUpdatepanel">
            <ContentTemplate>
                <asp:Panel class="tabelaPagina" ID="pnlPesquisa" runat="server">
                    <table>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlTabela" runat="server" >
                                    <asp:ListItem Text="País Clifor" Value="CTA_TBL_PAIS" />
                                    <asp:ListItem Text="Resgate de Cotas Submassa" Value="SUBMASSA" />
                                    <asp:ListItem Text="Pagamento da Rede" Value="TP_MOVTO" />
                                    <asp:ListItem Text="Programa Protheus - Saúde" Value="PLN_PRG_SAU" />
                                    <asp:ListItem Text="Programa Protheus - Previdência" Value="PLN_PRG_PRV" />
                                    <asp:ListItem Text="Associação de Verba" Value="ASSOCIACAO_VERBA" />                                    
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Código:</td>
                            <td><asp:TextBox ID="txtPesqCodigo" Width="100px" runat="server" MaxLength="5"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Descrição:</td>
                            <td colspan="3"><asp:TextBox ID="txtPesqDescricao" Width="200px" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label id="lblPesqDescricao2" runat="server" Text="Descrição 2:"></asp:Label></td>
                            <td colspan="3"><asp:TextBox ID="txtPesqDescricao2" Width="200px" runat="server"></asp:TextBox></td>
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
                            <td>Código:</td>
                            <td><asp:TextBox ID="txtCodigo" Width="100px" runat="server" MaxLength="5"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Descrição:</td>
                            <td colspan="3"><asp:TextBox ID="txtDescricao" Width="200px" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label id="ldlDescricao2" runat="server" Text="Descrição 2:"></asp:Label></td>
                            <td colspan="3"><asp:TextBox ID="txtDescricao2" Width="200px" runat="server"></asp:TextBox></td>
                        </tr>

                    </table>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlLista" class="tabelaPagina" Visible="true">
                    <asp:ObjectDataSource runat="server" ID="odsCCusto"
                        TypeName="IntegWeb.Previdencia.Aplicacao.BLL.Int_Protheus.IntegraProtheusBLL"
                        SelectMethod="GetData"
                        SelectCountMethod="GetDataCount"
                        EnablePaging="true"
                        SortParameterName="sortParameter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlTabela" Name="pTabela" PropertyName="SelectedValue" Type="String" ConvertEmptyStringToNull="true"/>
                            <asp:ControlParameter ControlID="txtPesqCodigo" Name="pCodigo" PropertyName="Text" Type="Int32" ConvertEmptyStringToNull="true"/>
                            <asp:ControlParameter ControlID="txtPesqDescricao" Name="pDescricao" PropertyName="Text" Type="String" ConvertEmptyStringToNull="true"/>
                            <asp:ControlParameter ControlID="txtPesqDescricao2" Name="pDescricao2" PropertyName="Text" Type="String" ConvertEmptyStringToNull="true"/>
                        </SelectParameters>
                    </asp:ObjectDataSource>

                    <asp:GridView ID="grdCCusto" runat="server"
                        DataKeyNames="CODIGO"
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
                            <asp:BoundField HeaderText="Código" DataField="CODIGO" SortExpression="CODIGO" />
                            <asp:BoundField HeaderText="Descrição" DataField="DESCRICAO" SortExpression="DESCRICAO" />
                            <asp:BoundField HeaderText="Descrição2" DataField="DESCRICAO2" SortExpression="DESCRICAO2" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAlterar" runat="server" CssClass="button"
                                        Text="Abrir" CommandName="Edit" CommandArgument='<%# Eval("CODIGO") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkExcluir" runat="server" CssClass="button"
                                        Text="Excluir" CommandName="Delete" CommandArgument='<%# Eval("CODIGO") %>' OnClientClick="return confirm('Deseja realmente excluir?');"></asp:LinkButton>
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
