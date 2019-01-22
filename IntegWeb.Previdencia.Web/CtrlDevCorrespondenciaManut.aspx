<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="CtrlDevCorrespondenciaManut.aspx.cs" Inherits="IntegWeb.Previdencia.Web.CtrlDevCorrespondenciaManut" %>

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
                <h1>Manutenção - Controle de Devoluções de Correspondecias</h1>
                <div class="tabelaPagina">
                    <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0">
                        <ajax:TabPanel ID="tbTipoPlanoAcao" HeaderText="Plano de Ação" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <asp:Panel ID="pnlTipoPlanoAcaoInsert" runat="server">
                                    <table>
                                        <tr>
                                            <td>Inserção da Descrição do Plano de Ação</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDescricaoPlanoAcao" runat="server" Style="width: 190px;" /></td>
                                            <td>
                                                <asp:Button ID="btnInserirPlanoAcao" runat="server" CssClass="button" Text="Inserir" OnClick="btnInserirPlanoAcao_Click" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnConsultarTipoAcao" runat="server" CssClass="button" Text="Consultar" OnClick="btnConsultarTipoAcao_Click" /></td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="pnlTipoAcaoConsulta" runat="server">
                                    <asp:GridView ID="grdControleTipoPlanoAcao"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        EmptyDataText="Não retornou registros"
                                        AllowPaging="True"
                                        CssClass="Table"
                                        OnRowCommand="grdControleTipoPlanoAcao_RowCommand"
                                        OnRowEditing="grdControleTipoPlanoAcao_RowEditing"
                                        OnRowUpdating="grdControleTipoPlanoAcao_RowUpdating"
                                        OnRowCancelingEdit="grdControleTipoPlanoAcao_RowCancelingEdit"
                                        OnPageIndexChanging="grdControleTipoPlanoAcao_PageIndexChanging"
                                        ClientIDMode="Static"
                                        Font-Size="13px">
                                        <Columns>

                                            <asp:TemplateField HeaderText="ID" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdReg" runat="server" Text='<%# Bind("ID_REG") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="12px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Descrição Ação">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescricao" runat="server" Text='<%# Bind("descricao") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDescricao" runat="server" Text='<%# Bind("descricao") %>'
                                                        Width="100%" />
                                                </EditItemTemplate>
                                                <HeaderStyle Font-Size="12px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEditarControle" runat="server" Text="Editar" CommandName="Edit" CssClass="button" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CommandName="Update" CssClass="button" />
                                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>

                            </ContentTemplate>
                        </ajax:TabPanel>

                        <ajax:TabPanel ID="tbTipoDocumento" HeaderText="Tipos de Documentos" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <asp:Panel ID="pnlTipoDocumento" runat="server">
                                    <table>
                                        <tr>
                                            <td>Inserção da Descrição do Tipo de Documento</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDescTipoDocumento" runat="server" Style="width: 190px;" /></td>
                                            <td>
                                                <asp:Button ID="btnInserirTipoDocumento" runat="server" CssClass="button" Text="Inserir" OnClick="btnInserirTipoDocumento_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnConsultarTipoDocumento" runat="server" CssClass="button" Text="Consultar" OnClick="btnConsultarTipoDocumento_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="pnlTipoDocumentoConsulta" runat="server">
                                    <asp:GridView ID="grdControleTipoDocumento"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        EmptyDataText="Não retornou registros"
                                        AllowPaging="True"
                                        CssClass="Table"
                                        OnRowCommand="grdControleTipoDocumento_RowCommand"
                                        OnRowEditing="grdControleTipoDocumento_RowEditing"
                                        OnRowUpdating="grdControleTipoDocumento_RowUpdating"
                                        OnRowCancelingEdit="grdControleTipoDocumento_RowCancelingEdit"
                                        OnPageIndexChanging="grdControleTipoDocumento_PageIndexChanging"
                                        ClientIDMode="Static"
                                        Font-Size="13px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdReg" runat="server" Text='<%# Bind("ID_REG") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Descrição Ação" HeaderStyle-Font-Size="12px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescricao" runat="server" Text='<%# Bind("descricao") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDescricao" runat="server" Text='<%# Bind("descricao") %>'
                                                        Width="100%" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEditarControle" runat="server" Text="Editar" CommandName="Edit" CssClass="button" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CommandName="Update" CssClass="button" />
                                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </ContentTemplate>
                        </ajax:TabPanel>

                        <ajax:TabPanel ID="tbMotivoDevolucao" HeaderText="Motivo da Devolução" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <asp:Panel ID="pnlMotivoDevolucao" runat="server">
                                    <table>
                                        <tr>
                                            <td>Inserção do Tipo de Motivo Devolução</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDescMotivoDevolucao" runat="server" Style="width: 190px;" /></td>
                                            <td>
                                                <asp:Button ID="btnInserirMotivoDevolucao" runat="server" CssClass="button" Text="Inserir" OnClick="btnInserirMotivoDevolucao_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnConsultarMotivoDevolucao" runat="server" CssClass="button" Text="Consultar" OnClick="btnConsultarMotivoDevolucao_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="pnlMotivoDevolucaoConsulta" runat="server">
                                    <asp:GridView ID="grdControleMotivoDevolucao"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        EmptyDataText="Não retornou registros"
                                        AllowPaging="True"
                                        CssClass="Table"
                                        OnRowCommand="grdControleMotivoDevolucao_RowCommand"
                                        OnRowEditing="grdControleMotivoDevolucao_RowEditing"
                                        OnRowUpdating="grdControleMotivoDevolucao_RowUpdating"
                                        OnRowCancelingEdit="grdControleMotivoDevolucao_RowCancelingEdit"
                                        OnPageIndexChanging="grdControleMotivoDevolucao_PageIndexChanging"
                                        ClientIDMode="Static"
                                        Font-Size="13px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdReg" runat="server" Text='<%# Bind("ID_REG") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Descrição Ação" HeaderStyle-Font-Size="12px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescricao" runat="server" Text='<%# Bind("descricao") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDescricao" runat="server" Text='<%# Bind("descricao") %>'
                                                        Width="100%" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEditarControle" runat="server" Text="Editar" CommandName="Edit" CssClass="button" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CommandName="Update" CssClass="button" />
                                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>

                            </ContentTemplate>
                        </ajax:TabPanel>

                        <ajax:TabPanel ID="tbFluxoAcao" HeaderText="Fluxo da Ação" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <asp:Panel ID="pnlFluxoAcao" runat="server">
                                    <table>
                                        <tr>
                                            <td>Associação - Tipo de Documento & Tipo Ação</td>
                                        </tr>
                                        <tr>
                                            <td>Tipo Documento:</td>
                                            <td>Tipo Ação:</td>
                                            <td>Tempo Prazo:</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoDocumentoInsert" runat="server"></asp:DropDownList>
                                            </td>

                                            <td>
                                                <asp:DropDownList ID="ddlTipoAcaoInsert" runat="server"></asp:DropDownList>
                                            </td>
                                            <td>
                                              <asp:DropDownList ID="ddlTempoPrazoInsert" runat="server">
                                                        <asp:ListItem Text="SIM" Value="SIM" />
                                                        <asp:ListItem Text="NÃO" Value="NAO" />
                                                    </asp:DropDownList>
                                                </td>
                                            <td>
                                                <asp:Button ID="btnInserirFluxoAcao" runat="server" CssClass="button" Text="Inserir" OnClick="btnInserirFluxoAcao_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnConsultarFluxoAcao" runat="server" CssClass="button" Text="Consultar" OnClick="btnConsultarFluxoAcao_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="pnlFluxoAcaoConsulta" runat="server">
                                    <asp:GridView ID="grdControleFluxoAcao"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        EmptyDataText="Não retornou registros"
                                        AllowPaging="True"
                                        CssClass="Table"
                                        OnRowCommand="grdControleFluxoAcao_RowCommand"
                                        OnRowEditing="grdControleFluxoAcao_RowEditing"
                                        OnRowUpdating="grdControleFluxoAcao_RowUpdating"
                                        OnRowCancelingEdit="grdControleFluxoAcao_RowCancelingEdit"
                                        OnPageIndexChanging="grdControleFluxoAcao_PageIndexChanging"
                                        OnRowDataBound="grdControleFluxoAcao_RowDataBound"
                                        ClientIDMode="Static"
                                        Font-Size="13px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdReg" runat="server" Text='<%# Bind("ID_REG") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tipo Documento" HeaderStyle-Font-Size="12px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTipoDocumentoDesc" runat="server" Text='<%# Bind("DescricaoDoc") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlTipoDocumentoDescInc"  runat="server"></asp:DropDownList>
                                                    <asp:Label ID="lblIdDescricaoDoc" runat="server"  Visible="false" Text='<%# Bind("IdDescricaoDoc") %>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tipo Ação" HeaderStyle-Font-Size="12px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTipoAcaoDesc" runat="server" Text='<%# Bind("DescricaoAcao") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlTipoAcaoDescInc"  runat="server"></asp:DropDownList>
                                                    <asp:Label ID="lblIdDescricaoAcao" runat="server"  Visible="false" Text='<%# Bind("IdDescricaoAcao") %>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tempo Prazo" HeaderStyle-Font-Size="12px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTempoPrazo" runat="server" Text='<%# Bind("TempoPrazo") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlTempoPrazoInc" runat="server"></asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEditarControle" runat="server" Text="Editar" CommandName="Edit" CssClass="button" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CommandName="Update" CssClass="button" />
                                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>

                            </ContentTemplate>
                        </ajax:TabPanel>
                    </ajax:TabContainer>
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
