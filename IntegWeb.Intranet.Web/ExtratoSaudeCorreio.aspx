<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="ExtratoSaudeCorreio.aspx.cs" Inherits="IntegWeb.Intranet.Web.ExtratoSaudeCorreio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>

            <div class="full_w">

                <div class="tabelaPagina">
                    <h1>&nbsp Participantes que irão receber Extratos de Saúde pelo Correio </h1>

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
                            <td>CPF:</td>
                            <td>
                                <asp:TextBox ID="txtCPF" runat="server" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td>Representante:</td>
                            <td>
                                <asp:TextBox ID="txtRepresentante" runat="server" onkeypress="mascara(this, soNumeros)" ReadOnly="true"></asp:TextBox>
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
                                <td><b>Participantes que irão receber Extratos de Saúde pelo Correio:</b></td>
                            </tr>
                            <tr>
                                <asp:GridView ID="grdExtratoSaudeCorreio" runat="server"
                                    AllowSorting="false"
                                    AutoGenerateColumns="False"
                                    EmptyDataText="Participante não Cadastrado para receber o Extrato de Saúde pelo Correio, para cadastrar clique em Inserir ! "
                                    CssClass="Table"
                                   >
                                    <Columns>
                                        <asp:BoundField DataField="COD_EMPRS" HeaderText="Empresa" />
                                        <asp:BoundField DataField="NUM_RGTRO_EMPRG" HeaderText="Matricula" />
                                         <asp:BoundField DataField="NUM_CPF_EMPRG" HeaderText="CPF" />
                                        <asp:BoundField DataField="NUM_IDNTF_RPTANT" HeaderText="Representante" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnDeletar" runat="server" CssClass="button" Text="Deletar" OnClick="btnDeletar_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnInserir" runat="server" CssClass="button" Text="Inserir" OnClick="btnInserir_Click" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
