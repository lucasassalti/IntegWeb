<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="MenuTela.aspx.cs" Inherits="IntegWeb.Administracao.Web.MenuTela" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="upMenu" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="mvwMenu" runat="server">

                <asp:View ID="vwLista" runat="server">

                    <div class="full_w">

                        <div class="h_title"></div>
                        <h2>Lista de Menus</h2>
                        <table>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="drpMenu" runat="server">
                                        <asp:ListItem Text="Selecione" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Código" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Nome" Value="2" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Sistema" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Nível" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Link" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Status" Value="6"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtPequisaMenu" runat="server" Width="300px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnPesquisaMenu" OnClick="btnPesquisaMenu_Click" runat="server" Text="Pesquisar" CssClass="button" CausesValidation="False" />
                                    <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="button" CausesValidation="false" OnClick="btnLimpar_Click" />
                                    <asp:Button ID="btnNovo" runat="server" Text="Inserir Menu" OnClick="btnNovo_Click" CssClass="button" />
                                </td>
                            </tr>
                        </table>

                        <div class="sep"></div>

                        <asp:GridView ID="grvMenu" runat="server"
                            AutoGenerateColumns="false"
                            OnRowCommand="grvMenu_RowCommand"
                            AllowPaging="true" PageSize="10" OnPageIndexChanging="grvMenu_PageIndexChanging" EmptyDataText="A consulta não retornou dados">

                            <Columns>

                                <asp:BoundField HeaderText="Código" DataField="ID_MENU" />
                                <asp:BoundField HeaderText="Nome" DataField="NM_MENU" />
                                <asp:BoundField HeaderText="Sistema" DataField="NM_SISTEMA" />
                                <asp:BoundField HeaderText="Nível" DataField="CD_NIVEL" />
                                <asp:BoundField HeaderText="Menu Pai" DataField="MENU_PAI" />
                                <asp:BoundField HeaderText="Link" DataField="DS_LINK" />
                                <asp:BoundField HeaderText="Status" DataField="descricao_status" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEditar" runat="server" CssClass="button"
                                            Text="Editar" CommandName="EDITAR" CommandArgument='<%# Eval("ID_MENU") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkExcluir" runat="server" CssClass="button"
                                            Text="Alterar Status" CommandName="Status" CommandArgument='<%# Eval("ID_MENU")+","+ Eval("Status") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                        </asp:GridView>

                        <br />
                </asp:View>

                <asp:View ID="vwCadastro" runat="server">

                    <div class="full_w">

                        <div class="h_title">
                            <label>Código</label>
                        </div>
                        <label>Código</label>
                        <br />
                        <asp:TextBox ID="txtCodigo" runat="server"
                            Width="50px" MaxLength="2"></asp:TextBox>

                        <br />
                        <br />

                        <label>Nome</label>
                        <br />
                        <asp:TextBox ID="txtNome" runat="server"
                            Width="200px" MaxLength="80"></asp:TextBox>
                        <asp:RequiredFieldValidator
                            ID="rfvNome"
                            runat="server"
                            ControlToValidate="txtNome"
                            EnableClientScript="false"
                            SetFocusOnError="true"
                            Text="Informe o nome"
                            ForeColor="Red"></asp:RequiredFieldValidator>

                        <br />
                        <br />

                        <label>Sistema</label>
                        <br />
                        <asp:DropDownList ID="ddlSistema" runat="server"
                            Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlSistema_SelectedIndexChanged">
                        </asp:DropDownList>

                        <br />
                        <br />

                        <label>Nivel</label>
                        <br />
                        <asp:DropDownList ID="ddlNivel" runat="server"
                            Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlNivel_SelectedIndexChanged">
                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                        </asp:DropDownList>

                        <br />
                        <br />

                        <label>Menu Pai</label>
                        <br />
                        <asp:DropDownList ID="ddlMenuPai" runat="server"
                            Width="400px">
                            <asp:ListItem Value="" Text="Selecione o Sistema e o Nivel" Enabled="false"></asp:ListItem>
                        </asp:DropDownList>

                        <br />
                        <br />

                        <label>Link</label>
                        <br />
                        <asp:TextBox ID="txtLink" runat="server"
                            Width="400px" MaxLength="200"></asp:TextBox>

                        <br />
                        <br />

                        <asp:Button ID="btnSalvar" runat="server"
                            Text="Salvar" OnClick="btnSalvar_Click" CssClass="button" />

                        <asp:Button ID="btnAlterar" runat="server"
                            Text="Alterar" OnClick="btnSalvar_Click" CssClass="button" />

                        <asp:Button ID="btnCancelar" runat="server"
                            Text="Voltar" OnClick="btnCancelar_Click" CssClass="button" />

                    </div>
                </asp:View>

            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
