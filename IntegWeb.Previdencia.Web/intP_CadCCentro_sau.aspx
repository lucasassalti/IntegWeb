<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="intP_CadCCentro_sau.aspx.cs" Inherits="IntegWeb.Previdencia.Web.intP_CadCCentro_sau" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="full_w">
        <div class="h_title">
        </div>
        
        <h1>Integração Protheus - Cadastro Centro de Custo Saúde</h1>
        <asp:UpdatePanel runat="server" ID="upUpdatepanel">
            <ContentTemplate>
                <asp:Panel class="tabelaPagina" ID="pnlPesquisa" runat="server">
                    <table>
                        <tr>
                            <td>Empresa:</td>
                            <td><asp:TextBox ID="txtPesqEmpresa" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" Text="4" Enabled="false"></asp:TextBox></td>
                            <td>Núm. Orgão:</td>
                            <td><asp:DropDownList ID="ddlPesqNumOrgao" runat="server" Width="204px" /></td>
                            <%--<td><asp:TextBox ID="txtPesqNumOrgao" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox></td>--%>
                            <td>Adm?</td>
                            <td><asp:CheckBox ID="chkPesqAdm" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Centro Custo:</td>
                            <td><asp:TextBox ID="txtPesqCCusto" Width="100px" runat="server" MaxLength="4"></asp:TextBox></td>
                            <td>Desc. CCusto:</td>
                            <td colspan="3"><asp:TextBox ID="txtPesqDescCCusto" Width="200px" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Text="Pesquisar   " OnClick="btnPesquisar_Click" />
                                <asp:Button ID="btnLimpar" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimpar_Click" />
                                <asp:Button ID="btnNovo" runat="server" CssClass="button" Text="Novo" OnClick="btnNovo_Click" />
                            </td>
                            <td colspan="3">
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
                            <td><asp:TextBox ID="txtEmpresa" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" ReadOnly="true" BackColor="Silver"></asp:TextBox></td>
                            <td>Núm. Orgão:</td>
                            <td><asp:DropDownList ID="ddlNumOrgao" runat="server" Width="204px" /></td>
                            <%--<td><asp:TextBox ID="txtNumOrgao" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox></td>--%>
                            <td>Adm?</td>
                            <td><asp:CheckBox ID="chkAdm" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Centro Custo:</td>
                            <td><asp:TextBox ID="txtCCusto" Width="100px" runat="server" MaxLength="4"></asp:TextBox></td>
                            <td>Desc. CCusto:</td>
                            <td colspan="3"><asp:TextBox ID="txtDescCCusto" Width="200px" runat="server"></asp:TextBox></td>
                        </tr>
                    </table>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlLista" class="tabelaPagina" Visible="true">
                    <asp:ObjectDataSource runat="server" ID="odsCCusto"
                        TypeName="IntegWeb.Previdencia.Aplicacao.BLL.Int_Protheus.CCustoBLL"
                        SelectMethod="GetData"
                        SelectCountMethod="GetDataCount"
                        EnablePaging="true"
                        SortParameterName="sortParameter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtPesqEmpresa" Name="pEmpresa" PropertyName="Text" Type="Int16" ConvertEmptyStringToNull="true"/>
                            <asp:ControlParameter ControlID="ddlPesqNumOrgao" Name="pNumOrgao" PropertyName="SelectedValue" Type="Int32" ConvertEmptyStringToNull="true"/>
                            <asp:ControlParameter ControlID="chkPesqAdm" Name="pDspAdm" PropertyName="Checked" Type="Boolean" ConvertEmptyStringToNull="true"/>
                            <asp:ControlParameter ControlID="txtPesqCCusto" Name="pCcusto" PropertyName="Text" Type="String" ConvertEmptyStringToNull="true"/>
                            <asp:ControlParameter ControlID="txtPesqDescCCusto" Name="pDscCcusto" PropertyName="Text" Type="String" ConvertEmptyStringToNull="true"/>
                        </SelectParameters>
                    </asp:ObjectDataSource>

                    <asp:GridView ID="grdCCusto" runat="server"
                        DataKeyNames="COD_EMPRS,NUM_ORGAO,DSP_ADM"
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
                            <asp:BoundField HeaderText="Empresa" DataField="COD_EMPRS" SortExpression="COD_EMPRS" />
                            <asp:BoundField HeaderText="Núm. Orgão" DataField="NUM_ORGAO" SortExpression="NUM_ORGAO" />
                            <asp:BoundField HeaderText="Adm?" DataField="DSP_ADM" SortExpression="DSP_ADM" />
                            <asp:BoundField HeaderText="Centro Custo" DataField="CCUSTO" SortExpression="CCUSTO" />
                            <asp:BoundField HeaderText="Descrição C. Custo" DataField="DSC_CCUSTO" SortExpression="DSC_CCUSTO" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAlterar" runat="server" CssClass="button"
                                        Text="Abrir" CommandName="Edit" CommandArgument='<%# Eval("COD_EMPRS") + "," + Eval("NUM_ORGAO") + "," + Eval("DSP_ADM") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkExcluir" runat="server" CssClass="button"
                                        Text="Excluir" CommandName="Delete" CommandArgument='<%# Eval("COD_EMPRS") + "," + Eval("NUM_ORGAO") + "," + Eval("DSP_ADM") %>' OnClientClick="return confirm('Deseja realmente excluir?');"></asp:LinkButton>
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
