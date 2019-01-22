<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="ExclusaoBemEstar.aspx.cs" Inherits="IntegWeb.Intranet.Web.ExclusaoBemEstar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>

            <div class="full_w">

                <div class="tabelaPagina">

                    <h1>&nbsp Suspensão do envio da Revista Bem Estar</h1>
                    
                        <table>
                            <tr>
                                <td>Empresa:</td>
                                <td>
                                    <asp:TextBox ID="txtCodEmpresa" runat="server" Style="width: 50px;" onkeypress="mascara(this, soNumeros)" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td style="text-align: end;">Matrícula:</td>
                                <td>
                                    <asp:TextBox ID="txtCodMatricula" runat="server" Style="width: 70px;" onkeypress="mascara(this, soNumeros)" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Representante:</td>
                                <td>
                                    <asp:TextBox ID="txtNumRepresentante" runat="server" onkeypress="mascara(this, soNumeros)" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Nome:</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtNome" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    
                    <div>
                        <table>
                               <tr>
                                <td><b>Suspensão do envio da Revista Bem Estar:</b></td>
                            </tr>
                            <tr>
                                <asp:GridView ID="grdBemestar" runat="server"
                                    AllowSorting="false"
                                    AutoGenerateColumns="False"
                                    EmptyDataText="Para cadastrar a suspensão de envio, clicar em Inserir "
                                    CssClass="Table"
                                    >
                                    <Columns>
                                        <asp:BoundField DataField="COD_EMPRS" HeaderText="Empresa" />
                                        <asp:BoundField DataField="NUM_RGTRO_EMPRG" HeaderText="Matricula" />
                                        <asp:BoundField DataField="NUM_IDNTF_RPTANT" HeaderText="Representante" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnDeletar" runat="server" CssClass="button" Text="Excluir" OnClick="btnDeletar_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </tr>
                            <tr>
                                <td>&nbsp
                                    <asp:Button ID="btnInserir" runat="server" CssClass="button" Text="Inserir" OnClick="btnInserir_Click" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
