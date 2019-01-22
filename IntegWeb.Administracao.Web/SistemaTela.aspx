<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="SistemaTela.aspx.cs" Inherits="IntegWeb.Administracao.Web.SistemaTela" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upSistema" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="mvwSistema" runat="server">

                <asp:View ID="vwLista" runat="server">
                    <div class="full_w">

                        <div class="h_title">

                        </div>
                        <h2>Lista de Sistemas</h2>

                        <br />

                        <asp:GridView ID="grvSistema" runat="server"
                            AutoGenerateColumns="false"
                            OnRowCommand="grvSistema_RowCommand"
                            AllowPaging="true" PageSize="10" OnPageIndexChanging="grvSistema_PageIndexChanging">

                            <Columns>

                                <asp:BoundField HeaderText="Código" DataField="Codigo" />
                                <asp:BoundField HeaderText="Nome" DataField="Nome" />
                                <asp:BoundField HeaderText="Status" DataField="descricao_status" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEditar" runat="server" CssClass="button"
                                            Text="Editar" CommandName="EDITAR" CommandArgument='<%# Eval("Codigo") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkExcluir" runat="server" CssClass="button"
                                            Text="Alterar Status" CommandName="STATUS" CommandArgument='<%# Eval("Codigo")+","+ Eval("status") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                        </asp:GridView>

                        <br />

                        <asp:Button ID="btnNovo" runat="server"
                            Text="Inserir Sistema" OnClick="btnNovo_Click" CssClass="button" />
                    </div>
                </asp:View>

                <asp:View ID="vwCadastro" runat="server">
                    <div class="full_w">

                        <div class="h_title">
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
                            Text="Informe a descrição"
                            ForeColor="Red"></asp:RequiredFieldValidator>

                        <br />
                        <br />

                        <asp:Button ID="btnSalvar" runat="server"
                            Text="Salvar" OnClick="btnSalvar_Click" CssClass="button" />

                        <asp:Button ID="btnAlterar" runat="server"
                            Text="Alterar" OnClick="btnAlterar_Click" CssClass="button" />

                        <asp:Button ID="btnCancelar" runat="server"
                            Text="Voltar" OnClick="btnCancelar_Click"
                            CssClass="button" />
                    </div>
                </asp:View>

            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
