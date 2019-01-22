<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Contribuicao.aspx.cs" Inherits="IntegWeb.Previdencia.Web.Contribuicao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:UpdatePanel runat="server" ID="upUpdatePanel">
        <ContentTemplate>

            <div class="full_w">
                <div class="tabelaPagina">
                    <h1>Cálculo de Contribuições</h1>
                    <div id="divPesquisa" runat="server" class="tabelaPagina">
                        <table style="width: 274px;margin: 13px;">
                            <tr>
                                <td><span>Empresa:</span>
                                    <asp:TextBox ID="txtPesqEmpresa" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td><span>Matrícula:</span>
                                    <asp:TextBox ID="txtPesqMatricula" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td><span>Data Atualização:</span>
                                    <asp:TextBox ID="txtPesqDtUpd" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" />
                                </td>
                            </tr>
                            <tr>
                                <td><span>Data Pagamento:</span>
                                    <asp:TextBox ID="txtPesqDtPay" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Text="Pesquisar"  />
                                    <asp:Button ID="btnLimpar" runat="server" CssClass="button" Text="Limpar"  />
                                </td>
                            </tr>
                        </table>

                        
                    </div>
                </div>
            </div>
            <div class="full_w">
                <div class="tabelaPagina" id="divGrd">

                    <asp:GridView ID="grdPesquisa" runat="server"
                            AutoGenerateColumns="false"
                            EmptyDataText="Não Retornou Registros"
                            AllowPaging="True"
                            AllowSorting="True"
                            CssClass="Table"
                            PageSize="10"
                            ClientIDMode="Static">
                            <Columns>
                                <asp:TemplateField HeaderText="Plano" SortExpression="">
                                    <ItemTemplate>
                                        <asp:Label ID="lblX1" runat="server" Text='<%# Bind("Plano") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dt. Início" SortExpression="">
                                    <ItemTemplate>
                                        <asp:Label ID="lblX2" runat="server" Text='<%# Bind("DtInicio") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dt. Fim" SortExpression="">
                                    <ItemTemplate>
                                        <asp:Label ID="lblX3" runat="server" Text='<%# Bind("DtFim") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Unidade Monetária " SortExpression="">
                                    <ItemTemplate>
                                        <asp:Label ID="lblX5" runat="server" Text='<%# Bind("Unidade") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tipo Contribuição" SortExpression="">
                                    <ItemTemplate>
                                        <asp:Label ID="lblX4" runat="server" Text='<%# Bind("Tipo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                </div>
            </div>
            <div class="full_w">
                <div class="tabelaPagina">
                    <div style="margin: 13px;text-align: left;">
                        <asp:Button ID="Button1" runat="server" CssClass="button" Text="Incluir verbas de diferenças"  />
                        <asp:Button ID="Button2" runat="server" CssClass="button" Text="Excluir verbas carregadas"  />
                        <asp:Button ID="Button3" runat="server" CssClass="button" Text="Gerar relatório"  />
                        <asp:Button ID="Button4" runat="server" CssClass="button" Text="Carregar na capitalização"  />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
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
<style>
    .tabelaPagina > .tabelaPagina table tbody tr td > span { display: inline-block; width: 90px; }
    .tabelaPagina table { width: 97%; margin: 10px 0 0 15px; }
</style>
</asp:Content>