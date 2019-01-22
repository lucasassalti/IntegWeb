<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="RegraControleCRC.aspx.cs" Inherits="IntegWeb.Saude.Web.RegraControleCRC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="full_w">

        <div class="h_title">
        </div>
        <h1>Regra CRC</h1>
        <asp:UpdatePanel runat="server" ID="upRegra">
            <ContentTemplate>
                <div id="divAction" class="tabelaPagina" runat="server" >
                    <table>
                        <tr>
                            <td>Descrição da Regra:</td>
                            <td>
                                <asp:TextBox ID="txtDescr" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Valor:</td>
                            <td>
                                <asp:TextBox ID="TxtVl" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnInserirRegra" runat="server" OnClick="btnInserirRegra_Click" Text="Salvar    " CssClass="button" />
                                <asp:Button ID="btnVoltar" runat="server" OnClick="btnVoltar_Click" Text="Voltar    " CssClass="button" />
                            </td>
                        </tr>
                    </table>

                   
            </div>
                  
                <div id="divSelect" class="tabelaPagina" runat="server" >
                    <table>
                            <tr>
                         
                                <td>
                                    <asp:LinkButton ID="lnkInserirRegra" runat="server" CausesValidation="False" OnClick="lnkInserirRegra_Click"  CssClass="button">Inserir Regra</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    <asp:GridView DataKeyNames="ID_REGRA" OnRowCancelingEdit="grdRegra_RowCancelingEdit" OnRowDeleting="grdRegra_RowDeleting" OnRowEditing="grdRegra_RowEditing" OnRowUpdating="grdRegra_RowUpdating" AllowPaging="true"
                        AutoGenerateColumns="False" EmptyDataText="A consulta não retornou registros" CssClass="Table" ClientIDMode="Static" ID="grdRegra" runat="server" OnPageIndexChanging="grdRegra_PageIndexChanging" PageSize="10">
                        <Columns>
                            <asp:TemplateField HeaderText="Descrição">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescricao" runat="server" Text='<%# Bind("DESCRICAO") %>' Width="230px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator18" ControlToValidate="txtDescricao" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDescricao" runat="server" Text='<%# Bind("DESCRICAO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valor">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtValor" runat="server" Text='<%# Bind("VALOR") %>' Width="80px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator19" ControlToValidate="txtValor" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblValor" runat="server" Text='<%# Bind("VALOR") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:Button ID="btnSalvar" CssClass="button" runat="server" Text="Salvar" CommandName="Update" />&nbsp;&nbsp;
                                       <asp:Button ID="btnCancelar" CssClass="button" runat="server" Text="Cancelar" CommandName="Cancel" OnClientClick="return confirm('Tem certeza que deseja abandonar a atualização do cadastro?');" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="btnEditar" CssClass="button" runat="server" Text="Editar" CommandName="Edit" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="btnExcluir" CssClass="button" runat="server" Text="Excluir" CommandName="Delete" OnClientClick="return confirm('Tem certeza que deseja excluir?');"></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
