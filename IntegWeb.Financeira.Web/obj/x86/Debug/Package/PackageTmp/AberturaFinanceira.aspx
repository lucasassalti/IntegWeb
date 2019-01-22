<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="AberturaFinanceira.aspx.cs" Inherits="IntegWeb.Financeira.Web.AberturaFinanceira" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="MarginGrid">
        <asp:UpdatePanel runat="server" ID="UpdatePanel">
            <ContentTemplate>
                <div class="full_w">
                    <div class="tabelaPagina">
                        <h1>Fluxo de Caixa - Abertura Financeira</h1>


                        <div id="divFluxoEmpresas" runat="server" class="tabelaPagina">
                            <table>
                                <tr>
                                    <td>Mês:
                                        <asp:TextBox ID="txtMes" runat="server" onkeypress="mascara(this, soNumeros)" MaxLength="2" Width="100px" />
                                        <asp:RequiredFieldValidator runat="server" ID="redMes" ControlToValidate="txtMes" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidCampos" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Ano:
                                        <asp:TextBox ID="txtAno" runat="server" onkeypress="mascara(this, soNumeros)" MaxLength="4" Width="100px"/>
                                        <asp:RequiredFieldValidator runat="server" ID="reqAno" ControlToValidate="txtAno" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidCampos" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnRelatorio" runat="server" CssClass="button" Text="Carregar Relatório" CausesValidation="true" ValidationGroup="ValidCampos" OnClick="btnRelatorio_Click" />
                                        <asp:Button ID="btnLimpar" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimpar_Click" />
                                        <asp:Button ID="btnGerarRelatorio" runat="server" CssClass="button" Text="Gerar Relatório Consolidado" OnClick="btnGerarRelatorio_Click" Visible="false"/>


                                    </td>
                                </tr>
                            </table>

                            <asp:ObjectDataSource ID="odsAberturaFinanceira" runat="server"
                                TypeName="IntegWeb.Financeira.Aplicacao.DAL.Tesouraria.AberturaFinanceiraDAL"
                                SelectMethod="GetData"
                                SelectCountMethod="GetDataCount"
                                EnablePaging="True"
                                SortParameterName="sortParameter">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtMes" Name="mes" PropertyName="Text" Type="Int32" />
                                    <asp:ControlParameter ControlID="txtAno" Name="ano" PropertyName="Text" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

                            <asp:GridView ID="grdAberturaFinanceira" runat="server"
                                AutoGenerateColumns="false"
                                DataSourceID="odsAberturaFinanceira"
                                EmptyDataText="Não retornou registros"
                                 AllowPaging="True"
                                 AllowSorting="True"
                                CssClass="Table"
                                ClientIDMode="Static"
                                PageSize="150"
                                Visible="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Código" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdReg" runat="server" Text='<%# Bind("ID_REG") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Código" SortExpression="COD_EMPRS" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpresa" runat="server" Text='<%# Bind("COD_EMPRS") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Empresa" SortExpression="NOM_EMPRS" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblNomEmpresa" runat="server" Text='<%# Bind("NOM_EMPRS") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Natureza" SortExpression="NATUREZA">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNatureza" runat="server" Text='<%# Bind("NATUREZA") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Valor Participante">
                                        <ItemTemplate>
                                            <asp:Label ID="lblValorPart" runat="server" Text='<%# Bind("VALOR_PART","{0:N2}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Valor Bruto Credenciado ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblValorBrutoCred" runat="server" Text='<%# Bind("VALOR_CRED_BRUTO","{0:N2}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Imposto">
                                        <ItemTemplate>
                                            <asp:Label ID="lblImposto" runat="server" Text='<%# Bind("IMPOSTO","{0:N2}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Valor Credenciado Liquído">
                                        <ItemTemplate>
                                            <asp:Label ID="lblValorLiqCred" runat="server" Text='<%# Bind("VALOR_CRED_LIQ","{0:N2}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Aprovação">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgAprovacao" runat="server" ImageUrl='<%# (Eval("APROVACAO").ToString() == "N" ?  "~\\img\\i_empty.png" : "~\\img\\i_ok.png") %>'  ToolTip= '<%# (Eval("APROVACAO").ToString() == "N" ? "Aguardando Aprovação": "Aprovado")  %>' OnClick="imgAprovacao_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                            <uc1:ReportCrystal runat="server" ID="ReportCrystal" Style="display: none;" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <%--    <Triggers>
                <asp:PostBackTrigger ControlID="btnAvancar" />
            </Triggers>--%>
        </asp:UpdatePanel>
    </div>
</asp:Content>
