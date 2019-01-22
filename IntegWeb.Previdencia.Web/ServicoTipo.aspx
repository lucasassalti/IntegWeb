<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ServicoTipo.aspx.cs" Inherits="IntegWeb.Previdencia.Web.ServicoTipo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="full_w">

        <div class="h_title">
        </div>
        <h1>Tempo de Serviço</h1>
        <asp:UpdatePanel runat="server" ID="upPanel">
            <ContentTemplate>
                 <div id="divServico" class="tabelaPagina">
                        <table>
                            <tr>
                                <td>Digite o Tempo de Serviço:</td>
                                <td>
                                    <asp:TextBox ID="txtServico" runat="server" Width ="500"></asp:TextBox>
                                 </td>
                                <td>
                                    <asp:Button ID="btnInserir" runat="server" OnClick="btnInserir_Click" Text="Inserir" CssClass="button" />
                                </td>
                            </tr>
                        </table>
                        <asp:GridView  DataKeyNames="ID_TPSERVICO" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowDeleting="grd_RowDeleting" OnRowEditing="grd_RowEditing"  OnRowUpdating="grd_RowUpdating" AllowPaging="true"
                             AutoGenerateColumns="False" EmptyDataText="A consulta não retornou registros" CssClass="Table" ClientIDMode="Static" ID="grd"  runat="server" OnPageIndexChanging="grd_PageIndexChanging" PageSize="10">
                                <Columns>
                                   <asp:TemplateField HeaderText="Tempo de Serviço" >
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtTexto" runat="server" Text='<%# Bind("DESCRICAO") %>' Width="230px"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator18" ControlToValidate="txtTexto" runat="server" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                                    </EditItemTemplate> 
                                    <ItemTemplate>
                                        <asp:Label ID="lblTexto" runat="server" Text='<%# Bind("DESCRICAO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                    <EditItemTemplate>
                                       <asp:Button ID="btnSalvar" CssClass="button" runat="server" Text="Salvar" CommandName="Update"/>&nbsp;&nbsp;
                                       <asp:Button ID="btnCancelar" CssClass="button" runat="server" Text="Cancelar" CommandName="Cancel" OnClientClick="return confirm('Tem certeza que deseja abandonar a atualização do cadastro?');"/>
                                   </EditItemTemplate> 
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar"  CssClass="button" runat="server" Text="Editar" CommandName="Edit"/>
                                       </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=""  ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnExcluir"  CssClass="button" runat="server" Text="Excluir" CommandName="Delete" OnClientClick="return confirm('Deseja realmente excluir?');"></asp:Button>
                                     </ItemTemplate>
                                </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
               </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
