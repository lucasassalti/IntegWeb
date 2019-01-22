<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="PeriodoTela.aspx.cs" Inherits="IntegWeb.Periodico.Web.PeriodoTela" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="full_w">

        <div class="h_title">
        </div>
        <h1>Período</h1>
        <asp:UpdatePanel runat="server" ID="upPeriodo">
            <ContentTemplate>
                 <div id="divPeriodo" class="tabelaPagina">
                        <table>
                            <tr>
                                <td>Nome do Período:</td>
                                <td>
                                    <asp:TextBox ID="txtPeriodo" runat="server" Width ="500"></asp:TextBox>
                                 </td>
                                <td>
                                    <asp:Button ID="btnInserirPeiriodo" runat="server" OnClick="btnInserirPeiriodo_Click" Text="Inserir Período" CssClass="button" />
                                </td>
                            </tr>
                        </table>
                        <asp:GridView  DataKeyNames="ID_PERIODO" OnRowCancelingEdit="grdPeriodo_RowCancelingEdit" OnRowDeleting="grdPeriodo_RowDeleting" OnRowEditing="grdPeriodo_RowEditing" OnRowUpdating="grdPeriodo_RowUpdating" AllowPaging="true"
                             AutoGenerateColumns="False" EmptyDataText="A consulta não retornou registros" CssClass="Table" ClientIDMode="Static" ID="grdPeriodo"  runat="server" OnPageIndexChanging="grdPeriodo_PageIndexChanging" PageSize="10">
                                <Columns>
                                   <asp:TemplateField HeaderText="Período" >
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPer" runat="server" Text='<%# Bind("DESCRICAO") %>' Width="230px"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator18" ControlToValidate="txtPer" runat="server" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                                    </EditItemTemplate> 
                                    <ItemTemplate>
                                        <asp:Label ID="lblPeriodico" runat="server" Text='<%# Bind("DESCRICAO") %>'></asp:Label>
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
                                        <asp:Button ID="btnExcluir"  CssClass="button" runat="server" Text="Excluir" CommandName="Delete" OnClientClick="return confirm('Tem certeza que deseja excluir?');"></asp:Button>
                                     </ItemTemplate>
                                </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
               </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
