<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="intP_CadProcessoVerba.aspx.cs" Inherits="IntegWeb.Previdencia.Web.intP_CadProcessoVerba" %>

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
                    <h1>Integração Protheus - Processo de Verba</h1>

                    <asp:Panel ID="pnlInserir" runat="server">
                        <table>
                            <tr>
                                <td>Num.VerbaFSS:
                                </td>
                                <td>Cod.Verba:
                                </td>
                                <td>Tipo Negocio:
                                </td>
                                <td>Tipo Seg:
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtNumVerbaFSS" runat="server" Width="150px" MaxLength="5" onkeypress="mascara(this, soNumeros)" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodVerba" runat="server" Width="150px" MaxLength="5" onkeypress="mascara(this, soNumeros)" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTipoNegocioInsert" runat="server" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTipoSegInsert" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnAddProcessoVerba" runat="server" CssClass="button" Text="Inserir" OnClick="btnAddProcessoVerba_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                    <asp:Panel ID="pnlCadProcessoVerbaGrid" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnConsultar" runat="server" CssClass="button" Text="Consultar" OnClick="btnConsultar_Click" />
                                </td>
                            </tr>

                            <asp:Panel ID="pnlCadProcessoVerbaPesquisar" runat="server" Visible="false">
                                <tr>
                                    <td>Pesquisar Por Número da Verba:
                                    <asp:TextBox ID="txtFiltrarVerba" runat="server" Width="150px" MaxLength="5" onkeypress="mascara(this, soNumeros)" />
                                        <asp:Button ID="btnFiltrarVerba" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnFiltrarVerba_Click" />
                                    </td>
                                </tr>
                            </asp:Panel>

                        </table>

                        <asp:GridView ID="grdDadosProcessoVerba" runat="server"
                            AutoGenerateColumns="False"
                            EmptyDataText="A consulta não retornou registros"
                            CssClass="Table"
                            Font-Size="13px"
                            AllowPaging="True"
                            OnRowCommand="grdDadosProcessoVerba_RowCommand"
                            OnRowEditing="grdDadosProcessoVerba_RowEditing"
                            OnRowCancelingEdit="grdDadosProcessoVerba_RowCancelingEdit"
                            OnRowUpdating="grdDadosProcessoVerba_RowUpdating"
                            OnPageIndexChanging="grdDadosProcessoVerba_PageIndexChanging"
                            OnRowDataBound="grdDadosProcessoVerba_RowDataBound"
                            CausesValidation="true"
                            ClientIDMode="Static"
                            Visible="false">

                            <Columns>
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIDProcessoVerba" runat="server" Text='<%# Bind("COD_VRB_NEGOCIO") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Numero Verba Fss">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumVerbaFSS" runat="server" Text='<%# Bind("NUM_VRBFSS") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtNumVerbaFSS" runat="server" Text='<%# Bind("NUM_VRBFSS") %>'
                                            MaxLength="5" onkeypress="mascara(this, soNumeros)" Width="100%" />
                                    </EditItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Código Verba">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodVerba" runat="server" Text='<%# Bind("COD_VERBA") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCodVerba" runat="server" Text='<%# Bind("COD_VERBA") %>'
                                            MaxLength="5" onkeypress="mascara(this, soNumeros)" Width="100%" />
                                    </EditItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Tipo do Negocio" HeaderStyle-Font-Size="12px">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlTipoNegocio" disabled="true" runat="server"></asp:DropDownList>
                                        <asp:Label ID="lblTipoNegocioDesc" runat="server" Visible="false" Text='<%# Bind("TIP_NEGOCIO") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlTipoNegocioDescInc" runat="server"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Tipo Seg" HeaderStyle-Font-Size="12px">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlTipoSeg" disabled="true" runat="server"></asp:DropDownList>
                                        <asp:Label ID="lblTipoSegDesc" runat="server" Visible="false" Text='<%# Bind("TIP_SEG") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlTipoSegDescInc" runat="server"></asp:DropDownList>
                                    </EditItemTemplate>
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
