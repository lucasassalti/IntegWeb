<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Principal.Master" CodeBehind="RetiraFolhaFestaAposentado.aspx.cs" Inherits="IntegWeb.Intranet.Web.RetiraFolhaFestaAposentado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>



    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>

            <div class="tabelaPagina">
                <div class="full_w">
                    <table>
                        <tr>
                            <td>
                                <h1>Usuários Disque Funcesp</h1>
                            </td>
                        </tr>

                        <tr>
                            <asp:Table ID="tblInfosPesquisa" runat="server">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblMatricula" runat="server" CssClass="panelLabel" Text="Matrícula do usuário: "></asp:Label>

                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtMatricula" runat="server" CssClass="text" Width="200px"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="btnConsultarGrid" runat="server" CssClass="button" Text="Consultar" OnClick="btnConsultarGrid_Click" />
                                    </asp:TableCell>
                                    
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblNomeUsuario" runat="server" CssClass="panelLabel" Text="Nome do usuário:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtNomeUsuario" runat="server" CssClass="text" Width="200px"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="btnInserir" runat="server" CssClass="button" Text="Inserir" OnClick="btnInserir_Click" Width="80px" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="grdUsuariosDisque" runat="server"
                                    AutoGenerateColumns="False"
                                    EmptyDataText="A consulta não retornou registros"
                                    CssClass="Table"
                                    OnRowCommand="grdUsuariosDisque_RowCommand"
                                    OnRowEditing="grdUsuariosDisque_RowEditing"
                                    OnPreRender="grdUsuariosDisque_PreRender"
                                    OnSelectedIndexChanging="grdUsuariosDisque_SelectedIndexChanging"
                                    OnPageIndexChanged="grdUsuariosDisque_PageIndexChanged"
                                    Visible="false"
                                    PageSize="5">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Código Empresa">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodigoEmpresa" Font-Size="Larger" runat="server" Text='<%# Bind("COD_EMPRS") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Matrícula">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodigoMatricula" Font-Size="Larger" runat="server" Text='<%# Bind("NUM_RGTRO_EMPRG") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nome">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDcrMunicipio" Font-Size="Larger" runat="server" Text='<%# Bind("NOM_EMPRG") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                            <asp:ButtonField
                                                ButtonType="Button"
                                                CommandName="Exclude"
                                                Text="Excluir" ControlStyle-CssClass="button"></asp:ButtonField>
                                        
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
