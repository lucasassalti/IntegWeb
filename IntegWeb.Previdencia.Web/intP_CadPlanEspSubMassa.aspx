<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="intP_CadPlanEspSubMassa.aspx.cs" Inherits="IntegWeb.Previdencia.Web.intP_CadPlanEspSubMassa" %>

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

    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>

            <div class="full_w">
                <div class="h_title">
                </div>
                <div class="tabelaPagina">
                    <h1>Integração Protheus - Plano Especie X SubMassa</h1>

                    <asp:Panel ID="pnlInserir" runat="server">
                        <table>
                            <tr>
                                <td>Empresa:
                                </td>
                                <td>Plano:
                                </td>
                                <td>Especie:
                                </td>
                                <td>Submassa:
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtEmpresa" runat="server" Width="150px" MaxLength="3" onkeypress="mascara(this, soNumeros)" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPlanoInsert" runat="server" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlEspecieInsert" runat="server" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSubMassaInsert" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnAddPlanEspSubMassa" runat="server" CssClass="button" Text="Inserir" OnClick="btnAddPlanEspSubMassa_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                    <asp:Panel ID="pnlCadPlanEspSubMassaGrid" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnConsultar" runat="server" CssClass="button" Text="Consultar" OnClick="btnConsultar_Click" />
                                </td>
                            </tr>

                            <asp:Panel ID="pnlCadPlanEspSubMassaPesquisar" runat="server" Visible="false">
                                <tr>
                                    <td>Pesquisar Por SubMassa:
                                    <asp:DropDownList ID="ddlSubMassaFiltro" runat="server" />
                                        <asp:Button ID="btnFiltrarSubMassa" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnFiltrarSubMassa_Click" />
                                    </td>
                                </tr>
                            </asp:Panel>
                        </table>

                        <asp:GridView ID="grdDadosPlanEspSubMassa" runat="server"
                            AutoGenerateColumns="False"
                            EmptyDataText="A consulta não retornou registros"
                            CssClass="Table"
                            Font-Size="13px"
                            AllowPaging="True"
                            OnRowCommand="grdDadosPlanEspSubMassa_RowCommand"
                            OnRowEditing="grdDadosPlanEspSubMassa_RowEditing"
                            OnRowCancelingEdit="grdDadosPlanEspSubMassa_RowCancelingEdit"
                            OnRowUpdating="grdDadosPlanEspSubMassa_RowUpdating"
                            OnPageIndexChanging="grdDadosPlanEspSubMassa_PageIndexChanging"
                            OnRowDataBound="grdDadosPlanEspSubMassa_RowDataBound"
                            ClientIDMode="Static"
                            Visible="false">

                            <Columns>
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIDPlanEspSubMassa" runat="server" Text='<%# Bind("COD_PLN_SUBMASSA") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Empresa">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpresa" runat="server" Text='<%# Bind("COD_EMPRS") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEmpresa" runat="server" Text='<%# Bind("COD_EMPRS") %>'
                                            MaxLength="3" onkeypress="mascara(this, soNumeros)" Width="100%" />
                                    </EditItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Plano">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlPlanoDesc" disabled="true"  runat="server"></asp:DropDownList>
                                        <asp:Label ID="lblPlanoID" runat="server" Visible="false" Text='<%# Bind("NUM_PLBNF") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlPlanoInc" runat="server"></asp:DropDownList>
                                    </EditItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Especie">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlEspecieDesc" disabled="true"  runat="server"></asp:DropDownList>
                                        <asp:Label ID="lblEspecieID" runat="server" Visible="false" Text='<%# Bind("COD_ESPBNF") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlEspecieInc" runat="server"></asp:DropDownList>
                                    </EditItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="SubMassa">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSubMassaDesc" disabled="true"  runat="server"></asp:DropDownList>
                                        <asp:Label ID="lblSubMassaID" runat="server" Visible="false" Text='<%# Bind("COD_SUBMASSA") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlSubMassaInc" runat="server"></asp:DropDownList>
                                    </EditItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Edit" CssClass="button" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CommandName="Update" CssClass="button" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>

                    </asp:Panel>
                </div>
            </div>

        </ContentTemplate>
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

</asp:Content>
